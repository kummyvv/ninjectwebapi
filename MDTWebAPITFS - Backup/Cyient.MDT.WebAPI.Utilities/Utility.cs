using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Utilities
{
    public class Utility
    {

        /// <summary>
        /// It will bind the error if any error is there in Stored Proc
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static SQLErrorInfo GetError(DataTable dt)
        {
            SQLErrorInfo eInfo = new SQLErrorInfo();
            if (dt.Rows.Count > 0)
            {
                eInfo.Status = Convert.ToInt32(dt.Rows[0][0]);
                eInfo.ErrorNumber = Convert.ToInt32(dt.Rows[0][0]);
                eInfo.Procedure = dt.Rows[0]["ErrorProcedure"].ToString();
                eInfo.ErrorLineNo = Convert.ToInt32(dt.Rows[0]["ErrorLine"]);
                eInfo.TechnicalError = dt.Rows[0]["TechnicalError"].ToString();
                eInfo.ErrorMessage = dt.Rows[0]["ErrorMessage"].ToString();
            }

            return eInfo;
        }

        
        public static object GetTransObject(object obj, int Status)
        {
            TransactionInfo mdt = new TransactionInfo();
            ResourceReader resourceReader = new ResourceReader();
            //resourceReader.ReadResourceProperties();
            switch (Status)
            {
                case 0: // if no record found
                    mdt.transactionObject = obj;
                    mdt.status = HttpStatusCode.OK;
                    mdt.msgCode = TransactionCode.Success;
                    mdt.message = resourceReader.NoRecordMessage;
                    break;
                case 1: // if action completed successfully

                    mdt.transactionObject = obj;
                    mdt.status = HttpStatusCode.OK;
                    mdt.msgCode = TransactionCode.Success;
                    mdt.message = resourceReader.SuccessMessage;
                    break;
                case 2: // if record created and saved successfully

                    mdt.transactionObject = obj;
                    mdt.status = HttpStatusCode.OK;
                    mdt.msgCode = TransactionCode.Created;
                    mdt.message = resourceReader.RecordSaveSuccess;
                    break;
                case 3: // if record updated

                    mdt.transactionObject = obj;
                    mdt.status = HttpStatusCode.OK;
                    mdt.msgCode = TransactionCode.Updated;
                    mdt.message = resourceReader.UpdatedMessage;
                    break;
                case 4: // if record deleted

                    mdt.transactionObject = obj;
                    mdt.status = HttpStatusCode.OK;
                    mdt.msgCode = TransactionCode.Success;
                    mdt.message = resourceReader.DeletedMessage;
                    break;
                case 5: // If technical error comes into stored proc
                    mdt.transactionObject = obj;
                    mdt.status = HttpStatusCode.ExpectationFailed;
                    mdt.msgCode = TransactionCode.Failed;
                    mdt.message = resourceReader.TechnicalErrorMessage;
                    break;
                case 6: // If technical error comes into stored proc
                    mdt.transactionObject = obj;
                    mdt.status = HttpStatusCode.ExpectationFailed;
                    mdt.msgCode = TransactionCode.TechnicalError;
                    mdt.message = resourceReader.TechnicalErrorMessage;
                    break;
                case 7: // If user account is already locked
                    mdt.transactionObject = obj;
                    mdt.status = HttpStatusCode.OK;
                    mdt.msgCode = TransactionCode.Failed;
                    mdt.message = resourceReader.LockedAccount;
                    break;
                case 8: // if user entered 3 or more time wrong password
                    mdt.transactionObject = obj;
                    mdt.status = HttpStatusCode.OK;
                    mdt.msgCode = TransactionCode.Failed;
                    mdt.message = resourceReader.MultipleWrongAttempts;
                    break;
                case 9: // if user entered invalid password
                    mdt.transactionObject = obj;
                    mdt.status = HttpStatusCode.OK;
                    mdt.msgCode = TransactionCode.Failed;
                    mdt.message = resourceReader.InvalidPassword;
                    break;
                case 10: // if user entered invalid credential
                    mdt.transactionObject = obj;
                    mdt.status = HttpStatusCode.ExpectationFailed;
                    mdt.msgCode = TransactionCode.Failed;
                    mdt.message = resourceReader.InvalidCredential;
                    break;
                default:
                    mdt.transactionObject = obj;
                    mdt.status = HttpStatusCode.ExpectationFailed;
                    mdt.msgCode = TransactionCode.TechnicalError;
                    mdt.message = resourceReader.TechnicalErrorMessage;
                    break;
            }
            return mdt;
        }
    }
}
