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
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        public ProductController() : base() { }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetAllProduct()
        {
            try
            {
                List<ProductViewModel> productList = new ProductManager().GetAllProduct();
                return productList.Count == 0
                    ? Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        status = "false",
                        message = "Not Found"
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = productList
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
