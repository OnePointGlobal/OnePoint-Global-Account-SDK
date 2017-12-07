using System;
using System.Net.Http;
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

namespace OnePoint.AccountSdk
{
    public class AdminClient
    {
        public UserRoute User { get; private set; }
        public SurveyRoute Survey { get; private set; }
        public ProjectRoute Project { get; private set; }
        public EmailRoute Email { get; private set; }
        public CommonRoute Common { get; private set; }
        public QuestionnaireRoute Questionnaire { get; private set; }
        public MediaRoute Media { get; private set; }
        public PanelRoute Panel { get; private set; }
        public PanellistRoute Panellist { get; private set; }
        public ProfileElementRoute ProfileElement { get; private set; }
        public GeoLocationRoute GeoLocation { get; private set; }
        public GeoFencingRoute GeoFence { get; private set; }
        public SampleRoute Sample { get; private set; }
        public ThemeRoute Theme { get; private set; }
        public SchedulerRoute Scheduler { get; private set; }

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
            User = new UserRoute(handler);
            Survey = new SurveyRoute(handler);
            Project = new ProjectRoute(handler);
            Email = new EmailRoute(handler);
            Common = new CommonRoute(handler);
            Questionnaire = new QuestionnaireRoute(handler);
            Media = new MediaRoute(handler);
            Panel = new PanelRoute(handler);
            Panellist = new PanellistRoute(handler);
            ProfileElement = new ProfileElementRoute(handler);
            GeoLocation = new GeoLocationRoute(handler);
            GeoFence = new GeoFencingRoute(handler);
            Sample = new SampleRoute(handler);
            Theme = new ThemeRoute(handler);
            Scheduler = new SchedulerRoute(handler);
        }
    }
}
