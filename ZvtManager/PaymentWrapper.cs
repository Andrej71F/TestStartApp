using Interop.DT98_ARTS_InterfacesLib;
using Interop.DT98BaseCardLib;
using System;
using System.Collections.Generic;
using TaskStar.ZvtTest.Contracts;

namespace TaskStar.ZvtTest.ZvtManager
{
    public class PaymentWrapper
    {
        #region Private Fields

        private string track1 = string.Empty;

        private string track2 = string.Empty;

        private string track3 = string.Empty;

        private string chip = string.Empty;

        private int error = 0;

        private Interop.DT98_ARTS_InterfacesLib.IDT98_ARTS_Session _Session;

        private DT98CARDFACTORYLib.ICardCtl _CardSvr;

        private Interop.DT98BaseCardLib.BaseCard _BaseCard;

        private Interop.DT98_ARTS_InterfacesLib.IDT98_ARTS_PosAccessControl _PosAccessControl = null;

        private Interop.DT98SalesSvrLib.DT98SalesSvrClass _SalesSvr;

        private int _LockKey;

        private Interop.DT98TextSvrLib.DT98TextGenClass _DBT98TextGenClass;

        #endregion Private Fields

        #region Private Destructors

        ~PaymentWrapper()
        {
        }

        #endregion Private Destructors

        #region Private Methods

        private void _BaseCard_DisplayCashierText(string bstrOID, int TextModuleID, int TextId, int Language)
        {
            string text = _DBT98TextGenClass.GetText(TextModuleID, Language, TextId);
            OnCashierNotify?.Invoke(this, $"DisplayCashierText: bstrOID: '{bstrOID}', TextModuleID: '{TextModuleID}', TextId: '{TextId}', Language: '{Language}': {text}");
        }

        private void _BaseCard_Notify(string bstrOID, int nEvent, string bstrReceipt, int AnzReceipt)
        {
            List<string> messages = new List<string>();

            OnCardNotify?.Invoke(this, $"BaseCard_Notify-> bstrOID: '{bstrOID}', nEvent: '{nEvent}', AnzReceipt: '{AnzReceipt}', bstrReceipt: '{bstrReceipt}'");

            //N	Normal termination
            //A	Card Processing aborted
            //S	Special handling like cashier message or display or ...
            //-	Not used for indoor
            switch (nEvent)
            {
                case 0: //The processing went OK /N
                case 47: //Receipt is ready to print     //24-06-2016af
                    break;

                case 1: //The card is contained in the black list /A
                    break;

                case 2: //The card contained improper data /A
                    break;

                case 3: //The card is expired or has awrong check digit /A
                    break;

                case 4: //The card is not yet valid /A
                    break;

                case 5: //The card type is currently not accepted /A
                    break;

                case 6: //Filling with the same card has been detected indoor /A
                    break;

                case 7: //Filling with the same card has been detected outdoor /A
                    break;

                case 8: //Daily continget reached /A
                    break;

                case 9: //Indeterminate event cause /A
                    break;

                case 10: //The product is not allowed for the card /A
                    break;

                case 11: //event unused /
                    //messages.Add("event unused");
                    //eventType = CardEventType.None ;
                    break;

                case 12: //Payserv special: down option not allowed /-
                    //messages.Add("down option not allowed");
                    //eventType = CardEventType.None;
                    break;

                case 13: //The card is contained in the grey list /-
                    //messages.Add("The card is contained in the grey list");
                    //eventType = CardEventType.None;
                    break;

                case 14: //PIN tries exhausted /A
                    break;

                case 15: //Aborted by user or timeout /A
                    break;

                case 16: //card reader and TSM have been released /-
                    break;

                case 17: //PIN change is not allowed /-
                    break;

                case 18: //PIN change failed /-
                    break;

                case 19: //PIN change succeeded /-
                    break;

                case 20: //event unused /
                    break;

                case 21: //Receipt has been printed /
                    break;

                case 22: //Sale is requested from the POS /
                    break;

                case 23: //Timeout during pump selection /-
                    break;

                case 24: //Timeout during pump authorisation /-
                    break;

                case 25: //Timeout while waiting for nozzle hook off /-
                    break;

                case 26: //Timeout while waiting for fill end /-
                    break;

                case 27: //event unused /
                    break;

                case 28: //System error. Card has been returned. /A
                    break;

                case 29: //Ask cashier whether to skip PIN entry. /S
                    break;

                case 30: //PIN tries nearly exhausted /S
                    break;

                case 31: //Pump is in use or in error. /-
                    break;

                case 32: //Card process needs more time. /S
                    messages.Add("Karte wird verarbeitet.");
                    break;

                case 33: //Local Account Display Text /S
                    //messages.Add("Karte wird verarbeitet.");
                    //25-07-216af
                    string ms = (string)this._BaseCard.ServiceRequest((int)DT98BaseCardServiceRequestTypesEnum.DT98BCSRT_AccountDisplayText, null);
                    messages.Add(ms);
                    break;

                case 34: //Re-acquire terminal hardware /
                    //messages.Add("Re-acquire terminal hardware");
                    //eventType = CardEventType.None;
                    break;

                case 35: //Pump is not longer needed /-
                    //messages.Add("Pump is not longer needed");
                    //eventType = CardEventType.None;
                    break;

                case 36: //Outdoor payment not accepted /-
                    //messages.Add("Outdoor payment not accepted");
                    //eventType = CardEventType.None;
                    break;

                case 37: //Receipt printer is out of paper /S
                    messages.Add("Receipt printer is out of paper");
                    break;

                case 38: //Finalization booking failed /
                    messages.Add("Finalization booking failed");
                    break;

                case 39: //Problem in E-Funding detected /
                    messages.Add("Problem in E-Funding detected");//05-12-2016af
                    break;

                case 40: //Release pump and re-acquire terminal /-
                    //messages.Add("Release pump and re-acquire terminal");
                    //eventType = CardEventType.None;
                    break;

                case 41: //permanent card process has been established /-
                    //messages.Add("Permanent card process has been established");
                    //eventType = CardEventType.None;
                    break;

                case 42: //permanent card process shall be terminated /-
                    //messages.Add("Permanent card process shall be terminated");
                    //eventType = CardEventType.None;
                    break;

                case 43: //rebate card verified /-
                    //messages.Add("Rebate card verified");
                    //eventType = CardEventType.None;
                    break;

                case 44: //Aborted with receipt /A
                    messages.Add("Aborted with receipt");
                    break;

                case 45: //The processing of a NULL sale went OK /-
                    //messages.Add("The processing of a NULL sale went OK");
                    //eventType = CardEventType.None;
                    break;

                case 46: //fidelity card verified /-
                    //messages.Add("Fidelity card verified");
                    //eventType = CardEventType.None;
                    break;

                case 48: //Local Card Cashier Info Text /S
                    // messages.Add("Local Card Cashier Info Text");
                    //25-07-216af
                    string ms1 = (string)this._BaseCard.ServiceRequest((int)DT98BaseCardServiceRequestTypesEnum.DT98BCSRT_CardCashierText, null);
                    messages.Add(ms1);
                    break;

                case 49: //Split transaction refused /S
                    messages.Add("Split transaction refused");
                    break;

                case 50: //Indoor authorization may be enabled /-
                    //messages.Add("Indoor authorization may be enabled");
                    //eventType = CardEventType.None;
                    break;

                case 51: //Outdoor filling begun. Sent by PICU instead of CARDEVENT_OUTDOOR_RELEASE /-
                    //messages.Add("Outdoor filling begun. Sent by PICU instead of CARDEVENT_OUTDOOR_RELEASE");
                    //eventType = CardEventType.None;
                    break;

                case 52: //CARDEVENT_WL_REST_AMOUNT_WITH_CONFIRMATION   15-07-2015af
                    break;

                case 53: //CARDEVENT_WL_PAYIN_AMOUNT_WITH_CONFIRMATION   15-07-2015af
                    break;

                case 54: //CARDEVENT_WL_REST_AMOUNT_OHNE_CONFIRMATION   15-07-2015af
                    break;

                case 55: //CARDEVENT_WL_REST_AMOUNT_OHNE_CONFIRMATION   15-07-2015af
                    break;

                case 56: //CARDEVENT_WL_REST_AMOUNT_OHNE_CONFIRMATION   15-07-2015af
                    break;

                case 61:    //Aviaprepaid data
                    decimal actualAmount =
                        (decimal)this._BaseCard.ServiceRequest((int)DT98BaseCardServiceRequestTypesEnum.DT98BCSRT_AVIA_PREPAY_GetActualAmount, null);
                    decimal minAmount =
                        (decimal)this._BaseCard.ServiceRequest((int)DT98BaseCardServiceRequestTypesEnum.DT98BCSRT_AVIA_PREPAY_GetMinAmount, null);
                    decimal maxAmount =
                        (decimal)this._BaseCard.ServiceRequest((int)DT98BaseCardServiceRequestTypesEnum.DT98BCSRT_AVIA_PREPAY_GetMaxAmount, null);
                    string sPan =
                        (string)this._BaseCard.ServiceRequest((int)DT98BaseCardServiceRequestTypesEnum.DT98BCSRT_AVIA_PREPAY_GetPanData, null);

                    OnAviaPrepaidResponse?.Invoke(this, new() { ActualAmount = actualAmount, MinAmount = minAmount, MaxAmount = maxAmount, Pan = sPan });
                    break;

                case 62:    //Aviaprepaid data
                    actualAmount =
                       (decimal)this._BaseCard.ServiceRequest((int)DT98BaseCardServiceRequestTypesEnum.DT98BCSRT_AVIA_PREPAY_GetActualAmount, null);
                    sPan =
                       (string)this._BaseCard.ServiceRequest((int)DT98BaseCardServiceRequestTypesEnum.DT98BCSRT_AVIA_PREPAY_GetPanData, null);

                    OnAviaPrepaidResponse?.Invoke(this, new() { ActualAmount = actualAmount, MinAmount = -1, MaxAmount = -1, Pan = sPan });
                    break;

                default:
                    messages.Add("Unknown event");
                    break;
            }
        }

        private string AddItem(string terminalId, int ArticleId, decimal Amount)
        {
            try
            {
                Interop.DT98SalesSvrLib.IDT98SaleItem _saleItem = null;

                string _oIDSaleItem = string.Empty;
                string _oIDSale;

                if (_SalesSvr == null) return null;

                try { _oIDSale = _SalesSvr.Add(DateTime.Now, terminalId, "", "", "", 0, _Session); }
                catch { return null; }

                try { _SalesSvr.Sales().Item(_oIDSale).LockItem(_LockKey); }
                catch { return null; }

                _oIDSaleItem = _SalesSvr.AddEx(DateTime.Now, 0, terminalId, "1000", string.Empty
                    , ArticleId.ToString(), "Test Item", "Test Item", "receipt", 1, Amount, 0, 0, 0, 0, "50", "99", 1, 0, 0);

                try
                {
                    _oIDSaleItem = _SalesSvr.Attach(_LockKey, _oIDSale, _oIDSaleItem);
                }
                catch { return null; }

                _oIDSaleItem = _SalesSvr.AddEx(DateTime.Now, 0, terminalId, "1000", string.Empty
                    , "16", "Premium SuperPlus", "Premium SuperPlus", "receipt", 1, 1, 19, 0, 0, 0, "50", "99", 1, 0, 0);

                //try
                //{
                //    _oIDSaleItem = _SalesSvr.Attach(_LockKey, _oIDSale, _oIDSaleItem);
                //}
                //catch { return null; }

                //_oIDSaleItem = _SalesSvr.AddEx(DateTime.Now, 0, terminalId, "1000", string.Empty
                //     , "200028", "Zubehör", "Zubehör", "receipt", 1, -1, 19, 0, 0, 0, "50", "99", 1, 0, 0);

                //try
                //{
                //    _oIDSaleItem = _SalesSvr.Attach(_LockKey, _oIDSale, _oIDSaleItem);
                //}
                //catch { return null; }

                //_oIDSaleItem = _SalesSvr.AddEx(DateTime.Now, 0, terminalId, "1000", string.Empty
                //    , "5", "Biodiesel", "Biodiesel", "receipt", 3, 1, 19, 0, 0, 0, "50", "99", 1, 0, 0);

                //try
                //{
                //    _oIDSaleItem = _SalesSvr.Attach(_LockKey, _oIDSale, _oIDSaleItem);
                //}
                //catch { return null; }

                return _oIDSale;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion Private Methods

        #region Public Constructors

        //19-04-2016af
        /// <summary>
        /// Constructor for cash payment
        /// </summary>
        public PaymentWrapper()
        {
            if (_CardSvr == null)
            {
                //_CardSvr = (Interop.DT98CardFactoryLib.ICardCtl)
                // Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("ADBD542E-2DD6-11D2-B1B5-0080ADB725E0")));

                Type _cType = Type.GetTypeFromProgID("DT98CardFactory.CardCtl.1");
                //object o = Activator.CreateInstance(_cType);

                _CardSvr = new DT98CARDFACTORYLib.CardCtl();
            }

            Type _comType = Type.GetTypeFromProgID("DT98_ARTS_POSP.DT98Application");
            _PosAccessControl = (Interop.DT98_ARTS_InterfacesLib.IDT98_ARTS_PosAccessControl)Activator.CreateInstance(_comType);

            _SalesSvr = new Interop.DT98SalesSvrLib.DT98SalesSvrClass();

            _DBT98TextGenClass = new Interop.DT98TextSvrLib.DT98TextGenClass();

            _LockKey = new Random().Next(1, Int16.MaxValue);
        }

        #endregion Public Constructors

        #region Public Events

        public event Action<object, AviaPrepaidResponse> OnAviaPrepaidResponse;

        public event Action<object, string> OnCardNotify;

        public event Action<object, string> OnCashierNotify;

        #endregion Public Events

        #region Public Methods

        public bool Login(int TerminalID)
        {
            lock (this)
            {
                try
                {
                    if (!_PosAccessControl.ValidOperatorPwd(1, 1000, null)) return false;

                    bool _isFirstSession = _PosAccessControl.IsFirstSession(1, 1000);

                    if (_isFirstSession)
                    {
                        object oMissing = System.Reflection.Missing.Value;

                        ADODB.Recordset _tillOpenBalances = new ADODB.Recordset();

                        _tillOpenBalances.Fields.Append("TenderType", ADODB.DataTypeEnum.adBSTR, 20, ADODB.FieldAttributeEnum.adFldFixed, null);
                        _tillOpenBalances.Fields.Append("MediaUnits", ADODB.DataTypeEnum.adCurrency, 20, ADODB.FieldAttributeEnum.adFldFixed, null);
                        _tillOpenBalances.Fields.Append("Amount", ADODB.DataTypeEnum.adCurrency, 20, ADODB.FieldAttributeEnum.adFldFixed, null);

                        _tillOpenBalances.Open(oMissing, oMissing, ADODB.CursorTypeEnum.adOpenUnspecified, ADODB.LockTypeEnum.adLockUnspecified, 0);

                        //TODO hjs:	Add a new record for each tender type
                        _tillOpenBalances.AddNew(oMissing, oMissing);
                        _tillOpenBalances.Fields["TenderType"].Value = "CASH_INDOOR";
                        _tillOpenBalances.Fields["MediaUnits"].Value = 0;
                        _tillOpenBalances.Fields["Amount"].Value = 0;					//TODO hjs:	Need amount from outside
                        _tillOpenBalances.Update(oMissing, oMissing);

                        _Session = (IDT98_ARTS_Session)_PosAccessControl.StartSessionEx(/*iSession.StationID*/ 1 /*13-05-2016af*/,
                                                                                           TerminalID,
                                                                                           1000,
                                                                                           null,
                                                                                           _tillOpenBalances);
                    }
                    else
                    {
                        _Session = (IDT98_ARTS_Session)_PosAccessControl.StartSession(1, TerminalID, 1000, null);
                    }
                    if (_Session == null) return false;

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool DeleteSale(string saleOid) //08-08-2018af
        {
            try
            {
                if (_SalesSvr == null)
                    return false;
                _SalesSvr.Delete(_LockKey, saleOid);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CreateSale(int TerminalID)
        {
            try
            {
                string _oIDSale = null;

                if (_SalesSvr == null)
                    return false;

                try { _oIDSale = _SalesSvr.Add(DateTime.Now, TerminalID.ToString(), "", "", string.Empty, 0, _Session); }
                catch { return false; }

                try { _SalesSvr.Sales().Item(_oIDSale).LockItem(_LockKey); }
                catch { return false; }

                return true;
            }
            catch (Exception ex)
            {
                //Log(SeverityDef.Error, ex.Message);
                return false;
            }
        }

        public string GetReceiptText(string Track1, string Track2, string Track3, string Chip, string TerminalId, string receiptOid)
        {
            try
            {
                if (Track1 != string.Empty)
                    track1 = string.Format("#{0}", Track1);
                if (Track2 != string.Empty)
                    track2 = string.Format("#{0}", Track2);
                if (Track3 != string.Empty)
                    track3 = string.Format("#{0}", Track3);
                if (Chip != string.Empty)
                    chip = string.Format("#{0}", Chip);
                error = 0;

                try { _BaseCard = (BaseCard)_CardSvr.CardIdentify(TerminalId, track1, track2, track3, chip, error); }
                catch { return string.Empty; }

                try
                {
                    _BaseCard.ServiceRequest((int)DT98BaseCardServiceRequestTypesEnum.DT98BCSRT_PutCashierName, _Session.GetOperatorName());
                }
                catch (Exception ex)
                {
                }

                return
                    _BaseCard.GenerateReceipt(46,/*Indoor*/ 1, receiptOid, string.Empty, 2, 0)
                    + Environment.NewLine + Environment.NewLine +
                    _BaseCard.GenerateReceipt(46,/*Indoor*/ 1, receiptOid, string.Empty, 1, 0);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public bool Payment(string track1, string track2, string track3, string chip, string TerminalId, bool onlineCard, int ArticleId, decimal Amount, object zvt)
        {
            try
            {
                string saleOid = AddItem(TerminalId, ArticleId, Amount);

                OnAviaPrepaidResponse?.Invoke(this, new() { SaleOid = saleOid });

                if (saleOid == null)
                {
                    return false;
                }

                bool BalanceOrPrepay = false;
                if (track1 == "AVIA_PREPAID_ACCOUNTBALANCE" || track1 == "AVIA_PREPAID_PREPAID")
                {
                    BalanceOrPrepay = true;
                }
                else if (!string.IsNullOrEmpty(track2) && string.IsNullOrEmpty(track1) && track2 != "EMV=")
                {
                    if (track1 != "Payment")
                    {
                        track1 = "AVIA_PREPAID_SPLITPAYMENT";
                    }
                }
                else if (track2 == "EMV=")
                {
                    track2 = "";
                    track3 = "EMV=";
                }
                else if (string.IsNullOrEmpty(track3))
                {
                    track3 = track2;
                }

                if (track1 != string.Empty)
                    track1 = string.Format("#{0}", track1);
                if (track2 != string.Empty)
                    track2 = string.Format("#{0}", track2);
                if (track3 != string.Empty)
                    track3 = string.Format("#{0}", track3);
                if (chip != string.Empty)
                    chip = string.Format("#{0}", chip);
                error = 0;

                try
                {
                    _BaseCard = (BaseCard)_CardSvr.CardIdentify(TerminalId, track1, track2, track3, chip, error);
                }
                catch
                {
                    throw new Exception("CardSvr.CardIdentify Failed"); //21-04-2016af
                    //return false;
                }

                if (_BaseCard == null)
                {
                    throw new Exception("__BaseCard == null Failed"); //21-04-2016af
                }

                _BaseCard.Notify += new Interop.DT98BaseCardLib.DIBaseCardEvent_NotifyEventHandler(_BaseCard_Notify);

                //19-05-2015af
                _BaseCard.DisplayCashierText += new Interop.DT98BaseCardLib.DIBaseCardEvent_DisplayCashierTextEventHandler(_BaseCard_DisplayCashierText);

                _BaseCard.CardLanguage = 0;

                _BaseCard.track1 = track1;
                _BaseCard.track2 = track2;
                _BaseCard.track3 = track3;
                _BaseCard.chip = chip;

                _BaseCard.LockKey = _LockKey;
                _BaseCard.SaleOID = saleOid;
                _BaseCard.CashierSession = _Session;
                _BaseCard.TerminalType = 1 /*Indoor*/;

                try
                {
                    _BaseCard.ZVT = zvt;

                    //20-03-2015af Online card payment
                    if (onlineCard)
                    {
                        _BaseCard.ServiceRequest((int)DT98BaseCardServiceRequestTypesEnum.DT98BCSRT_CardPaymentWithRead, null);
                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    _BaseCard.ServiceRequest((int)DT98BaseCardServiceRequestTypesEnum.DT98BCSRT_PutCashierName, _Session.GetOperatorName());
                }
                catch (Exception ex)
                {
                }

                if (BalanceOrPrepay)
                {
                    _BaseCard.ServiceRequest(61, track3);
                }

                try { _BaseCard.CardVerify(); }
                catch (Exception ex)
                {
                    throw new Exception("_BaseCard.CardVerify() Failed"); //21-04-2016af
                    //return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion Public Methods
    }
}