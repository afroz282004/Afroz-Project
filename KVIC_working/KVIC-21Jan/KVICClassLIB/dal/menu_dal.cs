using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace KVICClassLIB
{
    partial class menu_dal
    {
        #region "General Declaration"
        Common com = new Common();
        #endregion
        #region "bank Data Methods"
        public DataTable MenuSelect(menu_Prop menuprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_menu_select", new object[] { menuprop.UserName }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
