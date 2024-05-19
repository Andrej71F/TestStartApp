using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskStar.ZvtTest.Contracts;
using TaskStar.ZvtTest.ZvtManager;

namespace TaskStar.ZvtTest.StartApp.Controls.ViewModels
{
    public class HoyerIfsfViewModel : ViewModelBase
    {
        #region Private Fields

        private readonly List<string> _PosCommands = new List<string>()
        { "Retrieve Card Attribute", "Indoor Payment", "Outdoor Payment"};

        private string _SelectedZvtCommand = string.Empty;

        private string _FreeText = string.Empty;

        private int _Amount = 10;

        private int _ProductCode = 2;

        private int _AppId = 6;

        private int _Site = 123412345;

        private int _MileAge = 987654321;

        private int _TerminalId = 3;

        private int _TerminalType = 1;

        private string _SelectedTrack2 = string.Empty;

        private HoyerIfsf _hoyer;

        #endregion Private Fields

        #region Private Methods

        private void Submit(object parameter)
        {
            RunCommand();
        }

        private void RunCommand()
        {
            if (SelectedZvtCommand == "Retrieve Card Attribute")
            {
                FreeText = string.Empty;
                FreeText = _hoyer.RetrieveCardAttribute(SelectedTrack2, Site, AppId, TerminalType, TerminalId);
            }
            else if (SelectedZvtCommand == "Indoor Payment")
            {
                _hoyer.IndoorPayment(SelectedTrack2, Site, AppId, TerminalType, TerminalId, ProductCode, Amount, MileAge);
            }
            else if (SelectedZvtCommand == "Outdoor Payment")
            {
            }
        }

        private bool CanSubmit(object parameter)
        {
            return true;
        }

        #endregion Private Methods

        #region Public Constructors

        public HoyerIfsfViewModel() : base()
        {
            SubmitCommand = new DelegateCommand<object>(Submit, CanSubmit);
            _hoyer = new HoyerIfsf();
        }

        public HoyerIfsfViewModel(IContainerRegistry containerRegistry, Microsoft.Extensions.Logging.ILogger logger)
            : base(containerRegistry, logger, null)
        {
            _hoyer = new HoyerIfsf();

            SubmitCommand = new DelegateCommand<object>(Submit, CanSubmit);

            Track2 = _hoyer.CardData.Select(s => s.Pan + ", " + s.Pin + " ," + s.HoyerCardType).ToList();
            SelectedTrack2 = Track2.ElementAt(0);
            SelectedZvtCommand = PosCommands.ElementAt(0);
        }

        #endregion Public Constructors

        #region Public Properties

        public DelegateCommand<object> SubmitCommand { get; private set; }
        public string SelectedZvtCommand { get => _SelectedZvtCommand; set => SetProperty(ref _SelectedZvtCommand, value); }
        public int Amount { get => _Amount; set => SetProperty(ref _Amount, value); }
        public int ProductCode { get => _ProductCode; set => SetProperty(ref _ProductCode, value); }

        public List<string> PosCommands { get => _PosCommands; }
        public string SelectedTrack2 { get => _SelectedTrack2; set => SetProperty(ref _SelectedTrack2, value); }
        public string FreeText { get => _FreeText; set => SetProperty(ref _FreeText, value); }
        public List<string> Track2 { get; private set; }

        public int AppId { get => _AppId; set => SetProperty(ref _AppId, value); }

        public int TerminalType { get => _TerminalType; set => SetProperty(ref _TerminalType, value); }

        public int TerminalId { get => _TerminalId; set => SetProperty(ref _TerminalId, value); }

        public int Site { get => _Site; set => SetProperty(ref _Site, value); }

        public int MileAge { get => _MileAge; set => SetProperty(ref _MileAge, value); }

        #endregion Public Properties
    }
}