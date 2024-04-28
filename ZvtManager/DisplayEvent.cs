using Interop.DT98BaseDevicesLib;
using System;

namespace TaskStar.ZvtTest.ZvtManager
{
    public class DisplayEvent : BaseEvent, IBaseDisplayEvent
    {
        #region Public Fields

        public static Guid EventGuid = new Guid("{2827F9C6-8D3C-11D3-8F36-0080AD38DE23}");

        #endregion Public Fields

        #region Public Constructors

        public DisplayEvent(string terminalOid) : base(terminalOid)
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
        }

        public void FireNotifyResultExt(int nCookie, int nRequest, object vResult)
        {
        }

        #endregion Public Methods
    }
}