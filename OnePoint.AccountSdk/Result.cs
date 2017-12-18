// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Result.cs" company="OnePoint Global Ltd">
//   Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The result, manages the method's to operate on API json response 
//   and decodes data into a object conversion.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OnePoint.AccountSdk
{
    /// <summary>
    /// The result class, provides the properties and methods to anylise api response json data and conversion to object type.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets or sets a value indicating whether is error.
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Gets or sets the object result.
        /// </summary>
        public string ObjectResult { get; set; }

        /// <summary>
        /// Gets or sets the http response.
        /// </summary>
        public HttpResponseMessage HttpResponse { get; set; }

        /// <summary>
        /// The donwload file name.
        /// </summary>
        public string DonwloadFileName => HttpResponse.Content.Headers.ContentDisposition.FileName;

        /// <summary>
        /// Gets the get error json.
        /// </summary>
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

        /// <summary>
        /// The decoder.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Decoder<T>(T obj)
            where T : class
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(this.ObjectResult);
        }

        /// <summary>
        /// The json read.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string JsonRead(string key)
        {
            var data = JObject.Parse(this.ObjectResult);
            if (data[key] != null)
            {
                return data[key].ToString();
            }

            return null;
        }

        /// <summary>
        /// The json read by jarray.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
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

        /// <summary>
        /// The download file.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public void DownloadFile(string path)
        {
            byte[] buffer = this.HttpResponse.Content.ReadAsByteArrayAsync().Result;
            MemoryStream ms = new MemoryStream(buffer);
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            ms.WriteTo(file);
            file.Close();
            ms.Close();
        }

        /// <summary>
        /// The error to object.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T ErrorToObject<T>(T obj, string message)
            where T : class
        {
            var error = new HttpError { ErrorMessage = message };

            var json = JsonConvert.SerializeObject(error);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }

        /// <summary>
        /// The json to object.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="tokenName">
        /// The token name.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T JsonToObject<T>(T obj, string tokenName = null)
            where T : class
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

        /// <summary>
        /// The json to object.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="jsonstring">
        /// The jsonstring.
        /// </param>
        /// <param name="tokenName">
        /// The token name.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T JsonToObject<T>(T obj, string jsonstring, string tokenName = null)
            where T : class
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

        /// <summary>
        /// The json to key value.
        /// </summary>
        /// <param name="Key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string JsonToKeyValue(string Key)
        {
            JObject obj = JObject.Parse(this.ObjectResult);
            string value = (string)obj[Key];
            return value;
        }
    }
}