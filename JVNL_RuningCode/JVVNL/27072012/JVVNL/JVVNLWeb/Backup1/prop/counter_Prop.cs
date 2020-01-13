using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JVVNLClassLIB
{
    public class counter_Prop
    {
        private int _CounterId;
        private string _CounterName;
        private string _Address;
        private string _ContactPerson;
        private string _ContactNo;
        

        public int Counterid{get {return _CounterId;}set{_CounterId= value;}}
        public string CounterName { get { return _CounterName; } set { _CounterName= value; } }
        public string Address { get { return _Address; } set { _Address= value; } }
        public string ContactPerson { get { return _ContactPerson; } set { _ContactPerson= value; } }
        public string ContactNo { get { return _ContactNo; } set { _ContactNo= value; } }
    }
}
