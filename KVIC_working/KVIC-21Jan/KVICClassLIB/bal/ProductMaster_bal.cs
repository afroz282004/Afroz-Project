using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;


namespace KVICClassLIB
{
   public  class ProductMaster_bal
    {
       ProductMaster_dal ProductMasterdal = new ProductMaster_dal();
       string _ErrMessage;
       public string ErrMessage { get { return _ErrMessage; } set { _ErrMessage = value; } }

       public string ProductMasterInsert_bal(ProductMaster_prop l_ProductMaster)
       {
           try
           {
             string strMsg =   ProductMasterdal.ProductInser_dal(l_ProductMaster);
             return strMsg;
           }
           catch (Exception Ex)
           {

               ErrMessage = Ex.Message;
               return "";
           }
       }

       public DataTable ProductSearch_bal(ProductMaster_prop ProductMasterprop)
       {
           try
           {

               DataTable dt = ProductMasterdal.ProductSearch_dal(ProductMasterprop);
               return dt;
           }
           catch (Exception Ex)
           {
               ErrMessage = Ex.Message;
               return null;
           }
       }
    }
}
