using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KVICClassLIB
{
    public class users_Prop
    {
        private string _UserName;
        private string _Password;
        private string _FirstName;
        private string _LastName;
        private bool _IsActive;
        private int _ClusterID;
        private string _ClusterName;
        private int _StateID;
        private int _GroupID;
        private string _GroupName;
        private string _phoneno;
        private string _emailid;

        public string UserName{get {return _UserName;}set{_UserName = value;}}
        public string Password { get { return _Password; } set { _Password = value; } }
        public string FirstName { get { return _FirstName; } set { _FirstName = value; } }
        public string LastName { get { return _LastName; } set { _LastName = value; } }      
        public bool IsActive {get { return _IsActive;    } set { _IsActive = value; } }
        public int ClusterID { get { return _ClusterID; } set { _ClusterID = value; } }
        public string ClusterName { get { return _ClusterName; } set { _ClusterName = value; } }
        public int StateID { get { return _StateID; } set { _StateID = value; } }
        public int GroupID   {get { return _GroupID;     } set { _GroupID = value;  } }
        public string GroupName { get { return _GroupName; } set { _GroupName= value; } }
        public string PhoneNo { get { return _phoneno; } set { _phoneno= value; } }
        public string EmailID { get { return _emailid; } set { _emailid= value; } }
    }
}
