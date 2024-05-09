using Interop.TOMFundingIfaceLib;
using System;
using TaskStar.ZvtTest.Contracts;

namespace TaskStar.ZvtTest.ZvtManager
{
    public class ZvtTestManager : IZvtTestManager
    {
        #region Private Fields

        private string _TerminalOid = "2";//"1"; //"53";//"1";

        private ZvtWrapper _ZvtWrapper;

        private PaymentWrapper _PaymentWrapper;

        private IfsfTnd _IfsfTnd;

        #endregion Private Fields

        #region Private Methods

        private void _PaymentWrapper_OnAviaPrepaidResponse(object arg1, AviaPrepaidResponse arg2)
        {
            OnAviaPrepaidResponse?.Invoke(this, arg2);
        }

        private void _PaymentWrapper_OnCardNotify(object arg1, string arg2)
        {
            OnCardProzessNotify?.Invoke(this, arg2);
        }

        private void _PaymentWrapper_OnCashierNotify(object arg1, string arg2)
        {
            OnCardProzessNotify?.Invoke(this, arg2);
        }

        private void _ZvtWrapper_CardReaderErrorWrapper(Tuple<int, int, object> obj)
        {
            CardReaderError?.Invoke("Error" + Environment.NewLine);
        }

        private void _ZvtWrapper_CardReaderResultWrapper(Tuple<int, int, string> obj)
        {
            CardReaderResult?.Invoke(obj.Item3 + Environment.NewLine);
        }

        #endregion Private Methods

        #region Public Events

        public event Action<object, AviaPrepaidResponse> OnAviaPrepaidResponse;

        public event Action<object, string> OnCardProzessNotify;

        public event Action<string> CardReaderResult;

        public event Action<string> CardReaderError;

        #endregion Public Events

        #region Public Properties

        public string TerminalOid => _TerminalOid;

        #endregion Public Properties

        #region Public Methods

        public bool Init(string terminalOid)
        {
            _TerminalOid = terminalOid;

            _ZvtWrapper = ZvtWrapper.GetInstance(TerminalOid);
            _PaymentWrapper = new PaymentWrapper();
            _PaymentWrapper.OnAviaPrepaidResponse += _PaymentWrapper_OnAviaPrepaidResponse;
            _PaymentWrapper.OnCashierNotify += _PaymentWrapper_OnCashierNotify;
            _PaymentWrapper.OnCardNotify += _PaymentWrapper_OnCardNotify;
            _ZvtWrapper.CardReaderResultWrapper += _ZvtWrapper_CardReaderResultWrapper;
            _ZvtWrapper.CardReaderErrorWrapper += _ZvtWrapper_CardReaderErrorWrapper;

            _IfsfTnd = new IfsfTnd();
            return true;
        }

        public bool Term()
        {
            ZvtWrapper.TermInstance(_TerminalOid);
            return true;
        }

        public bool ReadCard()
        {
            _ZvtWrapper.CardReader.ReadCard(0, true, true, true, _ZvtWrapper.EventCardReader.Cookie);
            return true;
        }

        public bool AccountBalancing(string Track2)
        {
            //object tomEft = new  Interop.DTObjMgrLib.TOMEFundingIndication();
            //Type type = Type.GetTypeFromCLSID(new Guid("FF88B8AE-9284-4630-9617-F28C650E38C3"));
            //Object ob = Activator.CreateInstance(type);

            ITOMEFundingRequest tomEft = (ITOMEFundingRequest)Activator.CreateInstance(Type.GetTypeFromProgID("DTObjMgr.TOMEFundingRequest"));
            tomEft.RequestType = (int)TOMEFundingCommandCodesEnum.TOM_EFCC_ACCOUNT_BALANCE_REQUEST;
            tomEft.Track2 = Track2;
            _ZvtWrapper.EFunding.FundingRequest(tomEft, _ZvtWrapper.EventEFunding.Cookie);
            return true;
        }

        public bool Storno(decimal Amount, string BelegNr)
        {
            ITOMEFundingRequest tomEft = (ITOMEFundingRequest)Activator.CreateInstance(Type.GetTypeFromProgID("DTObjMgr.TOMEFundingRequest"));
            tomEft.TotalAmount = Amount;
            tomEft.TerminalReferenceID = BelegNr;
            tomEft.RequestType = (int)TOMEFundingCommandCodesEnum.TOM_EFCC_REVERSAL;
            tomEft.RequestID = ((int)TOMEFundingCommandCodesEnum.TOM_EFCC_REVERSAL).ToString();
            tomEft.ServiceLevel = "F";
            _ZvtWrapper.EFunding.Reversal(tomEft, _ZvtWrapper.EventEFunding.Cookie);
            return true;
        }

        public bool Prepayment(string Track2, decimal Amount, int ProductCode)
        {
            ITOMEFundingRequest tomEft = (ITOMEFundingRequest)Activator.CreateInstance(Type.GetTypeFromProgID("DTObjMgr.TOMEFundingRequest"));
            tomEft.TotalAmount = Amount;
            tomEft.Track2 = Track2;

            if (ProductCode > 0)
            {
                ITOMEFundingSaleItem pEFSaleItem = (ITOMEFundingSaleItem)Activator.CreateInstance(Type.GetTypeFromProgID("DTObjMgr.TOMEFundingSaleItem"));
                pEFSaleItem.ItemID = 1;
                pEFSaleItem.ProductCode = ProductCode;
                pEFSaleItem.Quantity = 1;
                pEFSaleItem.TotalAmount = Amount;
                pEFSaleItem.AddProductCode = ProductCode;
                pEFSaleItem.ProductName = "Avia Prepaid";
                tomEft.Add(pEFSaleItem);
            }

            _ZvtWrapper.EFunding.LoadPrepayCard(tomEft, _ZvtWrapper.EventEFunding.Cookie);
            return true;
        }

        public bool Payment(string Track2, decimal Amount, int ProductCode)
        {
            ITOMEFundingRequest tomEft = (ITOMEFundingRequest)Activator.CreateInstance(Type.GetTypeFromProgID("DTObjMgr.TOMEFundingRequest"));
            tomEft.TotalAmount = Amount;
            tomEft.Track2 = Track2;

            if (ProductCode > 0)
            {
                ITOMEFundingSaleItem pEFSaleItem = (ITOMEFundingSaleItem)Activator.CreateInstance(Type.GetTypeFromProgID("DTObjMgr.TOMEFundingSaleItem"));
                pEFSaleItem.ItemID = 1;
                pEFSaleItem.ProductCode = ProductCode;
                pEFSaleItem.Quantity = 1;
                pEFSaleItem.TotalAmount = Amount;
                pEFSaleItem.AddProductCode = ProductCode;
                pEFSaleItem.ProductName = "Avia Prepaid";
                tomEft.Add(pEFSaleItem);
            }

            _ZvtWrapper.EFunding.CardPayment(tomEft, _ZvtWrapper.EventEFunding.Cookie);
            return true;
        }

        public bool Login(int TerminalId)
        {
            return _PaymentWrapper.Login(TerminalId);
        }

        public bool PosPayment(string track1, string track2, string track3, string chip, string TerminalId, bool onlineCard, int ArticleId, decimal Amount)
        {
            if (track1 == "TND IFSF")
            {
                return _IfsfTnd.Pay(track1, track2, track3, chip, TerminalId, onlineCard, ArticleId, Amount);
            }
            else
            {
                return _PaymentWrapper.Payment(track1, track2, track3, chip, TerminalId, onlineCard, ArticleId, Amount, _ZvtWrapper.Zvt);
            }
        }

        public bool PosAccountBalancing(string track1, string track2, string track3, string chip, string TerminalId, int ArticleId)
        {
            return _PaymentWrapper.Payment(track1, track2, track3, chip, TerminalId, false, ArticleId, 10, _ZvtWrapper.Zvt);
        }

        public string GetReceiptText(string Track1, string Track2, string Track3, string Chip, string TerminalId, string receiptOid)
        {
            string receipt = _PaymentWrapper.GetReceiptText(Track1, Track2, Track3, Chip, TerminalId, receiptOid);
            _ZvtWrapper.Printer.PrintRawLine(receipt, 0);
            return receipt;
        }

        #endregion Public Methods
    }
}