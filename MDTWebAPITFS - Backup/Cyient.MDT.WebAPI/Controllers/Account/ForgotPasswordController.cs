using System;
using System.Net;
using System.Web.Http;
using Cyient.MDT.Infrastructure.Concrete.Account;
using Cyient.MDT.WebAPI.Core.Entities.Account;
using Cyient.MDT.WebAPI.Core.Common;
using System.Threading.Tasks;
using Cyient.MDT.WebAPI.WebAPISecurity;
using Cyient.MDT.WebAPI.Models;

namespace Cyient.MDT.WebAPI.Controllers.Account
{
  
    public class ForgotPasswordController : ApiController
    {
        private readonly  IForgotPassword _iforgotPassword = null;

        public ForgotPasswordController(IForgotPassword iforgotPassword)
        {
            _iforgotPassword = iforgotPassword;
        }

        // GET api/values
        [HttpPost]
        [Route("api/ForgotPassword/ForgotPassword")]
        public async Task<IHttpActionResult> ForgotPassword(ForgotPassword forgotPassword)
        {
            MDTTransactionInfo transactionInfo = null;
            Logger Log = new Logger();
            transactionInfo = await Task.Run(() =>
            {
                try
                {
                    return _iforgotPassword.ForgotPassword(forgotPassword);
                }
                catch (Exception ex)
                {
                    
                    Log.WriteErrorLog(ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                           "" + Environment.NewLine);
                    Log.WriteErrorLog(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);

                    return new MDTTransactionInfo { msgCode = MessageCode.Failed, message = ex.Message, status = HttpStatusCode.InternalServerError };
                }
            });

            return Ok(transactionInfo);
        }
    }
}
