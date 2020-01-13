using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JVVNLClassLIB
{
    public class users_Prop
    {
        private string _UserName;
        private string _Password;
        private string _FirstName;
        private string _LastName;
        private bool _IsActive;
        private int _CounterID;
        private int _GroupID;
        private string _GroupName;
        private string _CounterName;
        private string _subdivisioncode;
        private string _accountno;
        private string _phoneno;
        private string _emailid;
        private string _IPAddress;

        public string UserName{get {return _UserName;}set{_UserName = value;}}
        public string Password { get { return _Password; } set { _Password = value; } }
        public string FirstName { get { return _FirstName; } set { _FirstName = value; } }
        public string LastName { get { return _LastName; } set { _LastName = value; } }      
        public bool IsActive {get { return _IsActive;    } set { _IsActive = value; } }    
        public int CounterID {get { return _CounterID;   } set { _CounterID = value;} }       
        public int GroupID   {get { return _GroupID;     } set { _GroupID = value;  } }
        public string GroupName { get { return _GroupName; } set { _GroupName= value; } }
        public string CounterName { get { return _CounterName; } set { _CounterName = value; } }
        public string SubdivisionCode { get { return _subdivisioncode; } set { _subdivisioncode= value; } }
        public string AccountNo { get { return _accountno; } set { _accountno= value; } }
        public string PhoneNo { get { return _phoneno; } set { _phoneno= value; } }
        public string EmailID { get { return _emailid; } set { _emailid= value; } }
        public string IPAddress { get { return _IPAddress; } set { _IPAddress= value; } }
    }
}
