using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KVICClassLIB
          
{
   public class transaction_dal
    {
        #region "General Declaration"
        Common com = new Common();
        #endregion

        public DataTable PopulateCluster_dal(Transaction_Prop transacprop)
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_Transaction_PopCluster", new object[] { transacprop.UserName  }).Tables[0];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DataTable popImpAgency_dal()
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_Transaction_PopImplemAgency", new object[] { }).Tables[0];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DataTable popState_dal()
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_Transaction_PopState", new object[] { }).Tables[0];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DataTable popTechAgency_dal()
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_Transaction_PopTechagency", new object[] { }).Tables[0];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public string TransactionInsert(Transaction_Prop transacprop)
        {
            try
            {
                return SqlHelper.ExecuteDataset(com.con, "usp_MonthlyRFD_Insert", new object[] { 
                                                                                transacprop.ClusterID
                                                                                ,transacprop.Capacity
                                                                                ,transacprop.ExposureVisit
                                                                                ,transacprop.MarketPromotion
                                                                                ,transacprop.ArtisansBenefited
                                                                                ,transacprop.NoOFCFC
                                                                                ,transacprop.CFCArtisan
                                                                                ,transacprop.SHG
                                                                                ,transacprop.SHGMembers
                                                                                ,transacprop.ReplacementCharkhas
                                                                                ,transacprop.ReplacementLooms 
                                                                                ,transacprop.ReplacementTools                                                                                                                                                               
                                                                                ,transacprop.FundCluster
                                                                                ,transacprop.FundSODO
                                                                                ,transacprop.FundIA
                                                                                ,transacprop.FundTools
                                                                                ,transacprop.FundCFC
                                                                                ,transacprop.FundPDDI
                                                                                ,transacprop.FundMPA
                                                                                ,transacprop.FundCBM
                                                                                ,transacprop.FundCostIA
                                                                                ,transacprop.FundCDE
                                                                                ,transacprop.FundTA                                                                                                                                                                                                                                                                                                                      
                                                                                ,transacprop.Production
                                                                                ,transacprop.Prod_ArtisanBenefited
                                                                                ,transacprop.PerCapitaProduction
                                                                                ,transacprop.SC_Male
                                                                                ,transacprop.SC_Female
                                                                                ,transacprop.ST_Male
                                                                                ,transacprop.ST_Female
                                                                                ,transacprop.OBC_Male
                                                                                ,transacprop.OBC_Female
                                                                                ,transacprop.Minority_Male
                                                                                ,transacprop.Minority_Female
                                                                                ,transacprop.Others_Male
                                                                                ,transacprop.Others_Female
                                                                                ,transacprop.AvgWagesSpinners
                                                                                ,transacprop.AvgWagesArtisan
                                                                                ,transacprop.AvgWagesWeavers
                                                                                ,transacprop.AvgEarningSpinners
                                                                                ,transacprop.AvgEarningArtisan
                                                                                ,transacprop.AvgEarningWeavers
                                                                                ,transacprop.Sales                                                                                
                                                                                ,transacprop.NoOfCDCGMeeting
                                                                                ,transacprop.NoOfTAVisit
                                                                                ,transacprop.NoOfClusterMeeting
                                                                                ,transacprop.NoOfVisitNodalOfficer
                                                                                ,transacprop.NoOfStateMeeting
                                                                                ,transacprop.NoOfZonalMeeting
                                                                                ,transacprop.NoOfReviewMeeting
                                                                                ,transacprop.NoOfVisitVIP
                                                                                ,transacprop.Achievements
                                                                                ,transacprop.MajorLinkages
                                                                                ,transacprop.OtherIssues
                                                                                }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
