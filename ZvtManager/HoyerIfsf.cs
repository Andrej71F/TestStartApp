using DTPCHostInterfaceLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace TaskStar.ZvtTest.ZvtManager
{
    public enum HoyerCardTypeEnum
    {
        CommonCard,

        PersonCard,

        DriveCard
    }

    public class CardData
    {
        #region Public Fields

        public HoyerCardTypeEnum HoyerCardType;

        public string Pan;

        public string Track2Data;

        public int Pin;

        #endregion Public Fields
    }

    public class HoyerIfsf
    {
        #region Private Fields

        private ICollection<CardData> _cardData;

        #endregion Private Fields

        #region Private Methods

        private ICollection<CardData> InitData()
        {
            return new List<CardData>()
            {
                new() {
                    HoyerCardType = HoyerCardTypeEnum.CommonCard,
                    Pan = "7005500000004475239",
                    Track2Data = "7005500000004475239=27649122891137198",
                    Pin = 2008
                },

                new (){
                    HoyerCardType = HoyerCardTypeEnum.CommonCard,
                    Pan = "7005500000004475247",
                    Track2Data = "7005500000004475247 = 27649127572115055",
                    Pin = 2009
                },

                new() {
                    HoyerCardType = HoyerCardTypeEnum.CommonCard,
                    Pan = "7005500000004475254",
                    Track2Data = "7005500000004475254 = 27649124810123025",
                    Pin = 2010
                },

                new() {
                    HoyerCardType = HoyerCardTypeEnum.CommonCard,
                    Pan = "7005500000004475262",
                    Track2Data = "7005500000004475262 = 27649122215134097",
                    Pin = 2011
                },

                new() {
                    HoyerCardType = HoyerCardTypeEnum.PersonCard,
                    Pan = "7005500000004475270",
                    Track2Data = "7005500000004475270 = 27649124331140023",
                    Pin = 2012
                },

                new() {
                    HoyerCardType = HoyerCardTypeEnum.DriveCard,
                    Pan = "7005500000004475288",
                    Track2Data = "7005500000004475288 = 27649125557537120",
                    Pin = 2013
                }
            };
        }

        #endregion Private Methods

        #region Public Constructors

        public HoyerIfsf()
        {
            //DTPCHostInterfaceLib.IDTPCHostCardData103
            //DTPCHostInterfaceLib.IDTPCHostProductDataSet103
            //DTPCHostInterfaceLib.IDTPCHostProductData103
            //DTPCHostInterfaceLib.IDTPCHostAdministrativeData103
            //DTPCHostInterfaceLib.IDTPCHostCardDataMaintenance103
        }

        #endregion Public Constructors

        #region Public Properties

        public ICollection<CardData> CardData
        {
            get
            {
                if (_cardData == null)
                {
                    _cardData = InitData();
                }

                return _cardData;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void RetrieveCardAttribute(string Pan, int site, int appId, int terminalType, int terminalId)
        {
            CardData cardData = CardData.FirstOrDefault(c => c.Pan.Trim() == Pan.Split(',')[0].Trim());

            DTPCHostInterfaceLib.IDTPCHostObjFactory101 spObjFact
                = (DTPCHostInterfaceLib.IDTPCHostObjFactory101)
                Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("5ba62096-ba5d-11d6-a34b-0080ad733d20")));

            DTPCHostInterfaceLib.IDTPCHostCardData103 m_oWLCardDataPtr
                = (DTPCHostInterfaceLib.IDTPCHostCardData103)
                Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("5ba62097-ba5d-11d6-a34b-0080ad733d20")));

            m_oWLCardDataPtr.Site = site;
            m_oWLCardDataPtr.PAN = cardData.Pan;
            m_oWLCardDataPtr.PANIntern = cardData.Pan.Substring(0, cardData.Pan.Length - 1);
            m_oWLCardDataPtr.Track2 = cardData.Track2Data;
            m_oWLCardDataPtr.TerminalId = terminalId;
            m_oWLCardDataPtr.TerminalType = terminalType;

            m_oWLCardDataPtr.AuthorizationRequestData.BatchNumber = 1;
            m_oWLCardDataPtr.LocalDate = Convert.ToInt32($"{DateTime.Now:yyyyMMdd}");   // trnsactions Date YYYYMMDD
            m_oWLCardDataPtr.LocalTime = Convert.ToInt32($"{DateTime.Now:yyyyMMdd}");   // transactions Time HHMMSS
            m_oWLCardDataPtr.CurrencyId = 978;
            m_oWLCardDataPtr.TransactionPinRetry = 0;
            m_oWLCardDataPtr.RequestType = (int)EDTPCHostRequestTypes.DTPCHOST_REQTYPE_CARDINFO;

            DTPCHostInterfaceLib.IDTPCHostCardChecker101 spChecker =
                spObjFact.HandlerFromTypeId((int)DTPCHostInterfaceLib.EDTPCHostHandlerTypes.DTPCHOST_HNDTYPE_CHECKER, appId);
        }

        public bool Pay(string track1, string track2, string track3, string chip, string TerminalId, bool onlineCard, int ArticleId, decimal Amount)
        {
            DTPCHostInterfaceLib.IDTPCHostObjFactory101 spObjFact
                = (DTPCHostInterfaceLib.IDTPCHostObjFactory101)
                   Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("5ba62096-ba5d-11d6-a34b-0080ad733d20")));

            DTPCHostInterfaceLib.IDTPCHostCardData103 cardData
                = (DTPCHostInterfaceLib.IDTPCHostCardData103)
                    Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("5ba62097-ba5d-11d6-a34b-0080ad733d20")));

            DTPCHostInterfaceLib.IDTPCHostCardChecker101 spChecker =
                spObjFact.HandlerFromTypeId((int)DTPCHostInterfaceLib.EDTPCHostHandlerTypes.DTPCHOST_HNDTYPE_CHECKER, 7);

            DTPCHostInterfaceLib.IDTPCHostTransaction101 spTrx =
             spObjFact.HandlerFromTypeId((int)DTPCHostInterfaceLib.EDTPCHostHandlerTypes.DTPCHOST_HNDTYPE_TRANSACTION, 7);

            cardData.PAN = track2.Split(" = ")[0]; // "927600362000001001";
            cardData.Track2 = track2;
            cardData.Site = 123412345;
            cardData.TerminalId = Convert.ToInt32(TerminalId);
            cardData.PIN = Convert.ToInt32(track2.Substring(14, 4));
            cardData.TerminalType = 1;
            cardData.TransactionAmount = (int)Amount;

            cardData.RequestType = (int)DTPCHostInterfaceLib.EDTPCHostRequestTypes.DTPCHOST_REQTYPE_AUTHORIZE;
            //1200 DTPCHOST_REQTYPE_AUTHORIZE
            cardData.AuthorizationRequestData.BatchNumber = 1;
            cardData.LocalDate = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            cardData.LocalTime = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
            cardData.CurrencyId = 978;
            cardData.TransactionPinRetry = 0;

            DTPCHostInterfaceLib.IDTPCHostProductData103 productData
                = (DTPCHostInterfaceLib.IDTPCHostProductData103)
                    Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("137f9228-1adf-11df-afdc-001cc02f6f8c")));

            productData.Amount = productData.Quantity = (int)Amount;
            productData.UnitOfMeasure = 0; //
            productData.ProductCode = 2; // ArticleId;
            productData.UnitPrice = 1;
            productData.VatPercent = 19;
            cardData.ProductData.Add(productData);

            //int result = spChecker.Validate(cardData);

            cardData.RequestType = (int)DTPCHostInterfaceLib.EDTPCHostRequestTypes.DTPCHOST_REQTYPE_FINALIZE; //1200
            int result = spTrx.Finalize(cardData);

            //Validate DTPCHOST_REQTYPE_AUTHORIZE -> 1100 vorautorisierung
            //Finalise DTPCHOST_REQTYPE_FINALIZE for outdoor-> 1220 storno vorautorisierung
            //Finalise DTPCHOST_REQTYPE_REVERS-> 1420 storno

            //Web Interface User / yVp3NAk5SLGH  http://10.2.181.1/
            return true;
        }

        #endregion Public Methods
    }
}