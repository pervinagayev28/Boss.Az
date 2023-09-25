using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Boss.Az
{
    internal class Worker
    {
        public static int StaticId { get; set; } = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gmail { get; set; }
        public string Paswword { get; set; }
        public void showInfo()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Surname: {Surname}");
        }
        public List<Notfication> Notfications { get; set; }
        public List<CvWorker> CvWorker { get; set; }
        public Worker()
        {
            CvWorker = new List<CvWorker>();
            Notfications = new List<Notfication>();
            Id = StaticId++;
            Name = default;
            Surname = default;
            Gmail = default;
            Paswword = default;
        }

        public Worker(int id, string name, string surname, string gmail, string paswword)
        {
            Id = StaticId++;
            Name = name;
            Surname = surname;
            Gmail = gmail;
            Paswword = paswword;
        }

     
    }
}
