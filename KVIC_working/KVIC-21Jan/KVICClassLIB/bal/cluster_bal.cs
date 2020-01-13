using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace KVICClassLIB
{
    public class cluster_bal
    {
        #region GENERAL DECLARATION
        cluster_dal clusterdal = null;
        string _ErrMessage;
        public string ErrMessage { get { return _ErrMessage; } set { _ErrMessage = value; } }
        #endregion

        public string ClusterCheckIfExist(cluster_prop clusterprop)
        {
            try
            {
                clusterdal = new cluster_dal();
                string strMsg = clusterdal.ClusterCheckIfExist(clusterprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }

        public DataTable ClusterSelect(cluster_prop clusterprop)
        {
            try
            {
                clusterdal = new cluster_dal();
                DataTable dt = clusterdal.ClusterSelect(clusterprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }

        public string ClusterInsert(cluster_prop clusterprop)
        {
            try
            {
                clusterdal = new cluster_dal();
                string strMsg = clusterdal.ClusterInsert(clusterprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }

        public string ClusterUpdate(cluster_prop clusterprop)
        {
            try
            {
                clusterdal = new cluster_dal();
                string strMsg = clusterdal.ClusterUpdate(clusterprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }

        }
        public DataTable ClusterSearch(cluster_prop clusterprop)
        {
            try
            {
                clusterdal = new cluster_dal();
                DataTable dt = clusterdal.ClusterSearch(clusterprop);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }
    

        public string ClusterDelete(cluster_prop clusterprop)
        {
            try
            {
                clusterdal = new cluster_dal();
                string strMsg = clusterdal.ClusterDelete(clusterprop);
                return strMsg;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return "";
            }
        }


        public DataTable ClusterFillAddress_bal(int id)
        {

           

            try
            {
                clusterdal = new cluster_dal();
                DataTable dt = clusterdal.ClusterFillAddress_dal(id);
                return dt;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }

        }

        public DataTable ClusterFill_bal()
        {
            try
            {
                clusterdal = new cluster_dal();
                DataTable dt = clusterdal.ClusterFill_dal();
                return dt;
            }

            catch(Exception Ex)
            {
                ErrMessage = Ex.Message;
                return null;
            }

            
        }
       
    }
}
