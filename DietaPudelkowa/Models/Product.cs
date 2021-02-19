using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace DietaPudelkowa.Models
{
    public class Product
    {
        public string Nazwa { get; set; }
        public string Rodzaj { get; set; }
        public int Cal { get; set; }
        public int Cena { get; set; }
        public bool Wege { get; set; }
    }
}
