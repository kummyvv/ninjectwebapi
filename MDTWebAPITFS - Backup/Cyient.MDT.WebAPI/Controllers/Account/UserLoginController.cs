using System;
using System.IO;
using System.Net;
using System.Web.Http.Cors;
using System.Web.Http;
using Cyient.MDT.Infrastructure.Concrete.Account;
using Cyient.MDT.WebAPI.Core.Entities.Account;
using Cyient.MDT.WebAPI.Core.Common;
using System.Threading.Tasks;
using Cyient.MDT.WebAPI.Models;

namespace Cyient.MDT.WebAPI.Controllers.Account
{
   [EnableCors("*", "*", "*")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    //[RoutePrefix("api/UserLogin")]
    public class UserLoginController : ApiController
    {
        private readonly IUserLogin _iUserLogin= null;

        public UserLoginController()
        {

        }
        public UserLoginController(IUserLogin iUserLogin)
        {
            _iUserLogin = iUserLogin;
        }
        // GET api/values
        [HttpPost]
        [Route("api/UserLogin/Login")]
        //[Route("login", Name = "Login")]
        public async Task<IHttpActionResult> Login(UserLogin userLogin)
        {
            MDTTransactionInfo transactionInfo = null;
            Logger Log = new Logger();
            transactionInfo = await Task.Run(() =>
            {
                try
                {
                    if (userLogin == null)
                        Log.WriteErrorLog("userLogin object have no value or null");
                    return _iUserLogin.Login(userLogin);
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


        [HttpPost]
        [Route("api/UserLogin/GetPost")]
        public string GetPost()
        {
            return "This is post method called from Angular";
        }
    }
}
