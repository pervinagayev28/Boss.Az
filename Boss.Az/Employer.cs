using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Boss.Az
{
    internal class Employer:Person
    {
        public override Notfication Notfications { get; set; }
        public Employer(string name, string surName, string gmail)
            : base(name, surName, gmail) { }
    }
}
