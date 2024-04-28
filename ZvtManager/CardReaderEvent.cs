using Interop.DT98BaseDevicesLib;
using System;
using System.Reflection;

namespace TaskStar.ZvtTest.ZvtManager
{
    public class CardReaderEvent : BaseEvent, IBaseCardReaderEvent
    {
        #region Public Fields

        public static Guid EventGuid = new Guid("{2827F9C9-8D3C-11D3-8F36-0080AD38DE23}");

        #endregion Public Fields

        #region Public Constructors

        public CardReaderEvent(string terminalOid) : base(terminalOid)
        {
        }

        #endregion Public Constructors

        #region Public Events

        public event Action<Tuple<int, int, string>> NotifyResultExt;

        public event Action<Tuple<int, int, object>> NotifyErrorExt;

        #endregion Public Events

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
            NotifyErrorExt?.Invoke(new Tuple<int, int, object>(nCookie, nError, (string)vDetail));
        }

        public void FireNotifyResultExt(int nCookie, int nRequest, object vResult)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            NotifyResultExt?.Invoke(new Tuple<int, int, string>(nCookie, nRequest, (string)vResult));
        }

        #endregion Public Methods
    }
}