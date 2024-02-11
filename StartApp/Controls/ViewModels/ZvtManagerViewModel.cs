using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Threading;
using TaskStar.ZvtTest.Contracts;

namespace TaskStar.ZvtTest.StartApp.Controls.ViewModels
{
    public class ZvtManagerViewModel : ViewModelBase
    {
        #region Private Fields

        private string _TerminalOid = string.Empty;

        private string _FreeText = string.Empty;

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
            else if (parameter.ToString() == "ReadCard")
            {
                ZvtTestManager.ReadCard();
            }
        }

        private bool CanSubmit(object parameter)
        {
            return true;
        }

        int readCounter = 0;
        private void ZvtTestManager_CardReader(string obj)
        {
            FreeText = readCounter++.ToString("000  ") + obj + FreeText;
            Thread.Sleep(1000);
            ZvtTestManager.ReadCard();
        }

        #endregion Private Methods

        #region Public Constructors

        public ZvtManagerViewModel() : base()
        {
            SubmitCommand = new DelegateCommand<object>(Submit, CanSubmit);
        }

        public ZvtManagerViewModel(IContainerRegistry containerRegistry
            , Microsoft.Extensions.Logging.ILogger logger
            , IZvtTestManager zvtTestManager) : base(containerRegistry, logger, zvtTestManager)
        {
            SubmitCommand = new DelegateCommand<object>(Submit, CanSubmit);
            TerminalOid = zvtTestManager.TerminalOid;
            ZvtTestManager.Init(TerminalOid);
            ZvtTestManager.Login(Convert.ToInt32(TerminalOid));
            ZvtTestManager.CardReaderResult += ZvtTestManager_CardReader;
            ZvtTestManager.CardReaderError += ZvtTestManager_CardReader;
        }

        #endregion Public Constructors

        #region Public Properties

        public DelegateCommand<object> SubmitCommand { get; private set; }
        public string TerminalOid { get => _TerminalOid; set => SetProperty(ref _TerminalOid, value); }
        public string FreeText { get => _FreeText; set => SetProperty(ref _FreeText, value); }

        #endregion Public Properties
    }
}