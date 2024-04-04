using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VijayDairy.Models;

namespace VijayDairy.BusinessLogic
{
    public class PaymentManager
    {
		public string salt = string.Empty;
        public string Key = string.Empty;
		public string env = WebApiApplication.env;
        public string initiatePaymentURL = string.Empty;

		public PaymentManager()
        {
            switch (env)
            {
                case "prod":
					Key = WebApiApplication.prodkey;
					salt = WebApiApplication.prodsalt;
					initiatePaymentURL = WebApiApplication.initiatePaymentProdURL;
					break;
				case "test":
					Key = WebApiApplication.testkey;
					salt = WebApiApplication.testsalt;
					initiatePaymentURL = WebApiApplication.initiatePaymentTestURL;
					break;
                default:
                    break;
            }
        }

		public async Task<string> InitiatePayment(PaymentviewModel paymentViewModel)
		{
			string[] hashVarsSeq;
			string hash_string = string.Empty;
			string gen_hash;
			// Generate transaction ID -> make sure this is unique for all transactions
			Random rnd = new Random();
			string strHash = Easebuzz_Generatehash512(rnd.ToString() + DateTime.Now);
			string txnid = strHash.ToString().Substring(0, 20);

			// generate hash table
			Hashtable data = new Hashtable(); // adding values in gash table for data post
			data.Add("txnid", txnid);
			data.Add("key", Key);
			data.Add("amount", float.Parse(paymentViewModel.Amount, CultureInfo.InvariantCulture.NumberFormat));
			data.Add("firstname", paymentViewModel.Firstname?.Trim() ?? "");
			data.Add("phone", paymentViewModel.Phone?.Trim() ?? "");
			data.Add("email", paymentViewModel.Email?.Trim() ?? "");
			data.Add("productinfo", paymentViewModel.Productinfo?.Trim() ?? "");
			data.Add("surl", "https://localhost:44303");
			data.Add("furl", "https://localhost:44303");
			data.Add("udf1", paymentViewModel.Udf1?.Trim() ?? "");
            data.Add("udf2", paymentViewModel.Udf2?.Trim() ?? "");
            data.Add("udf3", paymentViewModel.Udf3?.Trim() ?? "");
            data.Add("udf4", paymentViewModel.Udf4?.Trim() ?? "");
            data.Add("udf5", paymentViewModel.Udf5?.Trim() ?? "");
            data.Add("udf6", paymentViewModel.Udf6?.Trim() ?? "");
            data.Add("udf7", paymentViewModel.Udf7?.Trim() ?? "");
            data.Add("udf8", paymentViewModel.Udf8?.Trim() ?? "");
            data.Add("udf9", paymentViewModel.Udf9?.Trim() ?? "");
            data.Add("udf10", paymentViewModel.Udf10?.Trim() ?? "");

            // generate hash
            hashVarsSeq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10".Split('|'); // spliting hash sequence from config
			foreach (string hash_var in hashVarsSeq)
			{
				hash_string = hash_string + (data.ContainsKey(hash_var) ? data[hash_var].ToString() : "");
				hash_string = hash_string + '|';
			}
			hash_string += salt;// appending SALT
			gen_hash = Easebuzz_Generatehash512(hash_string);//generating hash
			data.Add("hash", gen_hash);

			var client = new RestClient(initiatePaymentURL);
			var request = new RestRequest("", Method.Post);
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddParameter("key", Key);
			request.AddParameter("txnid", txnid);
			request.AddParameter("amount", float.Parse(paymentViewModel.Amount, CultureInfo.InvariantCulture.NumberFormat));
			request.AddParameter("productinfo", paymentViewModel.Productinfo?.Trim() ?? "");
			request.AddParameter("firstname", paymentViewModel.Firstname?.Trim() ?? "");
			request.AddParameter("phone", paymentViewModel.Phone?.Trim() ?? "");
			request.AddParameter("email", paymentViewModel.Email?.Trim() ?? "");
			request.AddParameter("surl", "https://localhost:44303");
			request.AddParameter("furl", "https://localhost:44303");
			request.AddParameter("hash", gen_hash);
			request.AddParameter("udf1", paymentViewModel.Udf1?.Trim() ?? "");
			request.AddParameter("udf2", paymentViewModel.Udf2?.Trim() ?? "");
			request.AddParameter("udf3", paymentViewModel.Udf3?.Trim() ?? "");
			request.AddParameter("udf4", paymentViewModel.Udf4?.Trim() ?? "");
			request.AddParameter("udf5", paymentViewModel.Udf5?.Trim() ?? "");
			request.AddParameter("udf6", paymentViewModel.Udf6?.Trim() ?? "");
			request.AddParameter("udf7", paymentViewModel.Udf7?.Trim() ?? "");
			request.AddParameter("udf8", paymentViewModel.Udf8?.Trim() ?? "");
			request.AddParameter("udf9", paymentViewModel.Udf9?.Trim() ?? "");
			request.AddParameter("udf10", paymentViewModel.Udf10?.Trim() ?? "");
			RestResponse response = await client.ExecuteAsync(request);
			var result = (JObject)JsonConvert.DeserializeObject(response.Content);
			int status = result["status"].Value<int>();
			string accessKey = result["data"].Value<string>();

			return status == 1 ? accessKey : "";
		}

		// hashcode generation
		public string Easebuzz_Generatehash512(string text)
		{
			byte[] message = Encoding.UTF8.GetBytes(text);
			byte[] hashValue;
			SHA512Managed hashString = new SHA512Managed();
			string hex = "";
			hashValue = hashString.ComputeHash(message);
			foreach (byte x in hashValue)
			{
				hex += string.Format("{0:x2}", x);
			}
			return hex;
		}
	}
}