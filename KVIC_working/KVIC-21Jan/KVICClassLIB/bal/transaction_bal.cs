using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace KVICClassLIB
{
   public  class transaction_bal
    {
        #region GENERAL DECLARATION
       
        transaction_dal transactiondal = null;
        string _ErrMessage;
        public string ErrMessage { get { return _ErrMessage; } set { _ErrMessage = value; } }
        #endregion
     
       public DataTable PopulateCluster_dal(Transaction_Prop transacprop)
       {
           try
           {
               transactiondal = new transaction_dal();
               return transactiondal.PopulateCluster_dal(transacprop);

           }
           catch (Exception Ex)
           {
               ErrMessage = Ex.Message;
               return null;
           }
       }
       

       public DataTable popImpAgency_bal()
       {
           try
           {
               transactiondal = new transaction_dal();
               return transactiondal.popImpAgency_dal();
           }
           catch (Exception Ex)
           {
               ErrMessage = Ex.Message;
               return null;
           }
       }

       public DataTable popTechAgency_bal()
       {
           try
           {
               transactiondal = new transaction_dal();
               return transactiondal.popTechAgency_dal();
           }
           catch (Exception Ex)
           {
               ErrMessage = Ex.Message;
               return null;
           }
       }

       public DataTable popState_bal()
       {
           try
           {
               transactiondal = new transaction_dal();
               return transactiondal.popState_dal();
           }
           catch (Exception Ex)
           {
               ErrMessage = Ex.Message;
               return null;
           }
       }

       public string TransactionInsert(Transaction_Prop transacprop)
       {
           try
           {
               transactiondal = new transaction_dal();
               string strMsg = transactiondal.TransactionInsert(transacprop);
               return strMsg;
           }
           catch (Exception e)
           {
               ErrMessage = e.Message;
               return "";
           }
       }


    }
}
