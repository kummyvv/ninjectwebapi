using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Utilities
{
    public enum TransactionCode
    {
        Void = 0,
        Success = 1,
        Created = 2,
        Updated = 3,
        Deleted = 4,
        Failed = 5,
        TechnicalError = 6,
        LockedAccount = 7,
        MultipleAttempts = 8,
        InvalidPassword = 9,
        InvalidAccount = 10

    }
    public enum TransactionType
    {
        Create = 1,
        Update = 2,
        Delete = 3
    }
    public class TransactionInfo
    {

        public System.Net.HttpStatusCode status { get; set; }
        public TransactionCode msgCode { get; set; }
        public string message { get; set; }
        public Object transactionObject { get; set; }
        public int LineNumber { get; set; }
        public string ProcedureName { get; set; }


        

    }
}
