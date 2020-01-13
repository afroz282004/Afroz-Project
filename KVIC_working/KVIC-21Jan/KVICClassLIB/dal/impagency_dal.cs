using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace KVICClassLIB
{
    partial class impagency_dal
    {
        #region "General Declaration"
        Common com = new Common();
        #endregion
        #region "Implementing Data Methods"
        public string ImpAgencyInsert(impagency_prop impagencyprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_ImpAgency_insert", new object[] { impagencyprop.ImpAgencyName, impagencyprop.Address, impagencyprop.Phone, impagencyprop.Fax, impagencyprop.Email, impagencyprop.ProductId, impagencyprop.ProductType }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ImpAgencyUpdate(impagency_prop impagencyprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_ImpAgency_Update", new object[] { impagencyprop.ImpAgencyName, impagencyprop.Address, impagencyprop.Phone, impagencyprop.Fax, impagencyprop.Email, impagencyprop.ProductId, impagencyprop.ProductType }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ImpAgencyPChildInsert(int product,int num,int maxid)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_ImpAgencyPChild_insert", new object[] { product, num, maxid }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ImpAgencyDelete(impagency_prop impagencyprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_ImpAgency_delete", new object[] { impagencyprop.ImpAgencyName }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable ImpAgencySearch(impagency_prop impagencyprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_ImpAgency_search", new object[] { impagencyprop.ImpAgencyName }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable PopulateProduct_dal()
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_Product_PopProduct", new object[] { }).Tables[0];
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DataTable PopulateProductChild_dal(int id)
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_TechnicalAgency_ProductChild", new object[] { id }).Tables[0];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DataTable ImpAgencySelect(impagency_prop impagencyprop)
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_ImpAgency_Select", new object[] { impagencyprop.id }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
