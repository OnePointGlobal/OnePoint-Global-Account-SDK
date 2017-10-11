using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace OnePoint.AccountSdk
{
    using OnePoint.AccountSdk.User;
    using OnePoint.AccountSdk.Survey;
    using OnePoint.AccountSdk.Project;
    using OnePoint.AccountSdk.Email;
    using OnePoint.AccountSdk.Common;
    using OnePoint.AccountSdk.Questionnaire;
    using OnePoint.AccountSdk.Media;
    using OnePoint.AccountSdk.PanelPanellist;
    using OnePoint.AccountSdk.GeoLocation;
    using OnePoint.AccountSdk.Sample;
    using OnePoint.AccountSdk.Theme;
    using OnePoint.AccountSdk.Schedule;

    public class AdminClient
    {
        public UserRoute user { get; private set; }
        public SurveyRoute survey { get; private set; }
        public ProjectRoute project { get; private set; }
        public EmailRoute email { get; private set; }
        public CommonRoute common { get; private set; }
        public QuestionnaireRoute questionnaire { get; private set; }
        public MediaRoute media { get; private set; }
        public PanelRoute panel { get; private set; }
        public PanellistRoute panellist { get; private set; }
        public ProfileElementRoute profileElement { get; private set; }
        public GeoLocationRoute geoLocation { get; private set; }
        public SampleRoute sample { get; private set; }
        public ThemeRoute theme { get; private set; }
        public SchedulerRoute scheduler { get; private set; }
        
        public AdminClient()
        {
            this.InitializeHandler("live");
        }

        public AdminClient(string domain)
        {
            this.InitializeHandler(domain);
        }


        private void InitializeHandler(string hostType)
        {
            var httpClient = new HttpClient(new WebRequestHandler { ReadWriteTimeout = 10 * 1000 })
            {
                // Specify request level timeout which decides maximum time taht can be spent on
                // download/upload files..
                Timeout = TimeSpan.FromMinutes(60)
            };

            var requestHandler = new AdminRequestHandler(httpClient, hostType);
            this.InitializeRoutes(requestHandler);
        }


        private void InitializeRoutes(AdminRequestHandler handler)
        {
            this.user = new UserRoute(handler);
            this.survey = new SurveyRoute(handler);
            this.project = new ProjectRoute(handler);
            this.email = new EmailRoute(handler);
            this.common = new CommonRoute(handler);
            this.questionnaire = new QuestionnaireRoute(handler);
            this.media = new MediaRoute(handler);
            this.panel = new PanelRoute(handler);
            this.panellist = new PanellistRoute(handler);
            this.profileElement = new ProfileElementRoute(handler);
            this.geoLocation = new GeoLocationRoute(handler);
            this.sample = new SampleRoute(handler);
            this.theme = new ThemeRoute(handler);
            this.scheduler = new SchedulerRoute(handler);
        }
    }
}
