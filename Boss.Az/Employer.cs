using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Boss.Az
{
    internal class Employer
    {
        private static int StaticId { get; set; } = 1;
        public static int StaticIdCv { get; set; } = 1;
        public int Id { get; set; }
        public string Gmail { get; set; }
        public string password { get; set; }
        public string CompanyName { get; set; }

        public List<Notfication> Notfications { get; set; }
        public CvEmployer CvEmployer { get; set; }
        public Employer()
        {
            Id = StaticId++;
            Notfications = new List<Notfication>();
            CvEmployer = new CvEmployer();
        }

   
        
    }
}
