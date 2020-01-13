using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JVVNLClassLIB
{
   public class report_Prop
    {

        private string _SubDivision;
        private string _CounterName;
        private string _fromdate;
        private string _todate;
        private string _UserName;
        private string _UserID;
        private string _Bank;
        private string _selectdate;
        private string _Receiptno;
        private string _ForGrid;
        public string Receiptno { get { return _Receiptno; } set { _Receiptno = value; } }
        public string SubDivision { get { return _SubDivision; } set { _SubDivision = value; } }
        public string CounterName { get { return _CounterName; } set { _CounterName = value; } }

        public string fromdate { get { return _fromdate; } set { _fromdate = value; } }

        public string todate { get { return _todate; } set { _todate = value; } }

        public string UserName { get { return _UserName; } set { _UserName = value; } }
        public string UserId { get { return _UserID; } set { _UserID = value; } }
        public string Bank { get { return _Bank; } set { _Bank = value; } }

        public string selectdate { get { return _selectdate; } set { _selectdate = value; } }
        public string ForGrid { get { return _ForGrid; } set { _ForGrid = value; } }

    }
}
