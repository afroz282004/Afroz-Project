using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JVVNLClassLIB
{
    public class group_bal
    {
        #region GENERAL DECLARATION
        group_dal  groupdal = null;
        string _ErrMessage;
        public string ErrMessage { get { return _ErrMessage; } set { _ErrMessage = value; } }
        #endregion

        public DataTable GroupSelect()
        {
            try
            {
                groupdal = new group_dal  ();
                DataTable dt = groupdal.GroupSelect();
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
