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
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [Route("{mobileNo}/GetUserId")]
        [HttpPost]
        public HttpResponseMessage GetUserID(string mobileNo, [FromBody] User user)
        {
            try
            {
                List<UserViewModel> userViewModels = new UserManager().GetUserID(mobileNo, user.Password);
                return userViewModels.Count == 0
                    ? Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        status = "false",
                        message = "Not Found"
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = userViewModels
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

        [Route("{userTypeID:int}/GetUser")]
        [HttpGet]
        public HttpResponseMessage GetUser(int userTypeID)
        {
            try
            {
                List<User> userViewModels = new UserManager().GetUser(userTypeID);
                return userViewModels.Count == 0
                    ? Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        status = "false",
                        message = "Not Found"
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = userViewModels
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

        [Route("UserData")]
        [HttpGet]
        public HttpResponseMessage UserData(bool currentStatus)
        {
            try
            {
                List<User> userViewModels = new UserManager().UserData(currentStatus);
                return userViewModels.Count == 0
                    ? Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        status = "false",
                        message = "Not Found"
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = userViewModels
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
