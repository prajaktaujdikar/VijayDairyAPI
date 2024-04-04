using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VijayDairy.Models
{
    public class CustomerRechargeDetailViewModel
    {
        public long CustomerRechargeID { get; set; }
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public DateTime? RechargeDate { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? InsertTime { get; set; }
        public string ResponseStatus { get; set; }
        public string PaymentMode { get; set; }
        public string BankCode { get; set; }
        public bool IsFail { get; set; }
        public bool IsHashMismatch { get; set; }
        public string HashString { get; set; }
        public string ResponseHashString { get; set; }
        public string ErrorCode { get; set; }
        public decimal? AdditionalCharge { get; set; }
        public string PaymentSource { get; set; }
    }
}