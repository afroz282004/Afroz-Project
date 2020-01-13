using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace JVVNLClassLIB
{
    public class dailycollection_bal
    {
        #region GENERAL DECLARATION
        dailycollection_dal  dailycollectiondal=null;
        string _ErrMessage;
        public string ErrMessage{get { return _ErrMessage; }set { _ErrMessage = value; }}
        #endregion
        
        public string DailyCollectionInsert(dailycollection_Prop dailycollectionprop)
        {
            try
            {
                dailycollectiondal = new dailycollection_dal ();
                string strMsg = dailycollectiondal.DailyCollectionInsert(dailycollectionprop );
                return  strMsg; 
            }
            catch (Exception e)
            {
                ErrMessage = e.Message; 
                return "";
            }
        }
        
        public DataTable DailyCollectionSelect(dailycollection_Prop  dailycollectionprop)
        {
            try
            {
                dailycollectiondal= new dailycollection_dal ();
                DataTable dt = dailycollectiondal.DailyCollectionSelect(dailycollectionprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }

        public DataTable DailyCashSelect(dailycollection_Prop dailycollectionprop)
        {

            try
            {
                dailycollectiondal = new dailycollection_dal();
                DataTable dt = dailycollectiondal.DailyCashSelect(dailycollectionprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }


        public DataSet DailyCollectionDetails(dailycollection_Prop dailycollectionprop)
        {

            try
            {
                dailycollectiondal = new dailycollection_dal();
                DataSet ds = dailycollectiondal.DailyCollectionDetails(dailycollectionprop);
                return ds;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        
    }
}
