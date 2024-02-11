using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using TaskStar.ZvtTest.Contracts;

namespace TaskStar.ZvtTest.StartApp.Controls.ViewModels
{
    public class ZvtManagerControlViewModel : ViewModelBase
    {
        #region Private Fields

        private List<string> _ZvtCommands = new List<string>()
        { "Payment", "Account Balancing", "Prepayment", "Storno" };

        private List<string> _PosCommands = new List<string>()
        { "Login", "Payment", "Prepayment" ,"Account Balancing", "LastReceipt", "TND IFSF"};

        private List<string> _Track2 = new List<string>()
        {
            "7005500000001999967=27611119992321421"
        };

        //private List<string> _Track2 = new List<string>()
        //{
        //    "9099900000000001=00000000000010000000",
        //    "9099900000001236=00000000123450000000",
        //    "789663060970763=250852142321011000"
        //   ,"EMV65=789663060970763=250852142321011000"
        //};

        private string _TerminalOid = string.Empty;

        private string _SelectedZvtCommand = string.Empty;

        private string _FreeText = string.Empty;

        private string _SelectedPosCommand = string.Empty;

        private decimal _Quantity = 0;

        private decimal _Amount = 0;

        private int _ProductCode = 0;

        private string _SelectedTrack2 = string.Empty;

        private string _LastSaleOid;

        private decimal _ActAmount = 0;

        private decimal _MinAmount = 0;

        private decimal _MaxAmount = 0;

        private string _Pan = string.Empty;

        private string _BuildTrack2 = string.Empty;

        #endregion Private Fields

        #region Private Methods

        private void Submit(object parameter)
        {
            if (parameter.ToString() == "InitZvt")
            {
                ZvtTestManager.Init(TerminalOid);
            }
            else if (parameter.ToString() == "TermZvt")
            {
                ZvtTestManager.Term();
            }
            else if (parameter.ToString() == "RunCommand")
            {
                RunCommand();
            }
            else if (parameter.ToString() == "RunPosCommand")
            {
                RunPosCommand();
            }
        }

        private void RunPosCommand()
        {
            string receiptOid = LastSaleOid;
            ActAmount = 0;
            MinAmount = 0;
            MaxAmount = 0;
            LastSaleOid = string.Empty;
            Pan = string.Empty;
            BuildTrack2 = string.Empty;
            FreeText = string.Empty;
            string track3 = SelectedTrack2;

            if (string.IsNullOrEmpty(SelectedTrack2))
            {
                SelectedTrack2 = "7013710010";
            }
            if (SelectedPosCommand == "Login")
            {
                ZvtTestManager.Login(Convert.ToInt32(TerminalOid));
            }
            else if (SelectedPosCommand == "Payment")
            {
                ZvtTestManager.PosPayment("Payment", SelectedTrack2, "", "", TerminalOid, false, ProductCode, Amount);
            }
            else if (SelectedPosCommand == "Account Balancing")
            {
                ZvtTestManager.PosAccountBalancing("AVIA_PREPAID_ACCOUNTBALANCE", SelectedTrack2, track3, "", TerminalOid, ProductCode);
            }
            else if (SelectedPosCommand == "Prepayment")
            {
                ZvtTestManager.PosPayment("AVIA_PREPAID_PREPAID", SelectedTrack2, track3, "", TerminalOid, false, ProductCode, Amount);
            }
            else if (SelectedPosCommand == "LastReceipt")
            {
                string receipt = ZvtTestManager.GetReceiptText("", SelectedTrack2, track3, "", TerminalOid, receiptOid);
                FreeText += receipt + Environment.NewLine;
            }
            else if (SelectedPosCommand == "TND IFSF")
            {
                ZvtTestManager.PosPayment("TND IFSF", SelectedTrack2, track3, "", TerminalOid, false, ProductCode, Amount);
            }
        }

        private void RunCommand()
        {
            if (SelectedZvtCommand == "Prepayment")
            {
                ZvtTestManager.Prepayment(SelectedTrack2, Amount, ProductCode);
            }
            else if (SelectedZvtCommand == "Payment")
            {
                ZvtTestManager.Payment(SelectedTrack2, Amount, ProductCode);
            }
            else if (SelectedZvtCommand == "Account Balancing")
            {
                ZvtTestManager.AccountBalancing(SelectedTrack2);
            }
            else if (SelectedZvtCommand == "Storno")
            {
                ZvtTestManager.Storno(Amount, Quantity.ToString());
            }
        }

        private bool CanSubmit(object parameter)
        {
            return true;
        }

        private void ZvtTestManager_OnAviaPrepaidResponse(object arg1, AviaPrepaidResponse arg2)
        {
            if (arg2.MinAmount != -1)
                MinAmount = arg2.MinAmount;
            if (arg2.MaxAmount != -1)
                MaxAmount = arg2.MaxAmount;
            if (arg2.ActualAmount != -1)
                ActAmount = arg2.ActualAmount;

            if (!string.IsNullOrEmpty(arg2.Pan))
                Pan = arg2.Pan;
            if (!string.IsNullOrEmpty(arg2.SaleOid))
                LastSaleOid = arg2.SaleOid;
        }

        private void ZvtTestManager_OnCardProzessNotify(object arg1, string arg2)
        {
            FreeText += arg2 + Environment.NewLine;
        }

        #endregion Private Methods

        #region Public Constructors

        public ZvtManagerControlViewModel() : base()
        {
            SubmitCommand = new DelegateCommand<object>(Submit, CanSubmit);
        }

        public ZvtManagerControlViewModel(IContainerRegistry containerRegistry
            , Microsoft.Extensions.Logging.ILogger logger
            , IZvtTestManager zvtTestManager) : base(containerRegistry, logger, zvtTestManager)
        {
            ZvtTestManager.OnAviaPrepaidResponse += ZvtTestManager_OnAviaPrepaidResponse;
            ZvtTestManager.OnCardProzessNotify += ZvtTestManager_OnCardProzessNotify;

            SubmitCommand = new DelegateCommand<object>(Submit, CanSubmit);
            TerminalOid = zvtTestManager.TerminalOid;
            SelectedZvtCommand = _ZvtCommands[0];
            SelectedPosCommand = _PosCommands[0];

            SelectedTrack2 = "927600362000001001=00000000000000000";
            SelectedPosCommand = "Payment";

            Amount = 11;
            ProductCode = 67;

            ZvtTestManager.Init(TerminalOid);
            ZvtTestManager.Login(Convert.ToInt32(TerminalOid));
        }

        #endregion Public Constructors

        #region Public Properties

        public DelegateCommand<object> SubmitCommand { get; private set; }
        public string TerminalOid { get => _TerminalOid; set => SetProperty(ref _TerminalOid, value); }
        public string SelectedZvtCommand { get => _SelectedZvtCommand; set => SetProperty(ref _SelectedZvtCommand, value); }

        public string SelectedPosCommand { get => _SelectedPosCommand; set => SetProperty(ref _SelectedPosCommand, value); }
        public decimal Quantity { get => _Quantity; set => SetProperty(ref _Quantity, value); }
        public decimal Amount { get => _Amount; set => SetProperty(ref _Amount, value); }
        public int ProductCode { get => _ProductCode; set => SetProperty(ref _ProductCode, value); }
        public List<string> ZvtCommands { get => _ZvtCommands; }

        public List<string> PosCommands { get => _PosCommands; }
        public string SelectedTrack2 { get => _SelectedTrack2; set => SetProperty(ref _SelectedTrack2, value); }

        public string LastSaleOid { get => _LastSaleOid; set => SetProperty(ref _LastSaleOid, value); }
        public decimal ActAmount { get => _ActAmount; set => SetProperty(ref _ActAmount, value); }
        public decimal MinAmount { get => _MinAmount; set => SetProperty(ref _MinAmount, value); }
        public decimal MaxAmount { get => _MaxAmount; set => SetProperty(ref _MaxAmount, value); }
        public string Pan { get => _Pan; set => SetProperty(ref _Pan, value); }
        public string BuildTrack2 { get => _BuildTrack2; set => SetProperty(ref _BuildTrack2, value); }
        public string FreeText { get => _FreeText; set => SetProperty(ref _FreeText, value); }
        public List<string> Track2 { get => _Track2; }

        #endregion Public Properties
    }
}