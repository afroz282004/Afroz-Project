using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KVICClassLIB
{
    partial class techagency_dal
    {
        #region "General Declaration"
        Common com = new Common();
        #endregion
        #region "Technical Data Methods"
        public string TechAgencyInsert(techagency_prop techagencyprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_TechAgency_insert", new object[] { techagencyprop.TechAgencyName, techagencyprop.Address, techagencyprop.Phone, techagencyprop.Fax, techagencyprop.Email }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string TechAgencyUpdate(techagency_prop techagencyprop)
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_TechAgency_Update", new object[] { techagencyprop.TechAgencyName, techagencyprop.Address, techagencyprop.Phone, techagencyprop.Fax, techagencyprop.Email }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string TechAgencyDelete(techagency_prop techagencyprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_TechAgency_delete", new object[] { techagencyprop.TechAgencyName }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable TechAgencySearch(techagency_prop techagencyprop)
        {
            try
            {

                return SqlHelper.ExecuteDataset(com.con, "usp_TechAgency_search", new object[] { techagencyprop.TechAgencyName }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable TechAgencySelect(techagency_prop techagencyprop)
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_TechAgency_Select", new object[] { techagencyprop.id }).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
