using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.Az
{
    internal class Notfication
    {
        public string About { get; set; }
        public DateTime NodedDate { get; set; }
        public string ByName { get; set; }
        public Notfication(string About,string Byname)
        {
            this.About = About;
            this.ByName = Byname;
            NodedDate = DateTime.Now;
        }
    }
}
