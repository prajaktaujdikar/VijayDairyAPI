using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VijayDairy.BusinessLogic;
using VijayDairy.Models;

namespace VijayDairy.Controllers
{
    [RoutePrefix("api/customerrecharge")]
    public class RechargeController : ApiController
    {
        public RechargeController() : base() { }

        [Route("{customerRechargeID:long}/CustomerRechargeDetail")]
        [HttpGet]
        public HttpResponseMessage GetCustomerRechargeDetail(long customerRechargeID)
        {
            try
            {
                List<CustomerRechargeDetailViewModel> rechargeDetail = new CustomerManager().GetCustomerRechargeDetail(customerRechargeID);
                return rechargeDetail.Count == 0
                    ? Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        status = "false",
                        message = "Not Found"
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = rechargeDetail
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

        [Route("AddCustomerRecharge")]
        [HttpPost]
        public HttpResponseMessage AddCustomerRecharge(CustomerRechargeDetailViewModel customerRecharge)
        {
            try
            {
                long rechargeId = new CustomerManager().AddCustomerRecharge(customerRecharge);
                return rechargeId == 0
                    ? Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        status = "false",
                        message = "Not Found"
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = new {
                            customerRechargeId = rechargeId
                        }
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

        [Route("UpdateCustomerRecharge")]
        [HttpPut]
        public HttpResponseMessage UpdateCustomerRecharge(CustomerRechargeDetailViewModel customerRecharge)
        {
            try
            {
                bool isUpdated = new CustomerManager().UpdateCustomerRecharge(customerRecharge);
                return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = isUpdated ? "Updated Successfull" : "Not Updated"
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
