using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace JVVNLClassLIB
{
    public class sdo_bal
    {
        #region GENERAL DECLARATION
        sdo_dal sdodal=null;
        string _ErrMessage;
        public string ErrMessage{get { return _ErrMessage; }set { _ErrMessage = value; }}
        #endregion
        
        public string SDOInsert(sdo_Prop sdoprop)
        {
            try
            { 
                sdodal= new sdo_dal();
                string strMsg = sdodal.SDOInsert(sdoprop);
                return  strMsg; 
            }
            catch (Exception e)
            {
                ErrMessage = e.Message; 
                return "";
            }
        }
        public string SDODelete(sdo_Prop sdoprop)
        {
            try
            {
                sdodal = new sdo_dal();
                string strMsg = sdodal.SDODelete(sdoprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public DataTable SDOSearch(sdo_Prop sdoprop)
        {
            try
            {
                sdodal = new sdo_dal();
                DataTable dt = sdodal.SDOSearch(sdoprop);
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
