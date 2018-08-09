using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;
using System.Resources;
namespace Cyient.MDT.WebAPI.Utilities
{
    public class ResourceReader
    {
        private ResourceManager rm = null;
        Assembly assembly;
        string[] resources;
        public ResourceReader()
        {
           rm= new ResourceManager("Cyient.MDT.WebAPI.Utilities.mdtResources.resx", Assembly.GetExecutingAssembly());
            assembly = Assembly.GetExecutingAssembly();
            resources = assembly.GetManifestResourceNames();
            ReadResourceProperties();
        }
        public string ChangePasswordMessage { get; set; }
        public string EmailBody { get; set; }
        public string EmailFailure { get; set; }
        public string ForgotPasswordEmailFailure { get; set; }
        public string ForgotPasswordEmailSuccess { get; set; }

        public string NoRecordMessage { get; set; }
        public string RecordFoundMessage { get; set; }
        public string RecordSavedFailure { get; set; }
        public string RecordSaveSuccess { get; set; }
        public string TechnicalErrorMessage { get; set; }
        public string LoginSuccess { get; set; }
        public string InvalidCredential { get; set; }
        public string LockedAccount { get; set; }
        public string MultipleWrongAttempts { get; set; }
        public string InvalidPassword { get; set; }
        public string SuccessMessage { get; set; }
        public string DeletedMessage { get; set; }
        public string UpdatedMessage { get; set; }

        
        private void ReadResourceProperties()
        {
            string s = resources[0].ToString();

         ChangePasswordMessage = rm.GetString("ChangePasswordMessage",CultureInfo.CurrentCulture);
         EmailBody = rm.GetString("EmailBody");
         EmailFailure = rm.GetString("EmailFailure");
         ForgotPasswordEmailFailure = rm.GetString("ForgotPasswordEmailFailure");
         ForgotPasswordEmailSuccess = rm.GetString("ForgotPasswordEmailSuccess");
         NoRecordMessage = rm.GetString("NoRecordMessage");
         RecordFoundMessage = rm.GetString("RecordFoundMessage");
         RecordSavedFailure = rm.GetString("RecordSavedFailure");

         RecordSaveSuccess = rm.GetString("RecordSaveSuccess");
         TechnicalErrorMessage = rm.GetString("TechnicalErrorMessage");
         LoginSuccess = rm.GetString("LoginSuccess");
         InvalidCredential = rm.GetString("InvalidCredential");
         LockedAccount = rm.GetString("LockedAccount");
         MultipleWrongAttempts = rm.GetString("MultipleWrongAttempts");
         InvalidPassword = rm.GetString("InvalidPassword");
         SuccessMessage = rm.GetString("SuccessMessage");
         DeletedMessage = rm.GetString("DeletedMessage");
         UpdatedMessage = rm.GetString("UpdatedMessage");
    }

    }
}
