using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KVICClassLIB
{
    public class cluster_prop
    {
        private int _ClusterID;
        private string _ClusterName;
        private string _Address;
        private int _StateID;        
        private int _ImpAgencyID;
        private int _TechAgencyID;

        public int ClusterID { get { return _ClusterID; } set { _ClusterID = value; } }
        public string ClusterName { get { return _ClusterName; } set { _ClusterName = value; } }
        public string Address { get { return _Address; } set { _Address = value; } }
        public int StateID { get { return _StateID; } set { _StateID = value; } }
        public int ImpAgencyID { get { return _ImpAgencyID; } set { _ImpAgencyID = value; } }
        public int TechAgencyID { get { return _TechAgencyID; } set { _TechAgencyID = value; } }
    }
}
