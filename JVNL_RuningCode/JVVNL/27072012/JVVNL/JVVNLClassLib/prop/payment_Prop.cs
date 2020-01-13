using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JVVNLClassLIB
{
    public class payment_Prop
    {
        private int _BillID;
        private int _PaymentID;
        private string _Mode;
        private double _Amount;
        private int _Bankid;
        private string _ChequeDate;
        private string _ChequeNo;
        private string _PhoneNo;
        private string _SubdivisionCode;
        private string _AccountNo;
        private string _Name;
        private string _username;
        private string _fromdate;
        private string _todate;
        private string _emailid;
        private int _status;
        private string _receiptno;
        private string _BillIDs;

        public int BillID{get {return _BillID;}set{_BillID= value;}}
        public int PaymentID { get { return _PaymentID; } set { _PaymentID= value; } }
        public string Mode { get { return _Mode; } set { _Mode= value; } }
        public double Amount { get { return _Amount; } set { _Amount= value; } }
        public int Bankid { get { return _Bankid; } set { _Bankid= value; } }
        public string  ChequeDate { get { return _ChequeDate; } set { _ChequeDate = value; } }
        public string ChequeNo { get { return _ChequeNo; } set { _ChequeNo= value; } }

        public string Receiptno { get { return _receiptno; } set { _receiptno = value; } }


        public string PhoneNo { get { return _PhoneNo; } set { _PhoneNo = value; } }
        public string SubdivisionCode { get { return _SubdivisionCode; } set { _SubdivisionCode= value; } }
        public string AccountNo { get { return _AccountNo; } set { _AccountNo= value; } }
        public string Name { get { return _Name; } set { _Name= value; } }
        public string UserName { get { return _username; } set { _username = value; } }
        public string Fromdate { get { return _fromdate; } set { _fromdate= value; } }
        public string Todate { get { return _todate; } set { _todate= value; } }
        public string Emailid { get { return _emailid; } set { _emailid = value; } }
        public string BillIDs { get { return _BillIDs; } set { _BillIDs = value; } }
        public int status { get { return _status; } set { _status= value; } }

    }
}
