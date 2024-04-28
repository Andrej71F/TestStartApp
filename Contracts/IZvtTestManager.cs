using System;

namespace TaskStar.ZvtTest.Contracts
{
    public interface IZvtTestManager
    {
        #region Public Events

        event Action<object, AviaPrepaidResponse> OnAviaPrepaidResponse;

        event Action<object, string> OnCardProzessNotify;

        public event Action<string> CardReaderResult;

        public event Action<string> CardReaderError;

        #endregion Public Events

        #region Public Properties

        string TerminalOid { get; }

        #endregion Public Properties

        #region Public Methods

        bool Init(string terminalOid);

        bool Term();

        bool AccountBalancing(string Track2);

        bool Prepayment(string Track2, decimal Amount, int ProductCode);

        bool Storno(decimal Amount, string BelegNr);

        bool Payment(string Track2, decimal Amount, int ProductCode);

        bool Login(int TerminalID);

        bool PosPayment(string track1, string track2, string track3, string chip, string TerminalId, bool onlineCard, int ArticleId, decimal Amount);

        bool PosAccountBalancing(string track1, string track2, string track3, string chip, string TerminalId, int ArticleId);

        string GetReceiptText(string Track1, string Track2, string Track3, string Chip, string TerminalId, string receiptOid);

        bool ReadCard();

        #endregion Public Methods
    }

    public class AviaPrepaidResponse
    {
        #region Public Properties

        public decimal ActualAmount { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public string Pan { get; set; }

        public string SaleOid { get; set; }

        #endregion Public Properties
    }
}