using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Cyient.MDT.Infrastructure.Concrete.Account;
using Cyient.MDT.WebAPI.Core.Common;
using Cyient.MDT.WebAPI.Core.Entities.Account;

namespace Cyient.MDT.WebAPI.Models
{
    public class UserLoginDep : IUserLogin
    {
        public MDTTransactionInfo Login(UserLogin userLogin)
        {
            UserAccountConcrete _service = new UserAccountConcrete();
            Logger Log = new Logger();
            try
            {
                Log.WriteErrorLog("I: " + "<br/>" + Environment.NewLine + "Logged in with user :" + userLogin.UserName +
                       "" + Environment.NewLine);
                Log.WriteErrorLog(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);

                return _service.Login(userLogin);
            }
            catch (Exception ex)
            {
                
                Log.WriteErrorLog(ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine);
                Log.WriteErrorLog(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);

                return new MDTTransactionInfo { msgCode = MessageCode.Failed, message = ex.Message, status = HttpStatusCode.InternalServerError };
            }
        }
    }
}