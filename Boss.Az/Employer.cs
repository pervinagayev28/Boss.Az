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
        public void showInfo()
        {
            Console.WriteLine("company name: "+CompanyName);
        }
        public static int StaticId { get; set; } 
        public int Id { get; set; }
        public string Gmail { get; set; }
        public string password { get; set; }
        public string CompanyName { get; set; }

        public List<Notfication> Notfications { get; set; }
        public List<CvEmployer> CvEmployer { get; set; }
        public Employer()
        {
            Id = ++StaticId;
            Notfications = new List<Notfication>();
            CvEmployer = new List<CvEmployer>();
        }

   
        
    }
}
