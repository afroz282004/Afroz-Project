using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace KVICClassLIB
{
    public class techagency_bal
    {
        #region GENERAL DECLARATION
        techagency_dal techagencydal = null;
        string _ErrMessage;
        public string ErrMessage{get { return _ErrMessage; }set { _ErrMessage = value; }}
        #endregion
        
        public string TechAgencyInsert(techagency_prop techagencyprop)
        {
            try
            {
                techagencydal = new techagency_dal();
                string strMsg = techagencydal.TechAgencyInsert(techagencyprop);
                return  strMsg; 
            }
            catch (Exception e)
            {
                ErrMessage = e.Message; 
                return "";
            }
        }

        public string TechAgencyUpdate(techagency_prop techagencyprop)
        {
            try
            {
                techagencydal = new techagency_dal();
                string strMsg = techagencydal.TechAgencyUpdate(techagencyprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }

        public string TechAgencyDelete(techagency_prop techagencyprop)
        {
            try
            {
                techagencydal = new techagency_dal();
                string strMsg = techagencydal.TechAgencyDelete(techagencyprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }
        public DataTable TechAgencySearch(techagency_prop techagencyprop)
        {
            try
            {
                techagencydal = new techagency_dal();
                DataTable dt = techagencydal.TechAgencySearch(techagencyprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }

        public DataTable TechAgencySelect(techagency_prop techagencyprop)
        {
            try
            {
                techagencydal = new techagency_dal();
                DataTable dt = techagencydal.TechAgencySelect(techagencyprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
        //public DataTable PopulateProductChild_dal(int id)
        //{
        //    try
        //    {
        //        techagencydal = new techagency_dal();
        //        DataTable dt = techagencydal.
        //        return dt;
        //    }
        //    catch (Exception e)
        //    {
        //        ErrMessage = e.Message;
        //        return null;
        //    }
        //}
        
        
    }
}