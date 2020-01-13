using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KVICClassLIB
{
    partial class cluster_dal
    {
        #region "General Declaration"
        Common com = new Common();
        #endregion
        #region "User Data Methods"
        public string ClusterCheckIfExist(cluster_prop clusterprop)
        {
            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(com.con, "usp_Cluster_CheckIfExist", new object[] { clusterprop.ClusterName });
                if (ds.Tables.Count > 1)
                    return ds.Tables[1].Rows[0][0].ToString();
                else
                    return ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable ClusterSelect(cluster_prop clusterprop)
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_Cluster_Select", new object[] { clusterprop.ClusterID }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable ClusterSearch(cluster_prop clusterprop)
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_Cluster_Search", new object[] { clusterprop.ClusterName }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       

        public string ClusterInsert(cluster_prop clusterprop)
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_ClusterMaster_INSERT", new object[] { clusterprop.ClusterName, clusterprop.Address, clusterprop.StateID, clusterprop.ImpAgencyID, clusterprop.TechAgencyID }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ClusterUpdate(cluster_prop clusterprop)
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_ClusterMaster_Update", new object[] { clusterprop.ClusterName, clusterprop.Address, clusterprop.StateID, clusterprop.ImpAgencyID, clusterprop.TechAgencyID }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ClusterDelete(cluster_prop clusterprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_Cluster_Delete", new object[] { clusterprop.ClusterID }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public DataTable ClusterFillAddress_dal(int id)
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_Cluster_FillAddress", new object[] { id }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public DataTable ClusterFill_dal()
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_Cluster_Fill", new object[] {  }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       

        #endregion
    }
}
