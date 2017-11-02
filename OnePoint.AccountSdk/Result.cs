using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace OnePoint.AccountSdk
{
    public class Result
    {
        public bool IsError { get; set; }

        public string ObjectResult { get; set; }

        public HttpResponseMessage HttpResponse { get; set; }

        public string DonwloadFileName => HttpResponse.Content.Headers.ContentDisposition.FileName;

        public string GetErrorJson
        {
            get
            {
                var error = new HttpError
                {
                    ErrorMessage = "Internal error!! Resource " + HttpResponse.StatusCode,
                    HttpStatusCode = Convert.ToString(HttpResponse.StatusCode)
                };
                return JsonConvert.SerializeObject(error);
            }
        }

        public T Decoder<T>(T obj) where T : class
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(this.ObjectResult);
        }

        public string JsonRead(string key)
        {
            var data = JObject.Parse(this.ObjectResult);
            if (data[key] != null)
            {
                return data[key].ToString();
            }

            return null;
        }

        public List<string> JsonReadByJarray(string key)
        {
            List<string> list = new List<string>();
            JArray v = JArray.Parse(this.ObjectResult);

            foreach (JObject j in v)
            {
                if (j[key] != null)
                {
                    list.Add(j[key].ToString());
                }
            }

            return list;
        }

        public void DownloadFile(string path)
        {
            byte[] buffer = this.HttpResponse.Content.ReadAsByteArrayAsync().Result;
            MemoryStream ms = new MemoryStream(buffer);
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            ms.WriteTo(file);
            file.Close();
            ms.Close();
        }

        public T ErrorToObject<T>(T obj, string message) where T : class
        {
            var error = new HttpError { ErrorMessage = message };

            var json = JsonConvert.SerializeObject(error);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }

        public T JsonToObject<T>(T obj, string tokenName = null) where T : class
        {
            if (!this.IsError && !string.IsNullOrEmpty(tokenName))
            {
                JToken token = JToken.Parse(this.ObjectResult);

                var jObj = new JObject();
                jObj[tokenName] = token;
                jObj["IsSuccess"] = true;

                this.ObjectResult = JsonConvert.SerializeObject(jObj);
            }
            else if (!this.IsError)
            {
                JObject jObj = JObject.Parse(this.ObjectResult);
                jObj["IsSuccess"] = true;
                this.ObjectResult = JsonConvert.SerializeObject(jObj);
            }

            return this.Decoder(obj);
        }

        public T JsonToObject<T>(T obj, string jsonstring, string tokenName = null) where T : class
        {
            if (!this.IsError && tokenName != null)
            {
                JToken token = JToken.Parse(jsonstring);

                var jObj = new JObject();
                jObj[tokenName] = token;
                jObj["IsSuccess"] = true;

                this.ObjectResult = JsonConvert.SerializeObject(jObj);
            }

            return this.Decoder(obj);
        }

        public string JsonToKeyValue(string Key)
        {
            JObject obj = JObject.Parse(this.ObjectResult);
            string value = (string)obj[Key];
            return value;
        }
    }
}
