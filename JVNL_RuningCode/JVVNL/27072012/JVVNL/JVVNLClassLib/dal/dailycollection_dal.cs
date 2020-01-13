using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace JVVNLClassLIB
{
    partial class dailycollection_dal
    {
        #region "General Declaration"
        Common com = new Common();
        #endregion
        #region "Daily Collection Data Methods"
        public string DailyCollectionInsert(dailycollection_Prop  dailycollectionprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_DailyCollection_INSERT", new object[] { dailycollectionprop.ThosandNotes, dailycollectionprop.FiveHundredNotes, 
                    dailycollectionprop.HundredNotes, dailycollectionprop.FiftyNote, dailycollectionprop.TwentyNotes, 
                    dailycollectionprop.TenNotes, dailycollectionprop.FiveNotes, dailycollectionprop.TwoNotes, 
                    dailycollectionprop.OneNotes, dailycollectionprop.UserName, dailycollectionprop.CDate, dailycollectionprop.CashAmt, dailycollectionprop.ChequeAmt}).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable DailyCollectionSelect(dailycollection_Prop dailycollectionprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_DailyCollection_select", new object[] { dailycollectionprop.UserName,dailycollectionprop.CDate }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable DailyCashSelect(dailycollection_Prop dailycollectionprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_CashAmount_Select", new object[] { dailycollectionprop.UserName }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataSet DailyCollectionDetails(dailycollection_Prop dailycollectionprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_dailycollectiondetails", new object[] { dailycollectionprop.CDate, dailycollectionprop.UserName });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
