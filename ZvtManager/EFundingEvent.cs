using Interop.DT98BaseDevicesLib;
using Interop.TOMFundingIfaceLib;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace TaskStar.ZvtTest.ZvtManager
{
    public class EFundingEvent : BaseEvent, IBaseEFundingEvent
    {
        #region Private Methods

        private int HandleEventcode(int value, int commandCode)
        {
            switch (value)
            {
                case (int)TOMEFundingEventCodes.TOM_EFEC_SUCCESS:
                    switch (commandCode)
                    {
                        case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_PREAUTHORISE:
                            return 65;

                        default:
                            return 64;
                    }

                case (int)TOMEFundingEventCodes.TOM_EFEC_HOST:
                    return 91;

                case (int)TOMEFundingEventCodes.TOM_EFEC_READ_ERROR:
                    return 92;

                case (int)TOMEFundingEventCodes.TOM_EFEC_NO_CARD_OR_OPT_DATA:
                    return 93;

                case (int)TOMEFundingEventCodes.TOM_EFEC_PROCESSING_ERROR:
                    return 94;

                case (int)TOMEFundingEventCodes.TOM_EFEC_NOT_AVAILABLE_EC:
                    return 95;

                case (int)TOMEFundingEventCodes.TOM_EFEC_NOT_AVAILABLE_CC:
                    return 96;

                case (int)TOMEFundingEventCodes.TOM_EFEC_TRANSACTION_BUFFER_FULL:
                    return 97;

                case (int)TOMEFundingEventCodes.TOM_EFEC_DEACTIVATED:
                    return 98;

                case (int)TOMEFundingEventCodes.TOM_EFEC_TIMEOUT_OR_ABORT:
                    return 67;

                case (int)TOMEFundingEventCodes.TOM_EFEC_CARD_BLACKLISTED:
                    return 100;

                case (int)TOMEFundingEventCodes.TOM_EFEC_WRONG_CURRENCY:
                    return 101;

                case (int)TOMEFundingEventCodes.TOM_EFEC_CREDIT_INSUFFICIENT:
                    return 102;

                case (int)TOMEFundingEventCodes.TOM_EFEC_CARD_READ_ERROR:
                    return 103;

                case (int)TOMEFundingEventCodes.TOM_EFEC_RECONCILIATION_NOT_POSSIBLE:
                    return 104;

                case (int)TOMEFundingEventCodes.TOM_EFEC_COMMUNICATION_ERROR:
                    return 105;

                case (int)TOMEFundingEventCodes.TOM_EFEC_NOT_APPLICABLE:
                    return 106;

                case (int)TOMEFundingEventCodes.TOM_EFEC_PINPAD_OUT_OF_ORDER:
                    return 107;

                case (int)TOMEFundingEventCodes.TOM_EFEC_TRANSMISSION_ERROR:
                    return 108;

                case (int)TOMEFundingEventCodes.TOM_EFEC_CONNECTION_ERROR:
                    return 109;

                case (int)TOMEFundingEventCodes.TOM_EFEC_BUSY:
                    return 110;

                case (int)TOMEFundingEventCodes.TOM_EFEC_RECEIVER_NOT_READY:
                    return 111;

                case (int)TOMEFundingEventCodes.TOM_EFEC_NO_ANSWER:
                    return 112;

                case (int)TOMEFundingEventCodes.TOM_EFEC_NO_LINE:
                    return 113;

                case (int)TOMEFundingEventCodes.TOM_EFEC_NOT_AVAILABLE_PAYCARD:
                    return 114;

                case (int)TOMEFundingEventCodes.TOM_EFEC_MEMORY_FULL:
                    return 115;

                case (int)TOMEFundingEventCodes.TOM_EFEC_ALREADY_REVERSED:
                    return 116;

                case (int)TOMEFundingEventCodes.TOM_EFEC_REVERSAL_NOT_POSSIBLE:
                    return 117;

                case (int)TOMEFundingEventCodes.TOM_EFEC_PREAUTH_WRONG:
                    return 118;

                case (int)TOMEFundingEventCodes.TOM_EFEC_PREAUTH_ERROR:
                    return 119;

                case (int)TOMEFundingEventCodes.TOM_EFEC_LOW_VOLTAGE:
                    return 120;

                case (int)TOMEFundingEventCodes.TOM_EFEC_SHUTTER_DEFECT:
                    return 121;

                case (int)TOMEFundingEventCodes.TOM_EFEC_DEALER_CARD_LOCK:
                    return 122;

                case (int)TOMEFundingEventCodes.TOM_EFEC_DIAG_REQUIRED:
                    return 123;

                case (int)TOMEFundingEventCodes.TOM_EFEC_MAX_AMOUNT_EXCEEDED:
                    return 124;

                case (int)TOMEFundingEventCodes.TOM_EFEC_AD_CARD_PROFILES:
                    return 125;

                case (int)TOMEFundingEventCodes.TOM_EFEC_PAYMENT_PROCEDURE_NOT_SUPPORTED:
                    return 126;

                case (int)TOMEFundingEventCodes.TOM_EFEC_CURRENCY_NOT_APPLICABLE:
                    return 127;

                case (int)TOMEFundingEventCodes.TOM_EFEC_AMOUNT_TOO_SMALL:
                    return 128;

                case (int)TOMEFundingEventCodes.TOM_EFEC_MAX_AMOUNT_TOO_SMALL:
                    return 129;

                case (int)TOMEFundingEventCodes.TOM_EFEC_APPLICABLE_ONLY_FOR_EURO:
                    return 130;

                case (int)TOMEFundingEventCodes.TOM_EFEC_PRINTER_NOT_READY:
                    return 131;

                case (int)TOMEFundingEventCodes.TOM_EFEC_CARD_INSERTED:
                    return 132;

                case (int)TOMEFundingEventCodes.TOM_EFEC_CARD_EJECT_ERROR:
                    return 133;

                case (int)TOMEFundingEventCodes.TOM_EFEC_CARD_CAPTURE_ERROR:
                    return 134;

                case (int)TOMEFundingEventCodes.TOM_EFEC_CARD_READER_DEFECT:
                    return 135;

                case (int)TOMEFundingEventCodes.TOM_EFEC_PRODUCT_GROUP_NOT_ALLOWED:
                    return 136;

                case (int)TOMEFundingEventCodes.TOM_EFEC_NO_PRODUCT_GROUP_TABLE:
                    return 137;

                case (int)TOMEFundingEventCodes.TOM_EFEC_WRONG_MAC:
                    return 138;

                case (int)TOMEFundingEventCodes.TOM_EFEC_CARD_CODE_NOT_ALLOWED:
                    return 139;

                case (int)TOMEFundingEventCodes.TOM_EFEC_UNKNOWN_PIN_ALGORITHM:
                    return 140;

                case (int)TOMEFundingEventCodes.TOM_EFEC_PIN_PROCESSING_NOT_POSSIBLE:
                    return 141;

                case (int)TOMEFundingEventCodes.TOM_EFEC_TOM_EFEC_PINPAD_OUT_OF_ORDER_2:
                    return 142;

                case (int)TOMEFundingEventCodes.TOM_EFEC_RECONCILIATION_NOT_FINISHED:
                    return 143;

                case (int)TOMEFundingEventCodes.TOM_EFEC_EC_OFFLINE_ERROR:
                    return 144;

                case (int)TOMEFundingEventCodes.TOM_EFEC_OPT_ERROR:
                    return 145;

                case (int)TOMEFundingEventCodes.TOM_EFEC_NO_OPT_DATA:
                    return 146;

                case (int)TOMEFundingEventCodes.TOM_EFEC_CLEARING_ERROR:
                    return 147;

                case (int)TOMEFundingEventCodes.TOM_EFEC_TRANSACTION_DATA_DAMAGED:
                    return 148;

                case (int)TOMEFundingEventCodes.TOM_EFEC_DEVICE_FAILURE:
                    return 149;

                case (int)TOMEFundingEventCodes.TOM_EFEC_BAUD_RATE_NOT_SUPPORTED:
                    return 150;

                case (int)TOMEFundingEventCodes.TOM_EFEC_REGISTER_UNKNOWN:
                    return 151;

                case (int)TOMEFundingEventCodes.TOM_EFEC_SYSTEM_ERROR:
                    return 67;

                default:
                    Log.LogWarning($"received unexpected TOMEFundingEventCode: {value}");
                    return 160;
            }
        }

        private void HandleIStatusCode(int value)
        {
            switch (value)
            {
                case 27:
                    break;

                case 255:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_CONFIRM_AMOUNT:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_ENTER_PIN_1:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_ENTER_PIN_2:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_WAIT_HOST:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_AUTO_REVERSAL:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_SEND_STORED_TRANSACTIONS:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_CARD_REJECTED:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_CARD_UNKNOWN:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_CARD_EXPIRED:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_CARD_INSERT:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_CARD_REMOVE:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_CARD_READ_ERROR:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_BUSY:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_AUTO_RECONCILIATION:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_SHOW_ASSETS:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_ASSETS_INSUFFUICIENT:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_WRONG_PIN:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_LIMIT_EXCEEDED:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_PLEASE_WAIT:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_SERVICE_MODE:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_ENTER_PHONE_NUMBER:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_REPEAT_PHONE_NUMBER:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_ENTER_KM_1:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_CASHIER_CARD_INSERT:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_AUTO_DIAGNOSIS:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_AUTO_INITIALISATION:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_HOST_CONNECTION_START:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_HOST_CONNECTION_ESTABLISHED:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_ENTER_KM_2:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_TERMINAL_OFFLINE:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_TERMINAL_ONLINE:
                    break;

                case (int)TOMEFundingInterStatusCodes.TOM_EFISC_TERMINAL_OFFLINE_TRANSACTION:
                    break;
            }
        }

        private void HandleFinancialAdviceResult(ITOMEFundingIndication Info, int lCommandCode)
        {
            switch (Info.IndicationType)
            {
                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_SUCCESS:          // 0x0001 (SUCCESS / Operation completed successfully)
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_DATA:             // 0x0006 (DATA / Funding data transfer)
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_DATA_PRINT:
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_INTERMEDIATE:     // intermediate status message
                    HandleIStatusCode(Info.EventCode);
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_ABORTED:          // 0x0004 (ABORTED / Operation aborted, e. g. by user)
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_FAILED:           // error
                    break;

                default:
                    Log.LogWarning($"received unexpected indication type {Info.IndicationType}");
                    break;
            }
        }

        private void HandleCardPaymentResult(ITOMEFundingIndication Info, int lCommandCode)
        {
            switch (Info.IndicationType)
            {
                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_SUCCESS:          // success
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_DATA:             // funding data
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_DATA_PRINT:
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_INTERMEDIATE:     // intermediate status message
                    HandleIStatusCode(Info.EventCode);
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_ABORTED:          // abort condition detected
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_FAILED:           // error
                default:
                    Log.LogWarning($"received unexpected indication type {Info.IndicationType}");
                    break;
            }
        }

        private void HandleRebateFidelityResult(ITOMEFundingIndication Info, int lCommandCode)
        {
            switch (Info.IndicationType)
            {
                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_SUCCESS:          // success
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_DATA:             // funding data

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_DATA_PRINT:
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_INTERMEDIATE:     // intermediate status message
                    HandleIStatusCode(Info.EventCode);
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_ABORTED:          // abort condition detected
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_FAILED:           // error
                    break;

                default:
                    Log.LogWarning($"received unexpected indication type {Info.IndicationType}");
                    break;
            }
        }

        private void HandleGetOpenBookingResult(ITOMEFundingIndication Info, int lCommandCode)
        {
            switch (Info.IndicationType)
            {
                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_SUCCESS:          // success
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_DATA:             // funding data
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_INTERMEDIATE:     // intermediate status message
                    HandleIStatusCode(Info.EventCode);
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_ABORTED:          // abort condition detected
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_FAILED:           // error
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_UNKNOWN:
                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_PARTIAL_SUCCESS:
                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_DATA_PRINT:
                    Log.LogWarning($"received unimplemented indication type {Info.IndicationType}");
                    break;

                default:
                    Log.LogWarning($"received unexpected indication type {Info.IndicationType}");
                    break;
            }
        }

        private void HandlePreAuthoriseResult(ITOMEFundingIndication Info, int lCommandCode)
        {
            int lIndicationType = Info.IndicationType;
            switch (lIndicationType)
            {
                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_SUCCESS:          // success
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_DATA:             // 0x0006 (DATA / Funding data transfer)
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_DATA_PRINT:
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_INTERMEDIATE:     // 0x0005 (INTERMEDIATE / Intermediate state indication)
                    HandleIStatusCode(Info.EventCode);
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_ABORTED:          // 0x0004 (ABORTED / Operation aborted, e. g. by user)
                    break;

                case (int)TOMEFundingIndicationTypesEnum.TOM_EFIT_FAILED:           // error
                    break;

                default:
                    Log.LogWarning($"received unexpected indication type {Info.IndicationType}");
                    break;
            }
        }

        private void HandleEfundingResult(ITOMEFundingIndication Info)
        {
            int lIndicationType = Info.IndicationType;
            int lCommandCode = Info.CommandCode;

            MethodBase m = MethodBase.GetCurrentMethod();
            Log.LogInformation($"{m.Name}: ");

            switch (lCommandCode)
            {
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_PREAUTHORISE:
                    HandlePreAuthoriseResult(Info, lCommandCode);
                    break;

                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_FINANCIALADVICE:
                    HandleFinancialAdviceResult(Info, lCommandCode);
                    break;

                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_CARD_PAYMENT:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_PAYMENT_REFUND:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_REVERSAL:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_GET_LOAD_PREPAYCARD:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_ACCOUNT_BALANCE_REQUEST:
                    HandleCardPaymentResult(Info, lCommandCode);
                    break;

                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_FIDELITY_DISCOUNTING:
                    HandleRebateFidelityResult(Info, lCommandCode);
                    break;

                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_GET_OPEN_BOOKING:
                    HandleGetOpenBookingResult(Info, lCommandCode);
                    break;

                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_UNKNOWN:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_TICKET_REPRINT:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_CHANGE_LANGUAGE:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_CARD_PAYMENT_READ_CARD:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_LOGIN:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_LOGOFF:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_INITIALISATION:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_DIAGNOSIS:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_CHANGE_MODE:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_SOFTWARE_UPDATE:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_SET_DATE_TIME:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_RECONCILIATION:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_GET_SALES_RECORDS:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_OPT_START_ACTION:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_OPT_POINT_OF_TIME:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_OPT_PRE_INIT:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_OPT_GET_DATA:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_RESET_TERMINAL:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_SOFTWARE_RESET:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_SERVICE_FUNCTION:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_MAINTAINANCE_FUNCTION:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_PRINT_INFO_RECEIPT:
                case (int)TOMEFundingCommandCodesEnum.TOM_EFCC_READ_REGISTER:
                    Log.LogError($"received unimplemented command code {lCommandCode:X8}");
                    break;

                default:
                    Log.LogError($"received unknow command code {lCommandCode:X8}");
                    break;
            }
        }

        private string DumpShort(string indent, ITOMEFundingIndication Info)
        {
            string _indent = indent ?? string.Empty;

            string _dump = string.Empty;
            if (Info != null)
            {
                _dump += Environment.NewLine + _indent + "Name                      : " + Info.Name;
                //_dump += Environment.NewLine + _indent + "Interpretation            : " + Info.Interpretation;
                _dump += Environment.NewLine + _indent + "Count                     : " + Info.Count;
                _dump += Environment.NewLine + _indent + "IndicationType            : " + Info.IndicationType;
                _dump += Environment.NewLine + _indent + "CommandCode               : " + Info.CommandCode;
                _dump += Environment.NewLine + _indent + "EventCode                 : " + Info.EventCode;
                _dump += Environment.NewLine + _indent + "EventDescription          : " + Info.EventDescription;
            }
            else
            {
                _dump += Environment.NewLine + _indent + "No Info data available";
            }
            return _dump;
        }

        private string Dump(string indent, ITOMEFundingIndication Info)
        {
            string _indent = indent ?? string.Empty;

            string _dump = string.Empty;
            if (Info != null)
            {
                _dump += Environment.NewLine + _indent + "Name                      : " + Info.Name;
                _dump += Environment.NewLine + _indent + "Version                   : " + Info.Version;
               // _dump += Environment.NewLine + _indent + "Interpretation            : " + Info.Interpretation;
                _dump += Environment.NewLine + _indent + "Count                     : " + Info.Count;
                _dump += Environment.NewLine + _indent + "IndicationType            : " + Info.IndicationType;
                _dump += Environment.NewLine + _indent + "CommandCode               : " + Info.CommandCode;
                _dump += Environment.NewLine + _indent + "EventCode                 : " + Info.EventCode;
                _dump += Environment.NewLine + _indent + "EventDescription          : " + Info.EventDescription;
                _dump += Environment.NewLine + _indent + "HostCode                  : " + Info.HostCode;
                _dump += Environment.NewLine + _indent + "HostCodeDescription       : " + Info.HostCodeDescription;
                _dump += Environment.NewLine + _indent + "ApplicationSender         : " + Info.ApplicationSender;
                _dump += Environment.NewLine + _indent + "WorkStationID             : " + Info.WorkStationID;
                _dump += Environment.NewLine + _indent + "POPID                     : " + Info.POPID;
                _dump += Environment.NewLine + _indent + "RequestID                 : " + Info.RequestID;
                _dump += Environment.NewLine + _indent + "TerminalID                : " + Info.TerminalID;
                _dump += Environment.NewLine + _indent + "TerminalBatch             : " + Info.TerminalBatch;
                _dump += Environment.NewLine + _indent + "STAN                      : " + Info.STAN;
                _dump += Environment.NewLine + _indent + "TerminalReferenceID       : " + Info.TerminalReferenceID;
                _dump += Environment.NewLine + _indent + "CardEntryMode             : " + Info.CardEntryMode;
                _dump += Environment.NewLine + _indent + "PrintVAT                  : " + Info.PrintVAT;
                _dump += Environment.NewLine + _indent + "TotalAmount               : " + Info.TotalAmount;
                _dump += Environment.NewLine + _indent + "PaymentAmount             : " + Info.PaymentAmount;
                _dump += Environment.NewLine + _indent + "CashBackAmount            : " + Info.CashBackAmount;
                _dump += Environment.NewLine + _indent + "CurrencyCode              : " + Info.CurrencyCode;
                _dump += Environment.NewLine + _indent + "AcquirerID                : " + Info.AcquirerID;
                _dump += Environment.NewLine + _indent + "CardPAN                   : " + Info.CardPAN;
                _dump += Environment.NewLine + _indent + "CardPANPrint              : " + Info.CardPANPrint;
                _dump += Environment.NewLine + _indent + "TerminalTimeStamp         : " + Info.TerminalTimeStamp;
                _dump += Environment.NewLine + _indent + "ApprovalCode              : " + Info.ApprovalCode;
                _dump += Environment.NewLine + _indent + "CardCircuit               : " + Info.CardCircuit;
                _dump += Environment.NewLine + _indent + "RestrictionCodes          : " + Info.RestrictionCodes;
                _dump += Environment.NewLine + _indent + "Track1                    : " + Info.Track1;
                _dump += Environment.NewLine + _indent + "Track2                    : " + Info.Track2;
                _dump += Environment.NewLine + _indent + "Track3                    : " + Info.Track3;
                _dump += Environment.NewLine + _indent + "ReceiptData               : " + Info.ReceiptData;
                _dump += Environment.NewLine + _indent + "LanguageCode              : " + Info.LanguageCode;
                _dump += Environment.NewLine + _indent + "PaymentType               : " + Info.PaymentType;
                _dump += Environment.NewLine + _indent + "Acquirer                  : " + Info.Acquirer;
                _dump += Environment.NewLine + _indent + "PINTransaction            : " + Info.PINTransaction;
                _dump += Environment.NewLine + _indent + "OnlineTransaction         : " + Info.OnlineTransaction;
                _dump += Environment.NewLine + _indent + "RestrictionCodeInversion  : " + Info.RestrictionCodeInversion;
                _dump += Environment.NewLine + _indent + "BankInstituteCode         : " + Info.BankInstituteCode;
                _dump += Environment.NewLine + _indent + "BankAccountNumber         : " + Info.BankAccountNumber;
                _dump += Environment.NewLine + _indent + "ExpirationDate            : " + Info.ExpirationDate;
                _dump += Environment.NewLine + _indent + "AcquirerIDParam           : " + Info.AcquirerIDParam;
                _dump += Environment.NewLine + _indent + "OriginalSTAN              : " + Info.OriginalSTAN;
                _dump += Environment.NewLine + _indent + "ApprovalCodeAttribute     : " + Info.ApprovalCodeAttribute;
                _dump += Environment.NewLine + _indent + "ContractNumber            : " + Info.ContractNumber;
                _dump += Environment.NewLine + _indent + "ContractAddition          : " + Info.ContractAddition;
                _dump += Environment.NewLine + _indent + "TurnoverNumber            : " + Info.TurnoverNumber;
                _dump += Environment.NewLine + _indent + "CardSequenceNumber        : " + Info.CardSequenceNumber;
                _dump += Environment.NewLine + _indent + "CardType                  : " + Info.CardType;
                _dump += Environment.NewLine + _indent + "CardTypeID                : " + Info.CardTypeID;
                _dump += Environment.NewLine + _indent + "OdometerValue             : " + Info.OdometerValue;
                _dump += Environment.NewLine + _indent + "VehiculeNumber            : " + Info.VehiculeNumber;
                _dump += Environment.NewLine + _indent + "DriverNumber              : " + Info.DriverNumber;
                _dump += Environment.NewLine + _indent + "DriverInfo                :" + Info.DriverInfo;
                _dump += Environment.NewLine + _indent + "VehiculeSign              :" + Info.VehiculeSign;
                _dump += Environment.NewLine + _indent + "ChipTransaction           : " + Info.ChipTransaction;
            }
            else
            {
                _dump += Environment.NewLine + _indent + "No Info data available";
            }
            return _dump;
        }

        #endregion Private Methods

        #region Public Fields

        public static Guid EventGuid = new Guid("{3353A8EC-007E-4924-AFE3-C3F4EF49AD87}");

        #endregion Public Fields

        #region Public Constructors

        public EFundingEvent(string terminalOid) : base(terminalOid)
        {
        }

        #endregion Public Constructors

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
            Log.LogInformation($"{m.Name}: NotifyError: nCookie: {nCookie}, nError: {nError}, vDetail: {vDetail}. MyCookie: {Cookie}");
        }

        public void FireNotifyResultExt(int nCookie, int nRequest, object vResult)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            Log.LogInformation($"NotifyResult: nCookie: {nCookie}, nRequest: {nRequest}, vResult: {vResult}. MyCookie: {Cookie}");
            if (!(vResult is ITOMEFundingIndication))
            {
                Log.LogWarning($"{m.Name}:Received info is not of expected type 'ITOMEFundingIndication'.");
            }
            else
            {
                ITOMEFundingIndication Info = vResult as ITOMEFundingIndication;
                Log.LogInformation($"Received info is type 'ITOMEFundingIndication'  ({TerminalOid})>>>>");
                Log.LogInformation(DumpShort($"", Info));
                Log.LogDebug(Dump($"", Info));
                HandleEfundingResult(Info);
            }
        }

        #endregion Public Methods
    }
}