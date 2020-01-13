using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace JVVNLClassLIB
{
    partial class sdo_dal
    {
        #region "General Declaration"
        Common com = new Common();
        #endregion
        #region "SDO Data Methods"
        public string SDOInsert(sdo_Prop  sdoprop)
        {
            try
            {
                
                return SqlHelper.ExecuteDataset(com.con, "usp_sdo_insert", new object[] {sdoprop.SDOCode,sdoprop.SDOName  }).Tables[0].Rows[0][0].ToString ();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string SDODelete(sdo_Prop sdoprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_sdo_delete", new object[] { sdoprop.SDOCode}).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable SDOSearch(sdo_Prop sdoprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_sdo_search", new object[] { sdoprop.SDOCode,sdoprop.SDOName  }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
