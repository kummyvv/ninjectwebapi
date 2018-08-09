using System;
using System.Net;
using System.Web.Http.Cors;
using System.Web.Http;
using Cyient.MDT.Infrastructure.Concrete.Account;
using Cyient.MDT.WebAPI.Core.Entities.Account;
using Cyient.MDT.WebAPI.Core.Common;
using System.Threading.Tasks;
using Cyient.MDT.WebAPI.WebAPISecurity;
using Cyient.MDT.WebAPI.Models;

namespace Cyient.MDT.WebAPI.Controllers.Account
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [BasicAuthentication]
    public class ChangePasswordController : ApiController
    {
        private readonly IChangePassword _iChangePassword = null;

        public ChangePasswordController(IChangePassword iChangePassword)
        {
            _iChangePassword = iChangePassword;
        }


        // GET api/values
        [HttpPost]
        [Route("api/ChangePassword/ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePassword changePassword)
        {
            MDTTransactionInfo transactionInfo = null;
            Logger Log = new Logger();
            transactionInfo = await Task.Run(() =>
            {
                try
                {
                    return _iChangePassword.ChangePassword(changePassword);
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
