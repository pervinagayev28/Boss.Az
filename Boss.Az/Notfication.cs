using Boss.Az.ManageDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.Az
{
    public class Notfication
    {
        public string About { get; set; }
        public int Id { get; set; }      
        public DateTime NodedDate { get; set; }
        public int By { get; set; }
        public Notfication()
        {
            Id = default;
            About = default;
            NodedDate = default;
            By = default;
        }
        public Notfication(string About, int By,int Id)
        {
            this.Id = Id;
            this.About = About;
            this.By = By;
            NodedDate = DateTime.Now;
        }
        public void ShowNotfics(int check)
        {
            Console.WriteLine($"About: {About}");
            Console.WriteLine("By Name: " +
                (check == 1 ? Database.Employers.FirstOrDefault(w => w.Id == By).CompanyName : Database.Workers.FirstOrDefault(e => e.Id == By).Name));
            Console.WriteLine($"Noded: {NodedDate.ToShortDateString()}");
        }
    }
}
