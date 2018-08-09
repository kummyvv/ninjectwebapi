using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cyient.MDT.Infrastructure.Concrete.BasicInputs;
using Cyient.MDT.WebAPI.Core.Entities.BasicInput;
using Cyient.MDT.WebAPI.Notification.Product;
    using Cyient.MDT.WebAPI.Notification.ConcreteProduct;
namespace Cyient.MDT.WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        //[HttpGet]
        //[Route("api/GetBasicInput")]
        //public BasicInputOptions Get()
        //{
        //    BasicInputRepository bsI = new BasicInputRepository();
        //    return bsI.GetBasicInputs();
        //}

        [HttpPost]
        [Route("api/SendEmail")]
        public string SendEmail(SendMailRequest sendMailRequest)
        {
            Email email = new Email();
            string message = "test email";//email.SendNotification(sendMailRequest);

            return message;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

    }
}
