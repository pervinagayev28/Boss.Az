using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.Az
{
    internal abstract class Person
    {
        private static int StaticId { get; set; }
        public Person()
        {
            Id = 0;
            Name = default;
            Surname = default;
            Gmail = default;
            Paswword = default;
        }
        protected Person(string name, string surname, string gmail,string Paswword)
        {
            Id = StaticId++;
            Name = name;
            Surname = surname;
            Gmail = gmail;
            this.Paswword = Paswword;
        }
        abstract public Notfication Notfications { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Paswword { get; set; }
        public string Surname { get; set; }
        public string Gmail { get; set; }
        public void showInfo()
        {
            Console.WriteLine("Id: " + Id);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Surname: " + Surname);
            Console.WriteLine("Gmail: " + Gmail);
        }
    }
}
