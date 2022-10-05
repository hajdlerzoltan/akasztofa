using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akasztofa
{
    class Program
    {
        static public string[] abc = { "a, b, c, d, e, f, g, h, i, j, k, l, m, n, ny, o, p, q, r, s, t, u, v, w, x, y, z,"};
        //tömb ami tartalmazza az abc-t és ezekből fogunk betüket válogatni
        static public int eletek = 10; // életek száma ami ha elfogy vége a játéknak
        static public List<string> szavak = new List<string>(); //A fileból olvasott szavakat ebben tároljuk
        static public bool gamestate = true; //játék futásáért felelő válzotó
        static public bool ischarin = true;//betü ismétlésért felelő bool

        static void Main(string[] args)
        {
            Fileread();
            
            var randomszo = new Random().Next(szavak.Count()); //kiválaszt egy random szót egy listából
            var szo = szavak[randomszo];// kiválasztott random szó tárolója
            char[] megoldas = new char[szo.Length];// Ebben a tömben vannak a megoldás szónak a betűi

            Console.WriteLine("/////////////////////////////////////////////////////");//UI
            Console.SetCursorPosition(Console.WindowWidth / 2, 2);
            Console.WriteLine($"eletek számas: {eletek}"); // játékos életeinek száma

            do
            {
                bool repet = false; // betü ismétlés ellenörző bool

                Console.SetCursorPosition(0, 2);
                Console.WriteLine("Adj meg egy betüt!:");
                Console.SetCursorPosition(0, 3);
                
                var betuinput = Console.ReadLine();
                bool inputin = false;

                try
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2, 3);
                    Console.WriteLine("                                     ");
                    for (int i = 0; i < szo.Length; i++)
                    {
                        
                        if (szo[i] == Convert.ToChar(betuinput))
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 2, 3);
                            Console.WriteLine($"a(z) /{betuinput}/ jó megoldás!");
                            ischarin = true;
                            inputin = true;
                            if (Convert.ToChar(betuinput) == megoldas[i])
                            {
                                repet = true;
                            }
                            megoldas.SetValue(Convert.ToChar(betuinput), i);
                            Console.SetCursorPosition(i, 4);
                            Console.Write(szo[i]);
                        }
                        else
                        {
                            ischarin = false;
                        }

                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Vagy üres volt, vagy több betűt írtál be");
                }

                Console.SetCursorPosition(20, 2);
                Console.WriteLine("                    ");
                Console.SetCursorPosition(Console.WindowWidth / 2, 2);
                Console.WriteLine($"eletek számas: {eletek}");
                Console.SetCursorPosition(0, 3);
                Console.Write(" ");

                string megoldas1 = new string(megoldas); //a helyes megoldások tömbjének elemeit itt rakja össze a program

                if (repet != true)
                {
                    Console.SetCursorPosition(20, 2);
                    Console.WriteLine("                    ");
                }
                else
                {
                    Console.SetCursorPosition(20, 2);
                    Console.WriteLine("ez a betü már volt");
                }


                if (ischarin == false && inputin == false)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2, 3);
                    Console.WriteLine($"a(z) /{betuinput}/ rossz megoldás!");
                    eletek--;
                    Console.SetCursorPosition(Console.WindowWidth / 2, 2);
                    Console.WriteLine($"eletek számas: {eletek}");
                }

                if (szo == megoldas1)
                {
                    Console.SetCursorPosition(0, 5);
                    Console.WriteLine("nyertel!");
                    gamestate = false;
                }
                if (eletek == 0)
                {
                    Console.WriteLine("vesztettél!");
                    Console.SetCursorPosition(0, 4);
                    Console.WriteLine("A megoldeás ez volt: " + szo);
                    gamestate = false;
                }
            } while (gamestate == true);

            
            Console.WriteLine("/////////////////////////////////////////////////////");//UI
            Console.ReadKey();
        }
        static void Fileread() // egy szavak.txt file-ból olvassa be a szavakat amiket a játék felhasznál
        {
            try
            {
                using (StreamReader sr = new StreamReader("szavak.txt"))
                {
                    string line = string.Empty;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        szavak.Add(line);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
