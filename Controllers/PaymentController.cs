using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VijayDairy.BusinessLogic;
using VijayDairy.Models;

namespace VijayDairy.Controllers
{
    [RoutePrefix("api/payment")]
    public class PaymentController : ApiController
    {
        public PaymentController() : base() { }

        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> InitiatePayment([FromBody] PaymentviewModel paymentviewModel)
        {
            try
            {
                string hash = await new PaymentManager().InitiatePayment(paymentviewModel);
                return string.IsNullOrWhiteSpace(hash)
                    ? Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        status = "false",
                        message = "Not generated"
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = hash
                    });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new
                {
                    status = "false",
                    message = ex.Message
                });
            }
        }
    }
}
