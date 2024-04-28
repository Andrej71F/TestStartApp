using Microsoft.Extensions.Logging;

namespace TaskStar.ZvtTest.ZvtManager
{
    public delegate void NotifyErrorEventHandler(int nCookie, int nError, object vDetail);

    public delegate void NotifyResultEventHandler(int nCookie, int nRequest, object vResult);

    public class BaseEvent
    {
        #region Private Fields

        public ILogger<BaseEvent> Log { get; }

        public int Cookie { get; set; }

        public readonly string TerminalOid;

        #endregion Private Fields

        #region Public Constructors

        public BaseEvent(string terminalOid)
        {
            MyName = this.GetType().Name;
            TerminalOid = terminalOid;
            //Init Log
            Serilog.ILogger lg = LoggerFactory.CreateLoggerSerilog(null, "serilogconfig.json", "Serilog");
            Log = new Serilog.Extensions.Logging.SerilogLoggerFactory(lg).CreateLogger<BaseEvent>();
        }

        #endregion Public Constructors

        #region Public Events

        public event NotifyErrorEventHandler NotifyError;

        public event NotifyResultEventHandler NotifyResult;

        #endregion Public Events

        #region Public Properties

        public string MyName { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void FireNotifyError(int nCookie, int nError, object vDetail)
        {
            Log.LogInformation($"{MyName}: FireNotifyError: nCookie: {nCookie}, nError: {nError}, vDetail: {vDetail}");
            NotifyError?.Invoke(nCookie, nError, vDetail);
        }

        public void FireNotifyResult(int nCookie, int nRequest, object vResult)
        {
            Log.LogInformation($"{MyName}: FireNotifyResult: nCookie: {nCookie}, nRequest: {nRequest}, vResult: {vResult}");
            NotifyResult?.Invoke(nCookie, nRequest, vResult);
        }

        #endregion Public Methods
    }
}