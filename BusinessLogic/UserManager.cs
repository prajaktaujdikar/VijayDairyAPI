using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VijayDairy.Models;

namespace VijayDairy.BusinessLogic
{
    public class UserManager
    {
        private SqlConnection conn = WebApiApplication.conn;
        public List<UserViewModel> GetUserID(string MobileNo, string Password)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("ApkGetUserID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                cmd.Parameters.AddWithValue("@Password", Password);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                List<UserViewModel> userViewModels = new List<UserViewModel>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        UserViewModel userView = new UserViewModel
                        {
                            CardNo = Convert.ToString(dr["CardNo"]),
                            CompanyAddress = Convert.ToString(dr["CompanyAddress"]),
                            CompanyID = Convert.ToInt64(dr["CompanyID"]),
                            CompanyMobileNo = Convert.ToString(dr["CompanyMobileNo"]),
                            CompanyName = Convert.ToString(dr["CompanyName"]),
                            CurrentStatus = Convert.ToBoolean(dr["CurrentStatus"]),
                            IsBranch = Convert.ToBoolean(dr["IsBranch"]),
                            IsMobile = Convert.ToBoolean(dr["IsMobile"]),
                            IsScan = Convert.ToBoolean(dr["IsScan"]),
                            IsWallet = Convert.ToBoolean(dr["IsWallet"]),
                            UserID = Convert.ToInt32(dr["UserID"]),
                            UserName = Convert.ToString(dr["UserName"]),
                            UserTypeID = Convert.ToInt32(dr["UserTypeID"]),
                            UType = Convert.ToChar(dr["UType"]),
                        };
                        userViewModels.Add(userView);
                    };
                }
                return userViewModels;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetUserID {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public List<User> GetUser(int userTypeID)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("GetUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserTypeID", userTypeID);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                List<User> userViewModels = new List<User>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        User userView = new User
                        {
                            MobileNo = Convert.ToString(dr["MobileNo"]),
                            Password = Convert.ToString(dr["Password"]),
                            RechargePassword = Convert.ToString(dr["RechargePassword"]),
                            UserId = Convert.ToInt64(dr["UserId"]),
                            UserTypeId = Convert.ToInt64(dr["UserTypeId"]),
                            CurrentStatus = Convert.ToBoolean(dr["CurrentStatus"]),
                            UserName = Convert.ToString(dr["UserName"])
                        };
                        userViewModels.Add(userView);
                    };
                }
                return userViewModels;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetUser {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public List<User> UserData(bool currentStatus)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("UserData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurrentStatus", currentStatus);
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                List<User> userViewModels = new List<User>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        User userView = new User
                        {
                            MobileNo = Convert.ToString(dr["MobileNo"]),
                            Password = Convert.ToString(dr["Password"]),
                            RechargePassword = Convert.ToString(dr["RechargePassword"]),
                            UserId = Convert.ToInt64(dr["UserId"]),
                            UserTypeId = Convert.ToInt64(dr["UserTypeId"]),
                            CurrentStatus = Convert.ToBoolean(dr["CurrentStatus"]),
                            UserName = Convert.ToString(dr["UserName"])
                        };
                        userViewModels.Add(userView);
                    };
                }
                return userViewModels;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in UserData {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}