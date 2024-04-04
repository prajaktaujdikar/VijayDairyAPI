using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace VijayDairy
{
    public class WebApiApplication : HttpApplication
    {
        public static string testkey = "2PBP7IABZ2";
        public static string prodkey = "K36XEC5MP7";
        public static string testsalt = "DAH88E3UWQ";
        public static string prodsalt = "FZNVP7RZ1P";
        public static string initiatePaymentTestURL = "https://testpay.easebuzz.in/payment/initiateLink";
        public static string initiatePaymentProdURL = "https://pay.easebuzz.in/payment/initiateLink";
        //public static string env = "prod";
        public static string env = "test";

        public static string ConnString = "Data Source=103.228.152.19;Initial Catalog=VijayDairy;User ID=vdairy;Password=Nwdk846*";
        public static SqlConnection conn = new SqlConnection(ConnString);
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
