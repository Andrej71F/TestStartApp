using Interop.DT98BaseDevicesLib;
using Microsoft.Extensions.Logging;
using System;

namespace TaskStar.ZvtTest.ZvtManager
{
    public class InputEvent : BaseEvent, IBaseInputEvent
    {
        #region Public Fields

        public static Guid EventGuid = new Guid("{2827F9C7-8D3C-11D3-8F36-0080AD38DE23}");

        #endregion Public Fields

        #region Public Constructors

        public InputEvent(string terminalOid) : base(terminalOid)
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
            Log.LogInformation($"NotifyError: nCookie: {nCookie}, nError: {nError}, vDetail: {vDetail}. MyCookie: {Cookie}");
        }

        public void FireNotifyResultExt(int nCookie, int nRequest, object vResult)
        {
            Log.LogInformation($"NotifyResult: nCookie: {nCookie}, nRequest: {nRequest}, vResult: {vResult}. MyCookie: {Cookie}");
        }

        #endregion Public Methods
    }
}