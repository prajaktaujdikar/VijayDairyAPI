using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using VijayDairy.Models;

namespace VijayDairy.BusinessLogic
{
    public class CustomerManager
    {
        private SqlConnection conn = WebApiApplication.conn;
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public List<CustomerMilkHistoryViewModel> GetMilkHistoryForCustomer(string customerID, string productID, string startDate, string lastDate)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("GetMilkHistoryForCustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@LastDate", lastDate);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                List<CustomerMilkHistoryViewModel> CustomerViewModel = new List<CustomerMilkHistoryViewModel>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        CustomerMilkHistoryViewModel customerView = new CustomerMilkHistoryViewModel
                        {
                            Date = Convert.ToDateTime(dr["Date"]),
                            RDate = Convert.ToDateTime(dr["RDate"]),
                            TransactionID = Convert.ToInt64(dr["TransactionID"]),
                            Amount = Convert.ToInt32(dr["Amount"]),
                            EmployeeName = Convert.ToString(dr["EmployeeName"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            Qty = Convert.ToInt32(dr["Qty"]),
                            Rate = Convert.ToInt32(dr["Rate"]),
                            RemainingBalance = Convert.ToInt32(dr["RemainingBalance"]),
                        };
                        CustomerViewModel.Add(customerView);
                    };
                }
                return CustomerViewModel;
            } catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetMilkHistoryForCustomer {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public List<CustomerRechargeHistoryViewModel> GetRechargeHistoryForCustomer(string customerID, string startDate, string lastDate)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("GetRechargeHistoryForCustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@LastDate", lastDate);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                List<CustomerRechargeHistoryViewModel> CustomerRechargeHistoryViewModel = new List<CustomerRechargeHistoryViewModel>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        CustomerRechargeHistoryViewModel rechargeHistoryViewModel = new CustomerRechargeHistoryViewModel
                        {
                            Date = Convert.ToDateTime(dr["Date"]),
                            RDate = Convert.ToDateTime(dr["RDate"]),
                            BankName = Convert.ToString(dr["BankName"]),
                            ChequeDate = Convert.ToString(dr["ChequeDate"]),
                            ChequeNo = Convert.ToString(dr["ChequeNo"]),
                            CustomerID = Convert.ToInt64(dr["CustomerID"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            MRP = Convert.ToInt32(dr["MRP"]),
                            PaymentType = Convert.ToString(dr["PaymentType"]),
                            RechargeID = Convert.ToInt64(dr["RechargeID"]),
                            Status = Convert.ToString(dr["Status"]),
                        };
                        CustomerRechargeHistoryViewModel.Add(rechargeHistoryViewModel);
                    };
                }

                return CustomerRechargeHistoryViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetMilkHistoryForCustomer {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public bool GetIsTransactionPinByCustomer(int cid)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("APKGetIsTransactionPinByCustomerID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", cid);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                bool isTransactionPin = false;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    isTransactionPin = Convert.ToBoolean(dr["IsTransactionPin"]);
                }
                return isTransactionPin;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetIsTransactionPinByCustomer {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public bool UpdateIsTransactionPinByCustomer(int cid, bool isTransactionPin)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("APKUpdateIsTransactionPinByCustomerID", conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", cid);
                    cmd.Parameters.AddWithValue("@IsTransactionPin", isTransactionPin);
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in UpdateIsTransactionPinByCustomer {ex.Message}");
            }
            return false;
        }

        public string SendForgetCustomerPinOTP(string mobileNo)
        {
            try
            {
                var Message = " is your forget Customer password OTP. This is valid for 3 minutes and one time accessible only. Please do not share this OTP to anyone";
                Random r = new Random();
                string OTP = r.Next(1000, 9999).ToString();
                string message1 = OTP + Message;
                string message2 = OTP + Message + " Regards, Vijay Dairy.";
                SMSsend(mobileNo, message2);
                return OTP;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in SendForgetCustomerPinOTP {ex.Message}");
            }
            finally
            {
            }

            return string.Empty;
        }  
        public bool GetCustomerPinByMobileNo(string mobileNo)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("APKGetCustomerPinByMobileNo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MobileNo", mobileNo).DbType = DbType.String;
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string Password = ds.Tables[0].Rows[0][0].ToString();

                    var Message = " is your Old CustomerPin. Please do not share this Pin to anyone.";
                    string message1 = Password + Message;
                    string message2 = Password + Message + " Regards, Vijay Dairy.";
                    SMSsend(mobileNo, message2);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetCustomerPinByMobileNo {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return false;
        }
        public long CheckMobileNoExistsForForgetPassword(string mobileNo)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("APKCheckMobileNoExists", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MobileNo", mobileNo).DbType = DbType.String;
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt64(ds.Tables[0].Rows[0]["CustomerID"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetCustomerPinByMobileNo {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return 0;
        }
        public string SendForgetPasswordOTP(string mobileNo)
        {
            try
            {
                var Message = " is your forget password OTP. This is valid for 3 minutes and one time accessible only. Please do not share this OTP to anyone";
                Random r = new Random();
                string OTP = r.Next(1000, 9999).ToString();
                string message1 = OTP + Message;
                string message2 = OTP + Message + " Regards, Vijay Dairy.";
                SMSsend(mobileNo, message2);
                return OTP;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetCustomerPinByMobileNo {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public bool GetPasswordByMobileNo(string mobileNo)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("APKGetPasswordByMobileNo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MobileNo", mobileNo).DbType = DbType.String;
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string Password = ds.Tables[0].Rows[0][0].ToString();

                    var Message = " is your Old Password. Please do not share this Password to anyone.";
                    string message2 = Password + Message + " Regards, Vijay Dairy.";
                    SMSsend(mobileNo, message2);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetCustomerPinByMobileNo {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return false;
        }
        public List<dynamic> GetCustomerDataForUser(string userId, string startDate, string lastDate)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("GetCustomerDataForUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@FirstDate", startDate);
                cmd.Parameters.AddWithValue("@LastDate", lastDate);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                List<dynamic> customers = new List<dynamic>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic model = new 
                        {
                            CustomerID = Convert.ToInt64(dr["CustomerID"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                        };
                        customers.Add(model);
                    };
                }

                return customers;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetCustomerDataForUser {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public List<dynamic> GetCustomerEntryForUser(string customerID, string userId, string startDate, string lastDate)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("GetCustomerEntryForUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                cmd.Parameters.AddWithValue("@FirstDate", startDate);
                cmd.Parameters.AddWithValue("@LastDate", lastDate);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                List<dynamic> customers = new List<dynamic>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic model = new
                        {
                            Date = Convert.ToDateTime(dr["Date"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            Qty = Convert.ToInt32(dr["Qty"]),
                            ODate = Convert.ToDateTime(dr["ODate"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                        };
                        customers.Add(model);
                    };
                }

                return customers;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetCustomerEntryForUser {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public List<CustomerRechargeDetailViewModel> GetCustomerRechargeDetail(long customerRechargeID)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("APKGetCustomerRechargeDetailByRechargeID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerRechargeID", customerRechargeID);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                List<CustomerRechargeDetailViewModel> rechargeDetail = new List<CustomerRechargeDetailViewModel>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DateTime? inserttime = null, rechargeDate = null;
                        if(!Convert.IsDBNull(dr["InsertTime"]))
                        {
                            inserttime = Convert.ToDateTime(dr["InsertTime"]);
                        }
                        if (!Convert.IsDBNull(dr["RechargeDate"]))
                        {
                            rechargeDate = Convert.ToDateTime(dr["RechargeDate"]);
                        }
                        CustomerRechargeDetailViewModel model = new CustomerRechargeDetailViewModel
                        {
                            AdditionalCharge = Convert.ToDecimal(dr["AdditionalCharge"]),
                            Amount= Convert.ToDecimal(dr["Amount"]),
                            BankCode= Convert.ToString(dr["BankCode"]),
                            CustomerID= Convert.ToInt64(dr["CustomerID"]),
                            CustomerName= Convert.ToString(dr["CustomerName"]),
                            ErrorCode= Convert.ToString(dr["ErrorCode"]),
                            HashString= Convert.ToString(dr["HashString"]),
                            InsertTime = inserttime,
                            IsFail = Convert.ToBoolean(dr["IsFail"]),
                            IsHashMismatch = Convert.ToBoolean(dr["IsHashMismatch"]),
                            MobileNo = Convert.ToString(dr["CustomerMobileNo"]),
                            PaymentMode = Convert.ToString(dr["PaymentMode"]),
                            PaymentSource = Convert.ToString(dr["payment_source"]),
                            RechargeDate = rechargeDate,
                            ResponseHashString = Convert.ToString(dr["ResponseHashString"]),
                            ResponseStatus = Convert.ToString(dr["ResponseStatus"])
                        };
                        rechargeDetail.Add(model);
                    };
                }

                return rechargeDetail;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetCustomerRechargeDetail {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public int RemainingAmount(string customerID)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("APKCustomerRemainingAmount", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                int remainingAmount = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    remainingAmount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }

                return remainingAmount;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetCustomerEntryForUser {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public long AddCustomerRecharge(CustomerRechargeDetailViewModel customerRecharge)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("ApkInsertCustomerRecharge", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", customerRecharge.CustomerID);
                cmd.Parameters.AddWithValue("@RechargeDate", customerRecharge.RechargeDate);
                cmd.Parameters.AddWithValue("@Amount", customerRecharge.Amount);
                cmd.Parameters.AddWithValue("@InsertTime", customerRecharge.InsertTime);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                long customerRechargeID = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    customerRechargeID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }

                return customerRechargeID;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in AddCustomerRecharge {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public bool UpdateCustomerRecharge(CustomerRechargeDetailViewModel customerRecharge)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("ApkUpdateCustomerRecharge", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerRechargeID", customerRecharge.CustomerRechargeID);
                cmd.Parameters.AddWithValue("@CustomerID", customerRecharge.CustomerID);
                cmd.Parameters.AddWithValue("@ResponseStatus", customerRecharge.ResponseStatus);
                cmd.Parameters.AddWithValue("@PaymentMode", customerRecharge.PaymentMode);
                cmd.Parameters.AddWithValue("@BankCode", customerRecharge.BankCode);
                cmd.Parameters.AddWithValue("@IsFail", customerRecharge.IsFail);
                cmd.Parameters.AddWithValue("@IsHashMismatch", customerRecharge.IsHashMismatch);
                cmd.Parameters.AddWithValue("@HashString", customerRecharge.HashString);
                cmd.Parameters.AddWithValue("@ResponseHashString", customerRecharge.ResponseHashString);
                cmd.Parameters.AddWithValue("@ErrorCode", Encoding.ASCII.GetBytes(customerRecharge.ErrorCode));
                cmd.Parameters.AddWithValue("@AdditionalCharge", customerRecharge.AdditionalCharge);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in AddCustomerRecharge {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public dynamic GetEmployeeDetailByCustomer(string customerID)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("APKGetEmployeeDetailByCustomerID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                dynamic customer = null;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        customer = new
                        {
                            EmployeeID = Convert.ToInt64(dr["EmployeeID"]),
                            EmployeeName = Convert.ToString(dr["EmployeeName"]),
                            EmployeeMobileNo = Convert.ToString(dr["MobileNo"]),
                            WalletNo = Convert.ToString(dr["WalletNo"]),
                            Balance = Convert.ToString(dr["Balance"]),
                            VendorName = Convert.ToString(dr["VendorName"]),
                            VendorMobileNo = Convert.ToString(dr["CustomerMobileNo"])
                        };
                    }
                }

                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetEmployeeDetailByCustomer {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        private static void SMSsend(string no, string message)
        {
            try
            {
                message = message.Replace("\n", "\r\n");
                message = message.Replace("&", "%26");
                message = message.Replace("#", "%23");
                message = message.Replace("{", "%0x7b");
                message = message.Replace("}", "%0x7d");
                message = message.Replace("+", "%2B");
                //JAMINI
                //key=627da7300cXX
                //senderid=SKKDPL

                //string URL = "http://7doors.jaiminisoftware.com/mobicomm/submitsms.jsp?";
                //URL += "user=jatinptl";
                //URL += "&key=b63e5ac612XX" + "&mobile=" + no + "&message= " + message;
                //URL += "&senderid=VIJAYD" + "&accusage=1&unicode=0";

                string URL = "http://tsms.vishawebsolutions.com/smsstatuswithid.aspx?";
                URL += "mobile=9825112279";
                URL += "&pass=6a0368162488429695091db985ba79e1" + "&senderid=VIJAYD" + "&to= " + no;
                URL += "&msg=" + message;

                //string URL = "http://sms17.jaiminisoftware.com/submitsms.jsp?";
                //URL += "user=jatinptl";
                //URL += "&key=ac754430e1XX" + "&mobile=" + no + "&message= " + message;
                //URL += "&senderid=NTFSMS" + "&accusage=1&unicode=0";

                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(URL);
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs while sending SMS {ex.Message}");
            }
        }
    }
}