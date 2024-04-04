using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VijayDairy.Models;

namespace VijayDairy.BusinessLogic
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        public CustomerController() : base() { }

        [Route("{customerID}/GetMilkHistoryForCustomer")]
        [HttpGet]
        public HttpResponseMessage GetMilkHistoryForCustomer(string customerID, [FromUri] string productID, [FromUri] string startDate, [FromUri] string lastDate)
        {
            try
            {
                List<CustomerMilkHistoryViewModel> milkHistorForCustomer = new CustomerManager().GetMilkHistoryForCustomer(customerID, productID, startDate, lastDate);
                return milkHistorForCustomer.Count == 0
                    ? Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        status = "false",
                        message = "Not Found"
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = milkHistorForCustomer
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

        [Route("{customerID}/GetRechargeHistoryForCustomer")]
        [HttpGet]
        public HttpResponseMessage GetRechargeHistoryForCustomer(string customerID, [FromUri] string startDate, [FromUri] string lastDate)
        {
            try
            {
                List<CustomerRechargeHistoryViewModel> CustomerRechargeHistoryViewModel = new CustomerManager().GetRechargeHistoryForCustomer(customerID, startDate, lastDate);
                return CustomerRechargeHistoryViewModel.Count == 0
                    ? Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        status = "false",
                        message = "Not Found"
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = CustomerRechargeHistoryViewModel
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

        [Route("{customerID:int}/GetIsTransactionPinByCustomer")]
        [HttpGet]
        public HttpResponseMessage GetIsTransactionPinByCustomer(int customerID)
        {
            try
            {
                bool isTransactionPin = new CustomerManager().GetIsTransactionPinByCustomer(customerID);
                return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = new {
                            IsTransactionPin = isTransactionPin
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

        [Route("{customerID:int}/UpdateIsTransactionPinByCustomer")]
        [HttpGet]
        public HttpResponseMessage UpdateIsTransactionPinByCustomer(int customerID, [FromUri] bool isTransactionPin)
        {
            try
            {
                bool isUpdated = new CustomerManager().UpdateIsTransactionPinByCustomer(customerID, isTransactionPin);
                return !isUpdated
                    ? Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "false",
                        message = "Not Updated",
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Updated Successfull",
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

        [Route("{mobileNo}/SendForgetCustomerPinOTP")]
        [HttpGet]
        public HttpResponseMessage SendForgetCustomerPinOTP(string mobileNo)
        {
            try
            {
                string otp = new CustomerManager().SendForgetCustomerPinOTP(mobileNo);
                return string.IsNullOrWhiteSpace(otp)
                    ? Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "false",
                        message = "Failed to send OTP",
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "OTP sent Successfully",
                        data = new
                        {
                           otp
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

        [Route("{mobileNo}/GetCustomerPinByMobileNo")]
        [HttpGet]
        public HttpResponseMessage GetCustomerPinByMobileNo(string mobileNo)
        {
            try
            {
                bool isSent = new CustomerManager().GetCustomerPinByMobileNo(mobileNo);
                return !isSent
                    ? Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "false",
                        message = "Failed to send messsage",
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Sent message Successfull",
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

        [Route("{mobileNo}/CheckMobileNoExistsForForgetPassword")]
        [HttpGet]
        public HttpResponseMessage CheckMobileNoExistsForForgetPassword(string mobileNo)
        {
            try
            {
                long customerId = new CustomerManager().CheckMobileNoExistsForForgetPassword(mobileNo);
                return customerId == 0
                    ? Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        status = "false",
                        message = "Mobile number not exists",
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = new {
                            CustomerId = customerId
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

        [Route("{mobileNo}/SendForgetPasswordOTP")]
        [HttpGet]
        public HttpResponseMessage SendForgetPasswordOTP(string mobileNo)
        {
            try
            {
                string otp = new CustomerManager().SendForgetPasswordOTP(mobileNo);
                return string.IsNullOrWhiteSpace(otp)
                    ? Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "false",
                        message = "Failed to send OTP",
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "OTP sent Successfully",
                        data = new
                        {
                            otp
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

        [Route("{mobileNo}/GetPasswordByMobileNo")]
        [HttpGet]
        public HttpResponseMessage GetPasswordByMobileNo(string mobileNo)
        {
            try
            {
                bool isSent = new CustomerManager().GetPasswordByMobileNo(mobileNo);
                return !isSent
                    ? Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "false",
                        message = "Failed to send message",
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Message sent Successfully"
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

        [Route("{userID}/GetCustomerDataForUser")]
        [HttpGet]
        public HttpResponseMessage GetCustomerDataForUser(string userID, [FromUri] string startDate, [FromUri] string lastDate)
        {
            try
            {
                List<dynamic> Customers = new CustomerManager().GetCustomerDataForUser(userID, startDate, lastDate);
                return (Customers?.Count ?? 0) == 0
                    ? Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        status = "false",
                        message = "Not Found"
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = Customers
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

        [Route("{customerID}/GetCustomerEntryForUser")]
        [HttpGet]
        public HttpResponseMessage GetCustomerEntryForUser(string customerID, string userID, string firstDate, string lastDate)
        {
            try
            {
                List<dynamic> Customers = new CustomerManager().GetCustomerEntryForUser(customerID, userID, firstDate, lastDate);
                return (Customers?.Count ?? 0) == 0
                    ? Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        status = "false",
                        message = "Not Found"
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = Customers
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

        [Route("{customerID}/remainingAmount")]
        [HttpGet]
        public HttpResponseMessage RemainingAmount(string customerID)
        {
            try
            {
                int remainingAmount = new CustomerManager().RemainingAmount(customerID);
                return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = new
                        {
                            RemainingAmount = remainingAmount
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

        [Route("{customerID}/GetEmployeeDetailByCustomer")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeDetailByCustomer(string customerID)
        {
            try
            {
                dynamic customer = new CustomerManager().GetEmployeeDetailByCustomer(customerID);
                return customer == null
                    ? Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        status = "false",
                        message = "Not Found"
                    })
                    : Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "true",
                        message = "Successfull",
                        data = customer
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
