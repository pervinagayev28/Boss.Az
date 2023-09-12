using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.Az
{
    internal class CvEmployer
    {
        public int Id { get; set; }
        public string  WorkArea { get; set; }
        public int RequiredWorkExperience { get; set; }
        public string RequiredLanguage { get; set; }
        public int Salary { get; set; }
        public void ShowInfo()
        {
            Console.WriteLine("Id: "+Id);
            Console.WriteLine("work area: "+WorkArea);
            Console.WriteLine("work experince: "+RequiredWorkExperience);
            Console.WriteLine("Required Language: "+RequiredLanguage);
            Console.WriteLine("Salary: "+Salary);
        }
        public CvEmployer()
        {
            Id = Employer.StaticIdCv++;
            WorkArea = default;
            RequiredWorkExperience = default;
            RequiredLanguage = default;
            Salary = default;
        }

        public CvEmployer(int id, string workArea, int requiredWorkExperience, string requiredLanguage, int salary)
        {
            Id = Employer.StaticIdCv++ ;
            WorkArea = workArea;
            RequiredWorkExperience = requiredWorkExperience;
            RequiredLanguage = requiredLanguage;
            Salary = salary;
        }
    }
}
