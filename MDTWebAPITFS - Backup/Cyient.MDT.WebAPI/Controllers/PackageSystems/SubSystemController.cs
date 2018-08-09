using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using Cyient.MDT.Infrastructure.Concrete.PackageSystems;
using Cyient.MDT.WebAPI.Core.Common;
using Cyient.MDT.WebAPI.WebAPISecurity;
using Cyient.MDT.WebAPI.Core.Repository.SubSystem;
//using Cyient.MDT.WebAPI.Controllers.WebAPISecurity;
namespace Cyient.MDT.WebAPI.Controllers.PackageSystems
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [BasicAuthentication]
    public class SubSystemController : ApiController
    {
        PackageSystemConcrete _service = new PackageSystemConcrete();
        //ISubSystem _service;

        //public SubSystemController(ISubSystem subSystem)
        //{
        //    _service = subSystem;
        //}

        [HttpGet]
        [Route("api/SubSystem/GetPackageSystem/{PackageID}")]
        public IHttpActionResult GetPackageSystem(int PackageID)
        {
            MDTTransactionInfo tInfo = null;
            try
            {
                tInfo = (PackageID == 0) ? new MDTTransactionInfo { msgCode = MessageCode.Failed, message = "Invalid Data", status = HttpStatusCode.BadRequest }
                                            : _service.GetPackageSystemDetails(PackageID);
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


        [HttpGet]
        [Route("api/SubSystem/GetEquipments/{PackageID}/{SystemID}/{ConfigurationID}")]
        public IHttpActionResult GetEquipments(int PackageID, int SystemID, int ConfigurationID)
        {
            MDTTransactionInfo tInfo = null;
            try
            {
                tInfo = (PackageID == 0 || SystemID == 0) ? new MDTTransactionInfo { msgCode = MessageCode.Failed, message = "Invalid Data", status = HttpStatusCode.BadRequest }
                                            : _service.GetEquipmentsDetail(PackageID,SystemID,ConfigurationID);
            }
            catch (Exception ex)
            {
                tInfo = new MDTTransactionInfo { msgCode = MessageCode.Failed, message = ex.Message, status = HttpStatusCode.InternalServerError };
            }

            return Ok(tInfo);
        }
    }
}
