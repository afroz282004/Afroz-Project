using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace JVVNLClassLIB
{
    public class menu_bal
    {
        #region GENERAL DECLARATION
        menu_dal menudal=null;
        string _ErrMessage;
        public string ErrMessage{get { return _ErrMessage; }set { _ErrMessage = value; }}
        #endregion
        
        
        public DataTable MenuSelect(menu_Prop menuprop)
        {
            try
            {
                menudal = new menu_dal();
                DataTable dt = menudal.MenuSelect(menuprop);
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
