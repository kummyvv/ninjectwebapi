using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Cors;
using System.Web.Http;
using Cyient.MDT.Infrastructure.Concrete.Account;
using Cyient.MDT.WebAPI.Core.Common;
using System.Web;
using System.IO;
using Cyient.MDT.WebAPI.WebAPISecurity;
using Cyient.MDT.WebAPI.Models;

namespace Cyient.MDT.WebAPI.Controllers.Account
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [BasicAuthentication]
    public class UploadProfilePicController : ApiController
    {
        private readonly IUploadPicture _iUploadPicture = null;

        public UploadProfilePicController(IUploadPicture iUploadPicture)
        {
            _iUploadPicture = iUploadPicture;
        }


        [HttpPost]
        [Route("api/UploadProfilePic/UploadPic/{UserID}")]
        public IHttpActionResult UploadPic(int UserID)
        {
            MDTTransactionInfo tInfo = null;
            Logger Log = new Logger();
            try
            {
                return _iUploadPicture.UploadPic(UserID);
                                            
            }
            catch (Exception ex)
            {
                
                Log.WriteErrorLog(ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine);
                Log.WriteErrorLog(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);

                tInfo = new MDTTransactionInfo { msgCode = MessageCode.Failed, message = ex.Message, status = HttpStatusCode.InternalServerError };
            }
           return Ok(tInfo);
        }

    }
}
