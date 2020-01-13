using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KVICClassLIB
{
    public class Transaction_Prop
    {
        private string _UserName;
        private int _ClusterID;
        private int _Capacity;
        private int _ExposureVisit;
        private int _MarketPromotion;
        private int _ArtisansBenefited;
        private int _NoOFCFC;
        private int _CFCArtisan;
        private int _SHG;
        private int _SHGMembers;
        private int _ReplacementCharkhas;
        private int _ReplacementTools;
        private int _ReplacementLooms;
        private double _FundCluster;
        private double _FundSODO;
        private double _FundTools;
        private double _FundPDDI;
        private double _FundCBM;
        private double _FundCDE;
        private double _FundIA;
        private double _FundCFC;
        private double _FundMPA;
        private double _FundCostIA;
        private double _FundTA;

        private double _Production;
        private int _Prod_ArtisanBenefited;
        private double _PerCapitaProduction;
        private int _SC_Male;
        private int _SC_Female;
        private int _ST_Male;
        private int _ST_Female;
        private int _OBC_Male;
        private int _OBC_Female;
        private int _Monority_Male;
        private int _Minority_Female;
        private int _Others_Male;
        private int _Others_Female;
        private double _AvgWagesSpinners;
        private double _AvgWagesArtisan;
        private double _AvgWagesWeavers;
        private double _AvgEarningSpinners;
        private double _AvgEarningArtisan;
        private double _AvgEarningWeavers;
        private double _Sales;

        private int _NoOfCDCGMeeting;
        private int _NoOfTAVisit;
        private int _NoOfClusterMeeting;
        private int _NoOfVisitNodalOfficer;
        private int _NoOfStateMeeting;
        private int _NoOfZonalMeeting;
        private int _NoOfReviewMeeting;
        private int _NoOfVisitVIP;
        private string _Achievements;
        private string _MajorLinkages;
        private string _OtherIssues;


        public int ClusterID { get { return _ClusterID; } set { _ClusterID = value; } }
        public int Capacity { get { return _Capacity; } set { _Capacity = value; } }
        public int ExposureVisit { get { return _ExposureVisit; } set { _ExposureVisit = value; } }
        public int MarketPromotion { get { return _MarketPromotion; } set { _MarketPromotion = value; } }
        public int ArtisansBenefited { get { return _ArtisansBenefited; } set { _ArtisansBenefited = value; } }
        public int NoOFCFC { get { return _NoOFCFC; } set { _NoOFCFC = value; } }
        public int CFCArtisan { get { return _CFCArtisan; } set { _CFCArtisan = value; } }
        public int SHG { get { return _SHG; } set { _SHG = value; } }
        public int SHGMembers { get { return _SHGMembers; } set { _SHGMembers = value; } }
        public int ReplacementCharkhas { get { return _ReplacementCharkhas; } set { _ReplacementCharkhas = value; } }
        public int ReplacementTools { get { return _ReplacementTools; } set { _ReplacementTools = value; } }
        public int ReplacementLooms { get { return _ReplacementLooms; } set { _ReplacementLooms = value; } }

        public double FundCluster { get { return _FundCluster; } set { _FundCluster = value; } }
        public double FundSODO { get { return _FundSODO; } set { _FundSODO = value; } }
        public double FundTools { get { return _FundTools; } set { _FundTools = value; } }
        public double FundPDDI { get { return _FundPDDI; } set { _FundPDDI = value; } }
        public double FundCBM { get { return _FundCBM; } set { _FundCBM = value; } }
        public double FundCDE { get { return _FundCDE; } set { _FundCDE = value; } }
        public double FundIA { get { return _FundIA; } set { _FundIA = value; } }
        public double FundCFC { get { return _FundCFC; } set { _FundCFC = value; } }
        public double FundMPA { get { return _FundMPA; } set { _FundMPA = value; } }
        public double FundCostIA { get { return _FundCostIA; } set { _FundCostIA = value; } }
        public double FundTA { get { return _FundTA; } set { _FundTA = value; } }

        public double Production { get { return _Production; } set { _Production = value; } }
        public int Prod_ArtisanBenefited { get { return _Prod_ArtisanBenefited; } set { _Prod_ArtisanBenefited = value; } }
        public double PerCapitaProduction { get { return _PerCapitaProduction; } set { _PerCapitaProduction = value; } }
        public int SC_Male { get { return _SC_Male; } set { _SC_Male = value; } }
        public int SC_Female { get { return _SC_Female; } set { _SC_Female = value; } }
        public int ST_Male { get { return _ST_Male; } set { _ST_Male = value; } }
        public int ST_Female { get { return _ST_Female; } set { _ST_Female = value; } }
        public int OBC_Male { get { return _OBC_Male; } set { _OBC_Male = value; } }
        public int OBC_Female { get { return _OBC_Female; } set { _OBC_Female = value; } }
        public int Minority_Male { get { return _Monority_Male; } set { _Monority_Male = value; } }
        public int Minority_Female { get { return _Minority_Female; } set { _Minority_Female = value; } }
        public int Others_Male { get { return _Others_Male; } set { _Others_Male = value; } }
        public int Others_Female { get { return _Others_Female; } set { _Others_Female = value; } }
        public double AvgWagesSpinners { get { return _AvgWagesSpinners; } set { _AvgWagesSpinners = value; } }
        public double AvgWagesArtisan { get { return _AvgWagesArtisan; } set { _AvgWagesArtisan = value; } }
        public double AvgWagesWeavers { get { return _AvgWagesWeavers; } set { _AvgWagesWeavers = value; } }
        public double AvgEarningSpinners { get { return _AvgEarningSpinners; } set { _AvgEarningSpinners = value; } }
        public double AvgEarningArtisan { get { return _AvgEarningArtisan; } set { _AvgEarningArtisan = value; } }
        public double AvgEarningWeavers { get { return _AvgEarningWeavers; } set { _AvgEarningWeavers = value; } }
        public double Sales { get { return _Sales; } set { _Sales = value; } }

        public int NoOfCDCGMeeting { get { return _NoOfCDCGMeeting; } set { _NoOfCDCGMeeting = value; } }
        public int NoOfTAVisit { get { return _NoOfTAVisit; } set { _NoOfTAVisit = value; } }
        public int NoOfClusterMeeting { get { return _NoOfClusterMeeting; } set { _NoOfClusterMeeting = value; } }
        public int NoOfVisitNodalOfficer { get { return _NoOfVisitNodalOfficer; } set { _NoOfVisitNodalOfficer = value; } }
        public int NoOfStateMeeting { get { return _NoOfStateMeeting; } set { _NoOfStateMeeting = value; } }
        public int NoOfZonalMeeting { get { return _NoOfZonalMeeting; } set { _NoOfZonalMeeting = value; } }
        public int NoOfReviewMeeting { get { return _NoOfReviewMeeting; } set { _NoOfReviewMeeting = value; } }
        public int NoOfVisitVIP { get { return _NoOfVisitVIP; } set { _NoOfVisitVIP = value; } }
        public string Achievements { get { return _Achievements; } set { _Achievements = value; } }
        public string MajorLinkages { get { return _MajorLinkages; } set { _MajorLinkages = value; } }
        public string OtherIssues { get { return _OtherIssues; } set { _OtherIssues = value; } }

        public string UserName { get { return _UserName; } set { _UserName = value; } }
    }
}
