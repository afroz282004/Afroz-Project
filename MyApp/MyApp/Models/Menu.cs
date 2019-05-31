using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyApp.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ParentID { get; set; }
    }
}