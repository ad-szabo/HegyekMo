using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HegyekMo
{
    class Program
    {
        static List<Hegycsucs> hegyek = new List<Hegycsucs>();
        static void Main(string[] args)
        {
            //2. Feladat
            fajlBeolvasas();

            //3. Feladat
            Console.WriteLine("3. Feladat: Hegycsúcsok száma: {0} db",hegyek.Count());

            //4. Feladat
            Console.WriteLine("4. Feladat: Hegycsúcsok átlagos magassága: {0} m",hegyek.Average(hegy=>hegy.magassag));

            //5. Feladat
            Hegycsucs legmagasabb = hegyek.Where(hegy => hegy.magassag == hegyek.Max(hegy1 => hegy1.magassag)).First();
            Console.WriteLine("5. Feladat: A legmagasabb hegycsúcs adatai: ");
            Console.WriteLine("\t Név: {0}",legmagasabb.nev);
            Console.WriteLine("\t Hegység: {0}",legmagasabb.hegyseg);
            Console.WriteLine("\t Magasság: {0} m",legmagasabb.magassag);

            //6. Feladat
            Console.Write("6. Feladat: Kérek egy magasságot: ");
            int magassag = int.Parse(Console.ReadLine());
            int darab = hegyek.Where(hegy=>hegy.hegyseg=="Börzsöny" && hegy.magassag>magassag).Count();
            Console.WriteLine($"\t{(darab>0?"Van":"Nincs")} {magassag} m-nél magasabb hegycsúcs a Börzsönyben!");

            //7. Feladat
            Console.WriteLine("7. Feladat: 3000 lábnál magasabb hegycsúcsok száma: {0}",hegyek.Where(hegy=>hegy.lab>3000).Count());

            //8. Feladat
            Console.WriteLine("8. Feladat: Hegység statisztika");
            foreach (var hegyseg in hegyek.Select(hegy => hegy.hegyseg).Distinct())
            {
                Console.WriteLine("\t{0} - {1} db",hegyseg,hegyek.Where(hegy=>hegy.hegyseg==hegyseg).Count());
            }

            //9. Feladat
            Console.WriteLine("9. Feladat: bukk-videk.txt");
            using (StreamWriter f = File.CreateText("bukk-videk.txt"))
            {
                f.WriteLine("Hegycsúcs neve;Magasság láb");
                foreach (var hegycsucs in hegyek.Where(hegy=>hegy.hegyseg== "Bükk-vidék"))
                {
                    f.WriteLine(String.Format("{0};{1:0.#}",hegycsucs.nev,hegycsucs.lab).Replace(',','.'));
                }
            }
            Console.ReadKey();

        }
        static void fajlBeolvasas()
        {
            foreach (var sor in File.ReadAllLines("hegyekMo.txt",Encoding.UTF8).Skip(1))
            {
                var adat = sor.Split(';');
                hegyek.Add(new Hegycsucs
                {
                    nev = adat[0],
                    hegyseg = adat[1],
                    magassag = int.Parse(adat[2])
                });
            } 
        }
    }
    struct Hegycsucs
    {
        public string nev;
        public string hegyseg;
        public int magassag;
        public double lab { get
            {
                return this.magassag * 3.280839895;
            } 
        }
    }
}
