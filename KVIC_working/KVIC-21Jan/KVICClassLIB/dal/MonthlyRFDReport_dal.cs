using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace KVICClassLIB
{
   public class MonthlyRFDReport_dal
    {
       #region "General Declaration"
       Common com = new Common();
       #endregion

       public DataTable MonthlyReport_dal(int clusterid, string Date)
       {
           try
           {
               return SqlHelper.ExecuteDataset(com.con, "usp_MonthlyRFDCumulative_Report", new object[] { clusterid, Date }).Tables[0];
           }
           catch (Exception e)
           {
               throw e;
           }
       }

    }
}
