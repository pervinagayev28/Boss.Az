﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.Az
{
    public class Notfication
    {
        public string About { get; set; }
        public DateTime NodedDate { get; set; }
        public int By { get; set; }
        public Notfication()
        {
            About = default;
            NodedDate = default;
            By = default;
        }
        public Notfication(string About,int By)
        {
            this.About = About;
            this.By = By;
            NodedDate = DateTime.Now;
        }
        public  void ShowNotfics()
        {
            Console.WriteLine($"About: {About}");
            Console.WriteLine($"By Name: ");
            Console.WriteLine($"Noded: {NodedDate.ToShortDateString()}");
        }
    }
}
