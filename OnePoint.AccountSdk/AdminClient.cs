// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdminClient.cs" company="OnePoint Global Ltd">
//   Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The admin client, manages creating a client to get access to entire account modules or routes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Net.Http;

using OnePoint.AccountSdk.Common;
using OnePoint.AccountSdk.Email;
using OnePoint.AccountSdk.GeoLocation;
using OnePoint.AccountSdk.Media;
using OnePoint.AccountSdk.PanelPanellist;
using OnePoint.AccountSdk.Project;
using OnePoint.AccountSdk.Questionnaire;
using OnePoint.AccountSdk.Sample;
using OnePoint.AccountSdk.Schedule;
using OnePoint.AccountSdk.Survey;
using OnePoint.AccountSdk.Theme;
using OnePoint.AccountSdk.User;

namespace OnePoint.AccountSdk
{
    /// <summary>
    /// The admin client class, provides the code to setup httpclient with API domain url and request handler.
    /// </summary>
    public class AdminClient
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        public UserRoute User { get; private set; }

        /// <summary>
        /// Gets the survey.
        /// </summary>
        public SurveyRoute Survey { get; private set; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        public ProjectRoute Project { get; private set; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        public EmailRoute Email { get; private set; }

        /// <summary>
        /// Gets the common.
        /// </summary>
        public CommonRoute Common { get; private set; }

        /// <summary>
        /// Gets the questionnaire.
        /// </summary>
        public QuestionnaireRoute Questionnaire { get; private set; }

        /// <summary>
        /// Gets the media.
        /// </summary>
        public MediaRoute Media { get; private set; }

        /// <summary>
        /// Gets the panel.
        /// </summary>
        public PanelRoute Panel { get; private set; }

        /// <summary>
        /// Gets the panellist.
        /// </summary>
        public PanellistRoute Panellist { get; private set; }

        /// <summary>
        /// Gets the profile element.
        /// </summary>
        public ProfileElementRoute ProfileElement { get; private set; }

        /// <summary>
        /// Gets the geo location.
        /// </summary>
        public GeoLocationRoute GeoLocation { get; private set; }

        /// <summary>
        /// Gets the geo fence.
        /// </summary>
        public GeoFencingRoute GeoFence { get; private set; }

        /// <summary>
        /// Gets the sample.
        /// </summary>
        public SampleRoute Sample { get; private set; }

        /// <summary>
        /// Gets the theme.
        /// </summary>
        public ThemeRoute Theme { get; private set; }

        /// <summary>
        /// Gets the scheduler.
        /// </summary>
        public SchedulerRoute Scheduler { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminClient"/> class, which initializes httpclinet with default live api domain.
        /// </summary>
        public AdminClient()
        {
            this.InitializeHandler("live");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminClient"/> class, initializes httpclinet with to specific api domain.
        /// </summary>
        /// <param name="domain">
        /// The domain.
        /// </param>
        public AdminClient(string domain)
        {
            this.InitializeHandler(domain);
        }

        /// <summary>
        /// The initialize request handler.
        /// </summary>
        /// <param name="hostType">
        /// The host type.
        /// </param>
        private void InitializeHandler(string hostType)
        {
            var httpClient = new HttpClient(new WebRequestHandler { ReadWriteTimeout = 10 * 1000 })
            {
                // Specify request level timeout which decides maximum time taht can be spent on
                // download/upload files..
                Timeout =
                                         TimeSpan
                                             .FromMinutes(
                                                 60)
            };

            var requestHandler = new AdminRequestHandler(httpClient, hostType);
            this.InitializeRoutes(requestHandler);
        }

        /// <summary>
        /// The initialize account modules or routes.
        /// </summary>
        /// <param name="handler">
        /// The handler.
        /// </param>
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