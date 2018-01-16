using System.Diagnostics;

namespace OnePoint.AccountSdk.Schedule
{
    public class NotificationRoute
    {
        /// <summary>
        /// The result.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Result _result = new Result();

        /// <summary>
        /// Gets or sets the request handler.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AdminRequestHandler RequestHandler { get; }

        public NotificationRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }
    }
}
