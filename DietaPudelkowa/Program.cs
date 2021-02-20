using DietaPudelkowa.Models;
using DietaPudelkowa.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietaPudelkowa
{
    class Program
    {
        static void GetProducts(IEnumerable<Product> items)
        {

        }

        static void Main(string[] args)
        {
            DataParsers _parser = new DataParsers();
            var items = _parser.GetAllProducts();
            int sumCam = 0;
            bool wege = true;

            Console.WriteLine("DIETA PUDEŁKOWA\n");
            Console.WriteLine("Co chcesz zrobić (wpisz a lub b): ");
            Console.Write("a - zamówić dietę\nb - wyświetlić dostępne składniki\n");

            char info = Console.ReadKey().KeyChar;

            while(info!='b' && info != 'a')
            {
                Console.WriteLine("\n\nPodałeś błędne dane.\nCo chcesz zrobić (wpisz a lub b): ");
                Console.Write("a - zamówić dietę\nb - wyświetlić dostępne składniki\n");

                info = Console.ReadKey().KeyChar;
            }

            if(info == 'b')
            {
                Console.WriteLine("\nSkładniki:\n\n");
                _parser.ShowAllProducts();
            }

            if(info=='a')
            {
                Console.WriteLine("\n\nProszę podać wartość kaloryczną diety:\na. 1000 cal,\nb. 1500 cal,\nc. 2000 cal,\nd. 2500 cal,\ne. 3000 cal\n");

                info = Console.ReadKey().KeyChar;

                switch(info)
                {
                    case 'a':
                        sumCam = 1000;
                        break;
                    case 'b':
                        sumCam = 1500;
                        break;
                    case 'c':
                        sumCam = 2000;
                        break;
                    case 'd':
                        sumCam = 2500;
                        break;
                    case 'e':
                        sumCam = 3000;
                        break;
                    default:
                        Console.WriteLine("\nPodano błędny znak.\n");
                        break;
                }

                Console.WriteLine("\nProszę podać czy dieta ma być wegetariańska: tak(T), nie(N)\n");

                info = Console.ReadKey().KeyChar;

                if (info == 't')
                    wege = true;
               else if (info == 'n')
                    wege = false;

                _parser.GenerateDiet(sumCam / 3, wege);
            }


            

            Console.ReadKey();
        }
    }
}
