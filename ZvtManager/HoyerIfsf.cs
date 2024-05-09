using System;

namespace TaskStar.ZvtTest.ZvtManager
{
    public class HoyerIfsf
    {
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

        #region Public Methods

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

            cardData.PAN = track2.Split("=")[0]; // "927600362000001001";
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