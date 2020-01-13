using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace JVVNLClassLIB
{
    partial class counter_dal
    {
        #region "General Declaration"
        Common com = new Common();
        #endregion
        #region "counter Data Methods"
        
        public string CounterInsert(counter_Prop counterprop)
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_Counter_INSERT", new object[] { counterprop.CounterName, counterprop.Address, counterprop.ContactPerson, counterprop.ContactNo }).Tables[0].Rows[0][0].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CounterUpdate(counter_Prop counterprop)
        {
            try
            {
                return SqlHelper.ExecuteNonQuery(com.con, "usp_Counter_UPDATE", new object[] { counterprop.Counterid, counterprop.CounterName, counterprop.Address, counterprop.ContactPerson, counterprop.ContactNo }).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CounterDelete(counter_Prop counterprop)
        {

            try
            {
                return SqlHelper.ExecuteNonQuery(com.con, "usp_Counter_DELETE", new object[] { counterprop .Counterid  }).ToString();
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public DataTable CounterSearch(counter_Prop counterprop)
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_Counter_Search", new object[] { counterprop.CounterName  }).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable CounterSelect(counter_Prop counterprop)
        {

            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_Counter_select", new object[] { counterprop.Counterid  }).Tables[0];
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion
    }
}
