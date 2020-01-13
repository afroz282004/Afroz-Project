using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JVVNLClassLIB
{
    public class cashcollection_Prop
    {
        private int _CounterID;
        private string _BoyName;
        private DateTime _cDate;


        public int CounterID { get { return _CounterID; } set { _CounterID= value; } }
        public string BoyName { get { return _BoyName; } set { _BoyName= value; } }
        public DateTime cDate { get { return _cDate; } set { _cDate= value; } }
    }
}
