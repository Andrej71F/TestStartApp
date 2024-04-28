using Interop.DT98BaseDevicesLib;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace TaskStar.ZvtTest.ZvtManager
{
    public class EftAdminEvent : BaseEvent, IBaseEFTAdminEvent, DIBaseEFTAdminEvent
    {
        #region Private Methods

        private string DumpShort(string indent, Interop.TOMFundingIfaceLib.ITOMEFTAdminIndication Info)
        {
            string _indent = indent ?? string.Empty;

            string _dump = string.Empty;
            if (Info != null)
            {
                _dump += Environment.NewLine + _indent + "Name                      : " + Info.Name;
                _dump += Environment.NewLine + _indent + "Interpretation            : " + Info.Interpretation;
                _dump += Environment.NewLine + _indent + "IndicationType            : " + Info.IndicationType;
                _dump += Environment.NewLine + _indent + "CommandCode               : " + Info.CommandCode;
                _dump += Environment.NewLine + _indent + "EventCode                 : " + Info.EventCode;
                _dump += Environment.NewLine + _indent + "EventDescription          : " + Info.EventDescription;
            }
            else
            {
                _dump += Environment.NewLine + _indent + "No Info data available";
            }
            return _dump;
        }

        private string Dump(string indent, Interop.TOMFundingIfaceLib.ITOMEFTAdminIndication Info)
        {
            string _indent = indent ?? string.Empty;

            string _dump = string.Empty;
            if (Info != null)
            {
                _dump += Environment.NewLine + _indent + "Name                      : " + Info.Name;
                _dump += Environment.NewLine + _indent + "Version                   : " + Info.Version;
                _dump += Environment.NewLine + _indent + "Interpretation            : " + Info.Interpretation;
                _dump += Environment.NewLine + _indent + "Count                     : " + Info.Count;
                _dump += Environment.NewLine + _indent + "IndicationType            : " + Info.IndicationType;
                _dump += Environment.NewLine + _indent + "CommandCode               : " + Info.CommandCode;
                _dump += Environment.NewLine + _indent + "EventCode                 : " + Info.EventCode;
                _dump += Environment.NewLine + _indent + "EventDescription          : " + Info.EventDescription;
                _dump += Environment.NewLine + _indent + "HostCode                  : " + Info.HostCode;
                _dump += Environment.NewLine + _indent + "HostCodeDescription       : " + Info.HostCodeDescription;
                _dump += Environment.NewLine + _indent + "ApplicationSender         : " + Info.ApplicationSender;
                _dump += Environment.NewLine + _indent + "WorkStationID             : " + Info.WorkStationID;
                _dump += Environment.NewLine + _indent + "POPID                     : " + Info.POPID;
                _dump += Environment.NewLine + _indent + "RequestID                 : " + Info.RequestID;
                _dump += Environment.NewLine + _indent + "TerminalID                : " + Info.TerminalID;
                _dump += Environment.NewLine + _indent + "TerminalBatch             : " + Info.TerminalBatch;
                _dump += Environment.NewLine + _indent + "STAN                      : " + Info.STAN;
                _dump += Environment.NewLine + _indent + "TerminalReferenceID       : " + Info.TerminalReferenceID;
                _dump += Environment.NewLine + _indent + "TotalAmount               : " + Info.TotalAmount;
                _dump += Environment.NewLine + _indent + "CurrencyCode              : " + Info.CurrencyCode;
                _dump += Environment.NewLine + _indent + "TerminalTimeStamp         : " + Info.TerminalTimeStamp;
                _dump += Environment.NewLine + _indent + "ReceiptData               : " + Info.ReceiptData;
                _dump += Environment.NewLine + _indent + "LanguageCode              : " + Info.LanguageCode;
            }
            else
            {
                _dump += Environment.NewLine + _indent + "No Info data available";
            }
            return _dump;
        }

        #endregion Private Methods

        #region Public Fields

        public static Guid EventGuid = new Guid("{CBF54593-C134-44E0-8351-135B7E7DD98E}");

        #endregion Public Fields

        #region Public Constructors

        public EftAdminEvent(string terminalOid) : base(terminalOid)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public new void NotifyError(int nCookie, int nError, object vDetail)
        {
            FireNotifyError(nCookie, nError, vDetail);
        }

        public new void NotifyResult(int nCookie, int nRequest, object vResult)
        {
            FireNotifyResult(nCookie, nRequest, vResult);
        }

        public void FireNotifyErrorExt(int nCookie, int nError, object vDetail)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Log.LogInformation($"{m.Name}:NotifyError: nCookie: {nCookie}, nError: {nError}, vDetail: {vDetail}. MyCookie: {Cookie}");
        }

        public void FireNotifyResultExt(int nCookie, int nRequest, object vResult)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Log.LogInformation($"NotifyResult: nCookie: {nCookie}, nRequest: {nRequest}, vResult: {vResult}. MyCookie: {Cookie}");
            if (!(vResult is Interop.TOMFundingIfaceLib.ITOMEFTAdminIndication))
            {
                Log.LogWarning("${ m.Name}: Received info is NOT of expected type 'ITOMEFTAdminIndication'.");
            }
            else
            {
                Interop.TOMFundingIfaceLib.ITOMEFTAdminIndication Info = vResult as Interop.TOMFundingIfaceLib.ITOMEFTAdminIndication;
                //log.LogInformation($"Received info is type 'ITOMEFTAdminIndication'  ({ConfigWrapper.GetInstance().SiteId}:{ConfigWrapper.GetInstance().TerminalId})>>>>");
                Log.LogInformation(DumpShort("", Info));
                Log.LogDebug(Dump("", Info));
            }
        }

        #endregion Public Methods
    }
}