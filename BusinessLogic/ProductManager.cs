using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using VijayDairy.Models;

namespace VijayDairy.BusinessLogic
{
    public class ProductManager
    {
        private SqlConnection conn = WebApiApplication.conn;

        public List<ProductViewModel> GetAllProduct()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("APKGetAllProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                List<ProductViewModel> productView = new List<ProductViewModel>();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ProductViewModel product = new ProductViewModel
                        {
                            ProductID = Convert.ToInt32(dr["ProductID"]),
                            ProductNameG = Convert.ToString(dr["ProductNameG"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            Price = Convert.ToInt32(dr["Price"]),
                            IsFixedRate = Convert.ToBoolean(dr["IsFixedRate"])
                        };

                        productView.Add(product);
                    }
                }
                return productView;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurs in GetAllProduct {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}