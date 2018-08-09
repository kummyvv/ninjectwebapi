using System;
using System.IO;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using Cyient.MDT.Infrastructure.Concrete.BasicInputs;
using Cyient.MDT.WebAPI.Core.Common;
using Cyient.MDT.WebAPI.WebAPISecurity;
namespace Cyient.MDT.WebAPI.Controllers.BasicInput
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[EnableCors("*", "*", "*")]
    [BasicAuthentication]
    public class BasicInputController : ApiController
    {

        BasicInputConcrete _service = new BasicInputConcrete();
        // GET api/values
        [HttpGet]
        [Route("api/BasicInput/GetBasicInput/{packageID}/{configurationID}")]
        public IHttpActionResult Get(int packageID, int configurationID)
        {
            MDTTransactionInfo tInfo = null;
            try
            {
                tInfo = _service.GetBasicInputs(packageID, configurationID);
            }
            catch (Exception ex)
            {
                Logger Log = new Logger();
                Log.WriteErrorLog(ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine);
                Log.WriteErrorLog(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);

                tInfo = new MDTTransactionInfo { msgCode = MessageCode.Failed, message = ex.Message, status = HttpStatusCode.InternalServerError };
            }
            return Ok(tInfo);
        }

    }
}
