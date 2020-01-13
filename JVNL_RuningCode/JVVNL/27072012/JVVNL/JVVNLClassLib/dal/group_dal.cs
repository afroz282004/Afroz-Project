using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace JVVNLClassLIB
{
    partial class group_dal
    {
        #region "General Declaration"
        Common com = new Common();
        #endregion
        #region "counter Data Methods"
        public DataTable GroupSelect()
        {
            try
            {
                
                return SqlHelper.ExecuteDataset(com.con, "usp_Group_select", new object[] { }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
