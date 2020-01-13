using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace KVICClassLIB
{
   public class MonthlyRFDReport_bal
    {
        #region GENERAL DECLARATION
       MonthlyRFDReport_dal MonthlyRFDReportdal = null;
        string _ErrMessage;
        public string ErrMessage { get { return _ErrMessage; } set { _ErrMessage = value; } }
        #endregion

        public DataTable MonthlyReport_bal(int clusterid, string Date)
        {
            try
            {
                MonthlyRFDReportdal = new MonthlyRFDReport_dal();
                DataTable dt = MonthlyRFDReportdal.MonthlyReport_dal(clusterid, Date);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }

    }
}
