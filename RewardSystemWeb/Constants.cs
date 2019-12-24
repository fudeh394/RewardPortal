using log4net;
using RewardSystem;
using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RewardSystemWeb
{
    public static class Constants
    {
        public const string USER_LOGIN_STATUS_ACTIVE = "A";
        public const string USER_LOGIN_STATUS_NOTACTIVE = "N";

        public const string BVN_REQUEST_TYPE_RETAIL_LINKING = "R";
        public const string BVN_REQUEST_TYPE_RETAIL_MODIFICATION = "RM";
        public const string BVN_REQUEST_TYPE_CORPORATE_LINKING = "C";
        public const string BVN_REQUEST_TYPE_CORPORATE_MODIFICATION = "CM";

        public const string ACCOUNT_LEVEL_SAVINGS = "S";
        public const string ACCOUNT_LEVEL_CURRENT = "S";
        //public const string ACCOUNT_LEVEL_CURRENT = "S";

        public const string BVN_CUSTOMER_TYPE_RETAIL = "R";
        public const string BVN_CUSTOMER_TYPE_CORPORATE = "C";

        public const int REQUESTSTATUS_APPROVED = 1;
        public const int REQUESTSTATUS_PENDING = 0;
        public const int REQUESTSTATUS_DECLINED = 2;
        public const int REQUESTSTATUS_COMMUNICATED = 3;
        public const int REQUESTSTATUS_FAILED = 4;


        public const string BVN_REQUESTSTATUS_APPROVED = "A";
        public const string BVN_REQUESTSTATUS_PENDING = "P";
        public const string BVN_REQUESTSTATUS_DECLINED = "D";
        public const string BVN_REQUESTSTATUS_COMMUNICATED = "C";
        public const string BVN_REQUESTSTATUS_FAILED = "F";

        public const int ROLE_TYPE_INPUTER = 1;
        public const int ROLE_TYPE_AUTHORISER = 2;
        public const int ROLE_TYPE_HEADOFFICE = 3;
        public const int ROLE_TYPE_ADMIN = 4;
    }


    public static
        class MySessions
    {


        public static List<BillingResult> CurrentUpload
        {
            get
            {
                var session = HttpContext.Current.Session;
                if (session["__CurrentUpload__"] == null)
                {
                    var uc = new List<BillingResult>();

                    session["__CurrentUpload__"] = uc;
                }
                return session["__CurrentUpload__"] as List<BillingResult>;

            }
            set
            {
                var session = HttpContext.Current.Session;
                session["__CurrentUpload__"] = value;
            }
        }

    }

}