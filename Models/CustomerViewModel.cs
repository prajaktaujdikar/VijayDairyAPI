
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VijayDairy.Models
{
    public class CustomerMilkHistoryViewModel
    {
        public DateTime Date { get; set; }
        public DateTime RDate { get; set; }
        public long TransactionID { get; set; }
        public string EmployeeName { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
        public int RemainingBalance { get; set; }
    }
    public class CustomerRechargeHistoryViewModel
    {
        public long RechargeID { get; set; }
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public DateTime RDate { get; set; }
        public int MRP { get; set; }
        public string PaymentType { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public string BankName { get; set; }
        public string Status { get; set; }
    }
}