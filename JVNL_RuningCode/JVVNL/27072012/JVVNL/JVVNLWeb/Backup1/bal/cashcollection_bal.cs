using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace JVVNLClassLIB
{
    public class cashcollection_bal
    {
        #region GENERAL DECLARATION
        cashcollection_dal cashcollectiondal=null;
        string _ErrMessage;
        public string ErrMessage{get { return _ErrMessage; }set { _ErrMessage = value; }}
        #endregion
        
        public string CashCollectionInsert(cashcollection_Prop cashcollectionprop)
        {
            try
            {
                cashcollectiondal = new cashcollection_dal();
                string strMsg = cashcollectiondal.CashCollectionInsert(cashcollectionprop );
                return  strMsg; 
            }
            catch (Exception e)
            {
                ErrMessage = e.Message; 
                return "";
            }
        }
       
    }
}
