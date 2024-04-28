using Interop.DT98BaseDevicesLib;
using Interop.DT98ConfigSvrLib;
using Interop.DT98ZVTManagerSvrLib;
using Interop.DT98ZVTSvrLib;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace TaskStar.ZvtTest.ZvtManager
{
    public class ZvtWrapper
    {
        #region Private Fields

        private static IList<ZvtWrapper> _ZvtCollections = new List<ZvtWrapper>();

        private ILogger<ZvtWrapper> log;

        private string _TerminalOid;

        private IZVTManager _ZvtManager;

        private IZvtUnit _ZvtUnit;

        private long _NumZvts;

        private Interop.DT98ZVTSvrLib.Zvt _Zvt;

        private IBaseDisplay _Display;

        private IBaseCardReader _CardReader;

        private IBasePrinter _Printer;

        private CardReaderEvent _EventCardReader;

        private IBaseInput _Input;

        private IBaseEFTAdmin _EftAdmin;

        private IBaseEFunding _EFunding;

        private IBaseSecurityModule _Security;

        private DisplayEvent _EventDisplay;

        private InputEvent _EventInput;

        private EftAdminEvent _EventEftAdmin;

        private EFundingEvent _EventEFunding;

        private SecurityEvent _EventSecurity;

        #endregion Private Fields

        #region Private Methods

        private void InitPrinter()
        {
            _Printer = Zvt.GetPrinter();
        }

        private void InitCardReader()
        {
            _CardReader = Zvt.GetCardReader();
            _CardReader.InitCardReader();
            log.LogInformation($"InitCardReader");

            //Event
            System.Runtime.InteropServices.ComTypes.IConnectionPointContainer cpc = (System.Runtime.InteropServices.ComTypes.IConnectionPointContainer)_CardReader;
            log.LogInformation("found ConnectionPointContainer");
            System.Runtime.InteropServices.ComTypes.IConnectionPoint ppCP = null;
            cpc.FindConnectionPoint(ref CardReaderEvent.EventGuid, out ppCP);
            _EventCardReader = new CardReaderEvent(_TerminalOid);
            ((BaseEvent)_EventCardReader).NotifyResult += _EventCardReader.FireNotifyResultExt;
            ((BaseEvent)_EventCardReader).NotifyError += _EventCardReader.FireNotifyErrorExt;
            ppCP.Advise(_EventCardReader, out int cookie);
            _EventCardReader.Cookie = cookie;
            _EventCardReader.NotifyErrorExt += _EventCardReader_NotifyErrorExt;
            _EventCardReader.NotifyResultExt += _EventCardReader_NotifyResultExt;
            log.LogInformation($"sink advise succesfull: {_EventCardReader.Cookie}");
        }

        private void _EventCardReader_NotifyResultExt(Tuple<int, int, string> obj)
        {
            CardReaderResultWrapper?.Invoke(obj);
        }

        private void _EventCardReader_NotifyErrorExt(Tuple<int, int, object> obj)
        {
            CardReaderErrorWrapper?.Invoke(obj);
        }

        private void InitDisplay()
        {
            try
            {
                _Display = Zvt.GetDisplay();
                _Display.InitDisplay();
                log.LogInformation($"InitDisplay");

                //initialise event from card terminal
                System.Runtime.InteropServices.ComTypes.IConnectionPointContainer cpc = (System.Runtime.InteropServices.ComTypes.IConnectionPointContainer)_Display;
                log.LogInformation("found ConnectionPointContainer");
                System.Runtime.InteropServices.ComTypes.IConnectionPoint ppCP = null;
                cpc.FindConnectionPoint(ref DisplayEvent.EventGuid, out ppCP);
                log.LogInformation("found FindConnectionPoint");
                _EventDisplay = new DisplayEvent(_TerminalOid);
                ((BaseEvent)_EventDisplay).NotifyResult += _EventDisplay.FireNotifyResultExt;
                ((BaseEvent)_EventDisplay).NotifyError += _EventDisplay.FireNotifyErrorExt;
                ppCP.Advise(_EventDisplay, out int cookie);
                _EventDisplay.Cookie = cookie;
                log.LogInformation($"sink advise succesfull: {_EventDisplay.Cookie}");
            }
            catch
            {
            }
        }

        private void InitInput()
        {
            _Input = Zvt.GetInput();
            _Input.InitInput();
            log.LogInformation($"InitInput");

            //Event
            System.Runtime.InteropServices.ComTypes.IConnectionPointContainer cpc = (System.Runtime.InteropServices.ComTypes.IConnectionPointContainer)_Input;
            log.LogInformation("found ConnectionPointContainer");
            System.Runtime.InteropServices.ComTypes.IConnectionPoint ppCP = null;
            cpc.FindConnectionPoint(ref InputEvent.EventGuid, out ppCP);
            _EventInput = new InputEvent(_TerminalOid);
            ((BaseEvent)_EventInput).NotifyResult += _EventInput.FireNotifyResultExt;
            ((BaseEvent)_EventInput).NotifyError += _EventInput.FireNotifyErrorExt;
            ppCP.Advise(_EventInput, out int cookie);
            _EventInput.Cookie = cookie;
            log.LogInformation($"sink advise succesfull: {_EventInput.Cookie}");
        }

        private void InitEftAdmin()
        {
            _EftAdmin = (IBaseEFTAdmin)Zvt.GetDevice((int)TerminalComponents.TERMCOMP_EFT_ADMIN);
            log.LogInformation($"InitEftAdmin");

            //Event
            System.Runtime.InteropServices.ComTypes.IConnectionPointContainer cpc = (System.Runtime.InteropServices.ComTypes.IConnectionPointContainer)_EftAdmin;
            log.LogInformation("found ConnectionPointContainer");
            System.Runtime.InteropServices.ComTypes.IConnectionPoint ppCP = null;
            cpc.FindConnectionPoint(ref EftAdminEvent.EventGuid, out ppCP);
            _EventEftAdmin = new EftAdminEvent(_TerminalOid);
            ((BaseEvent)_EventEftAdmin).NotifyResult += _EventEftAdmin.FireNotifyResultExt;
            ((BaseEvent)_EventEftAdmin).NotifyError += _EventEftAdmin.FireNotifyErrorExt;
            ppCP.Advise(_EventEftAdmin, out int cookie);
            _EventEftAdmin.Cookie = cookie;
            log.LogInformation($"sink advise succesfull: {_EventEftAdmin.Cookie}");
        }

        private void InitEfunding()
        {
            _EFunding = (IBaseEFunding)Zvt.GetDevice((int)TerminalComponents.TERMCOMP_E_FUNDING);
            log.LogInformation($"InitEfunding");

            //Event
            System.Runtime.InteropServices.ComTypes.IConnectionPointContainer cpc = (System.Runtime.InteropServices.ComTypes.IConnectionPointContainer)_EFunding;
            log.LogInformation("found ConnectionPointContainer");
            System.Runtime.InteropServices.ComTypes.IConnectionPoint ppCP = null;
            cpc.FindConnectionPoint(ref EFundingEvent.EventGuid, out ppCP);
            _EventEFunding = new EFundingEvent(_TerminalOid);
            ((BaseEvent)_EventEFunding).NotifyResult += _EventEFunding.FireNotifyResultExt;
            ((BaseEvent)_EventEFunding).NotifyError += _EventEFunding.FireNotifyErrorExt;
            ppCP.Advise(_EventEFunding, out int cookie);
            _EventEFunding.Cookie = cookie;
            log.LogInformation($"sink advise succesfull: {_EventEFunding.Cookie}");
        }

        private void InitSecurityModul()
        {
            _Security = Zvt.GetSecurityModule();
            _Security.InitSecurityModule();

            ////Event
            System.Runtime.InteropServices.ComTypes.IConnectionPointContainer cpc = (System.Runtime.InteropServices.ComTypes.IConnectionPointContainer)_Security;
            log.LogInformation("found ConnectionPointContainer");

            System.Runtime.InteropServices.ComTypes.IConnectionPoint ppCP = null;
            cpc.FindConnectionPoint(ref SecurityEvent.EventGuid, out ppCP);
            log.LogInformation("found FindConnectionPoint");

            _EventSecurity = new SecurityEvent(_TerminalOid);
            ((BaseEvent)_EventSecurity).NotifyResult += _EventSecurity.FireNotifyResultExt;
            ((BaseEvent)_EventSecurity).NotifyError += _EventSecurity.FireNotifyErrorExt;
            ppCP.Advise(_EventSecurity, out int cookie);
            _EventSecurity.Cookie = cookie;
            log.LogInformation($"sink advise succesfull: {_EventSecurity.Cookie}");
        }

        private void TermDisplay()
        {
            _Display?.TermDisplay();
            ((BaseEvent)_EventDisplay).NotifyResult -= _EventDisplay.FireNotifyResultExt;
            ((BaseEvent)_EventDisplay).NotifyError -= _EventDisplay.FireNotifyErrorExt;

            System.Runtime.InteropServices.ComTypes.IConnectionPointContainer cpc = (System.Runtime.InteropServices.ComTypes.IConnectionPointContainer)_Display;
            log.LogInformation("Unadvise: found ConnectionPointContainer");
            System.Runtime.InteropServices.ComTypes.IConnectionPoint ppCP = null;
            cpc.FindConnectionPoint(ref DisplayEvent.EventGuid, out ppCP);
            ppCP.Unadvise(_EventDisplay.Cookie);
            log.LogInformation($"sink unadvise succesfull: {_EventDisplay.Cookie}");
        }

        private void TermPrinter()
        {
            _Printer.TermPrinter();
        }

        private void TermCardReader()
        {
            _CardReader?.TermCardReader();
            ((BaseEvent)_EventCardReader).NotifyResult -= _EventCardReader.FireNotifyResultExt;
            ((BaseEvent)_EventCardReader).NotifyError -= _EventCardReader.FireNotifyErrorExt;

            System.Runtime.InteropServices.ComTypes.IConnectionPointContainer cpc = (System.Runtime.InteropServices.ComTypes.IConnectionPointContainer)_CardReader;
            log.LogInformation("Unadvise: found ConnectionPointContainer");
            System.Runtime.InteropServices.ComTypes.IConnectionPoint ppCP = null;
            cpc.FindConnectionPoint(ref CardReaderEvent.EventGuid, out ppCP);
            ppCP.Unadvise(_EventCardReader.Cookie);
            log.LogInformation($"sink unadvise succesfull: {_EventCardReader.Cookie}");
        }

        private void TermSecurityModul()
        {
            _Security?.TermSecurityModule();
            ((BaseEvent)_EventSecurity).NotifyResult -= _EventSecurity.FireNotifyResultExt;
            ((BaseEvent)_EventSecurity).NotifyError -= _EventSecurity.FireNotifyErrorExt;

            System.Runtime.InteropServices.ComTypes.IConnectionPointContainer cpc = (System.Runtime.InteropServices.ComTypes.IConnectionPointContainer)_Security;
            log.LogInformation("Unadvise: found ConnectionPointContainer");
            System.Runtime.InteropServices.ComTypes.IConnectionPoint ppCP = null;
            cpc.FindConnectionPoint(ref SecurityEvent.EventGuid, out ppCP);
            ppCP.Unadvise(_EventSecurity.Cookie);
            log.LogInformation($"sink unadvise succesfull: {_EventSecurity.Cookie}");
        }

        private void TermInput()
        {
            _Input?.TermInput();
            ((BaseEvent)_EventInput).NotifyResult -= _EventInput.FireNotifyResultExt;
            ((BaseEvent)_EventInput).NotifyError -= _EventInput.FireNotifyErrorExt;

            System.Runtime.InteropServices.ComTypes.IConnectionPointContainer cpc = (System.Runtime.InteropServices.ComTypes.IConnectionPointContainer)_Input;
            log.LogInformation("Unadvise: found ConnectionPointContainer");
            System.Runtime.InteropServices.ComTypes.IConnectionPoint ppCP = null;
            cpc.FindConnectionPoint(ref InputEvent.EventGuid, out ppCP);
            ppCP.Unadvise(_EventInput.Cookie);
            log.LogInformation($"sink unadvise succesfull: {_EventInput.Cookie}");
        }

        private void TermEftAdmin()
        {
            _EftAdmin?.Term();
            ((BaseEvent)_EventEftAdmin).NotifyResult -= _EventEftAdmin.FireNotifyResultExt;
            ((BaseEvent)_EventEftAdmin).NotifyError -= _EventEftAdmin.FireNotifyErrorExt;

            System.Runtime.InteropServices.ComTypes.IConnectionPointContainer cpc = (System.Runtime.InteropServices.ComTypes.IConnectionPointContainer)_EftAdmin;
            log.LogInformation("Unadvise: found ConnectionPointContainer");
            System.Runtime.InteropServices.ComTypes.IConnectionPoint ppCP = null;
            cpc.FindConnectionPoint(ref EftAdminEvent.EventGuid, out ppCP);
            ppCP.Unadvise(_EventEftAdmin.Cookie);
            log.LogInformation($"sink unadvise succesfull: {_EventEftAdmin.Cookie}");
        }

        private void TermEfunding()
        {
            _EFunding?.Term();
            ((BaseEvent)_EventEFunding).NotifyResult -= _EventEFunding.FireNotifyResultExt;
            ((BaseEvent)_EventEFunding).NotifyError -= _EventEFunding.FireNotifyErrorExt;

            System.Runtime.InteropServices.ComTypes.IConnectionPointContainer cpc = (System.Runtime.InteropServices.ComTypes.IConnectionPointContainer)_EFunding;
            log.LogInformation("Unadvise: found ConnectionPointContainer");
            System.Runtime.InteropServices.ComTypes.IConnectionPoint ppCP = null;
            cpc.FindConnectionPoint(ref EFundingEvent.EventGuid, out ppCP);
            ppCP.Unadvise(_EventEFunding.Cookie);
            log.LogInformation($"sink unadvise succesfull: {_EventEFunding.Cookie}");
        }

        #endregion Private Methods

        #region Public Constructors

        public ZvtWrapper(string terminalOid)
        {
            //Init Log
            Serilog.ILogger lg = TaskStar.ZvtTest.ZvtManager.LoggerFactory.CreateLoggerSerilog(null, "serilogconfig.json", "Serilog");
            log = new Serilog.Extensions.Logging.SerilogLoggerFactory(lg).CreateLogger<ZvtWrapper>();

            _TerminalOid = terminalOid;
            log.LogInformation($"create {this.GetType().Name} for Terminal Oid: {terminalOid}");
        }

        #endregion Public Constructors

        #region Public Events

        public event Action<Tuple<int, int, string>> CardReaderResultWrapper;

        public event Action<Tuple<int, int, object>> CardReaderErrorWrapper;

        #endregion Public Events

        #region Public Properties

        public CardReaderEvent EventCardReader => _EventCardReader;
        public IBaseDisplay Display { get => _Display; }
        public IBaseCardReader CardReader { get => _CardReader; }
        public IBaseSecurityModule Security { get => _Security; }
        public IBaseEFTAdmin EftAdmin { get => _EftAdmin; }
        public IBaseEFunding EFunding { get => _EFunding; }
        public IBaseInput Input { get => _Input; }
        public DisplayEvent EventDisplay { get => _EventDisplay; }
        public InputEvent EventInput { get => _EventInput; }
        public EftAdminEvent EventEftAdmin { get => _EventEftAdmin; }
        public EFundingEvent EventEFunding { get => _EventEFunding; }
        public SecurityEvent EventSecurity { get => _EventSecurity; }
        public Zvt Zvt { get => _Zvt; }
        public IBasePrinter Printer { get => _Printer; }

        #endregion Public Properties

        #region Public Methods

        public static ZvtWrapper GetInstance(string terminalOid)
        {
            ZvtWrapper tmp = _ZvtCollections.Where(t => t._TerminalOid == terminalOid).FirstOrDefault();
            if (tmp == null)
            {
                tmp = new ZvtWrapper(terminalOid);
                tmp.InitTerminal();
                _ZvtCollections.Add(tmp);
            }
            return tmp;
        }

        public static ZvtWrapper TermInstance(string terminalOid)
        {
            ZvtWrapper tmp = _ZvtCollections.Where(t => t._TerminalOid == terminalOid).FirstOrDefault();
            if (tmp != null)
            {
                tmp.TermTerminal();
                _ZvtCollections.Remove(tmp);
            }
            return tmp;
        }

        public void TermTerminal()
        {
            //term components
            TermDisplay();
            TermCardReader();
            TermSecurityModul();
            TermInput();
            TermEftAdmin();
            TermEfunding();
            TermPrinter();

            _NumZvts = -1;

            Zvt.TermZVT();

            _ = Marshal.ReleaseComObject(_ZvtManager);

            _ZvtUnit = null;
            _ZvtManager = null;

            ZvtWrapper tmp = _ZvtCollections.Where(t => t._TerminalOid == _TerminalOid).FirstOrDefault();
        }

        public void InitTerminal()
        {
            //Zvt Manager Svr
            _ZvtManager = new ZVTManagerClass();

            //ZVT Unit
            _ZvtUnit = (IZvtUnit)_ZvtManager.GetZVT(_TerminalOid);
            //Count of ZVTs unit
            _NumZvts = _ZvtUnit.NumberZvtUnits();

            //Only read and init index 0
            _Zvt = _ZvtUnit.ZvtByIndex(0);
            _Zvt.InitZVT();

            //init components
            InitDisplay();
            InitCardReader();
            InitSecurityModul();
            InitInput();
            InitEftAdmin();
            InitEfunding();
            InitPrinter();
        }

        #endregion Public Methods
    }
}