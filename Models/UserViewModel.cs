using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VijayDairy.Models
{
    public class UserViewModel
    {
		  public long UserID { get; set; }
		  public char UType { get; set; }
		  public int UserTypeID { get; set; }
		  public string UserName { get; set; }
		  public long CompanyID { get; set; }
		  public string CompanyName { get; set; }
		  public string CompanyAddress { get; set; }
		  public string CompanyMobileNo { get; set; }
		  public string CardNo { get; set; }
		  public bool CurrentStatus { get; set; }
		  public bool IsScan { get; set; }
		  public bool IsMobile { get; set; }
		  public bool IsWallet { get; set; }
		  public bool IsBranch { get; set; }
	}

    public class User
    {
        public long UserId { get; set; }
        public long UserTypeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public bool CurrentStatus { get; set; }
        public string RechargePassword { get; set; }
    }
}