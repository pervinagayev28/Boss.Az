using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Boss.Az
{
    internal class CvWorker
    {
        public CvWorker()
        {
            Id = StaticId++ ;
            Birthdate = default;
            Gender = default;
            MaritalStatus = default;
            WorkExperince = default;
            ProfessionKind = default;
            WantsAmount = default;
        }
        public CvWorker( DateTime birthdate,
            int gender, int maritalStatus, int workExperince, string professionKind, int wantsAmount)
        {
            Id = StaticId++;
            Birthdate = birthdate;
            Gender = gender;
            MaritalStatus = maritalStatus;
            WorkExperince = workExperince;
            ProfessionKind = professionKind;
            WantsAmount = wantsAmount;
        }
        public void showInfo()
        {
            Console.WriteLine("Id: " + Id);
            Console.WriteLine("Birth date: " + Birthdate.ToShortTimeString());
            Console.WriteLine(Gender == 1 ? "Gender: Male" : "Gender: Female");
            Console.WriteLine(MaritalStatus == 1 ? "marital status: married" : "marital status: single");
            Console.WriteLine("work experince: " + WorkExperince);
            Console.WriteLine("Profession: " + ProfessionKind);
            Console.WriteLine("wants amount: " + WantsAmount);
        }

        private static int StaticId { get; set; }
        public int Id { get; set; }
        public DateTime Birthdate { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public int WorkExperince { get; set; }
        public string ProfessionKind { get; set; }
        public int WantsAmount { get; set; }


    }
}
