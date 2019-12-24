using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RewardSystemWeb
{
    public class Logger
    {

        public static ILog Log { get; set; }

        static Logger()
        {
            Log = LogManager.GetLogger(typeof(Logger));
        }
        public static void Error(object msg)
        {
            Log.Error(msg);
        }

        public static void Error(object msg, Exception ex)
        {
            Log.Error(msg, ex);
        }

        public static void Info(object msg)
        {
            Log.Info(msg);
        }
    }

}