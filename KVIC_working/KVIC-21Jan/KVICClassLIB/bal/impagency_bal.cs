using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace KVICClassLIB
{
    public class impagency_bal
    {
        #region GENERAL DECLARATION
        impagency_dal impagencydal = null;
        string _ErrMessage;
        public string ErrMessage { get { return _ErrMessage; } set { _ErrMessage = value; } }
        #endregion

        public string ImpAgencyInsert(impagency_prop impagencyprop)
        {
            try
            {
                impagencydal = new impagency_dal();
                string strMsg = impagencydal.ImpAgencyInsert(impagencyprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }

        public string ImpAgencyUpdate(impagency_prop impagencyprop)
        {
            try
            {
                impagencydal = new impagency_dal();
                string strMsg = impagencydal.ImpAgencyUpdate(impagencyprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public string ImpAgencyPChildInsert_bal(int product, int num, int maxid)
        {
            try
            {
                impagencydal = new impagency_dal();
                string strMsg = impagencydal.ImpAgencyPChildInsert(product, num, maxid);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }

        public string ImpAgencyDelete(impagency_prop impagencyprop)
        {
            try
            {
                impagencydal = new impagency_dal();
                string strMsg = impagencydal.ImpAgencyDelete(impagencyprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public DataTable ImpAgencySearch(impagency_prop impagencyprop)
        {
            try
            {
                impagencydal = new impagency_dal();
                DataTable dt = impagencydal.ImpAgencySearch(impagencyprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }

        public DataTable PopulateProduct_bal()
        {
            try
            {
                impagencydal = new impagency_dal();
                return impagencydal.PopulateProduct_dal();
            }

            catch (Exception Ex)
            {
                ErrMessage = Ex.Message;
                return null;
            }
        }
        public DataTable PopulateProductChild_dal(int id)
        {
            try
            {
                impagencydal = new impagency_dal();
                DataTable dt = impagencydal.PopulateProductChild_dal(id);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }

        public DataTable ImpAgencySelect(impagency_prop impagencyprop)
        {
            try
            {
                impagencydal = new impagency_dal();
                DataTable dt = impagencydal.ImpAgencySelect(impagencyprop);
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
