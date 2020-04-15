using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace KursWalut
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Kurs Walut";

            poczatek:

            Console.WriteLine("Kurs Walut");
            Console.WriteLine("----------");
            Console.WriteLine();
            Console.Beep();

            //pobieranie i porządkowanie danych
            System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);

            XDocument xDocument = XDocument.Load(XmlReader.Create("http://www.nbp.pl/kursy/xml/lasta.xml"));

            int i = 0;
            int j = 0;

            string[,] tablicaPozycja = new string[35, 4];

            foreach (XElement hashElement in xDocument.Descendants("nazwa_waluty"))
            {
                string nazwaWaluty = (string)hashElement;
                //Console.WriteLine(hashValue);
                tablicaPozycja[i, 0] = nazwaWaluty;
                i++;
            }
            i = 0;
            foreach (XElement hashElement2 in xDocument.Descendants("przelicznik"))
            {
                string przelicznik = (string)hashElement2;
                tablicaPozycja[i, 1] = przelicznik;
                i++;
            }
            i = 0;
            foreach (XElement hashElement3 in xDocument.Descendants("kod_waluty"))
            {
                string kodWaluty = (string)hashElement3;
                tablicaPozycja[i, 2] = kodWaluty;
                i++;
            }
            i = 0;
            foreach (XElement hashElement4 in xDocument.Descendants("kurs_sredni"))
            {
                string kursSredni = (string)hashElement4;
                tablicaPozycja[i, 3] = kursSredni;
                i++;
            }

            //wyświetlanie wszyskich nazw walut
            for (int q = 0; q < 35; q++)
            {
                Console.WriteLine(q + 1 + " - " + tablicaPozycja[q, 0]);
            }

            //menu wyboru waluty
            wyborWaluty:
            Console.Write("\nWybierz walutę: ");
            string wybors = Console.ReadLine();

            int cleanWybor = 0;
            while (!int.TryParse(wybors, out cleanWybor))
            {
                Console.WriteLine("To jest błędna warość. Proszę wprowadzić poprawną!");
                goto wyborWaluty;
            }
            int wybor = int.Parse(wybors);
            if (wybor > 35 || wybor < 1)
            {
                Console.WriteLine("To jest błędna warość. Proszę wprowadzić poprawną!");
                goto wyborWaluty;
            }
            Console.Clear();

            Console.WriteLine("> "+tablicaPozycja[wybor-1, 0]+" <");
            string nazwa = tablicaPozycja[wybor - 1, 0];
            for (int t = 0; t < nazwa.Length + 4; t++)
            {
                Console.Write("-");
            }
            Console.WriteLine("\n");

            wyborKwoty:
            Console.Write("Wprowadź kwotę: ");
            string kwotas = Console.ReadLine();

            decimal cleanKwota = 0;
            while (!decimal.TryParse(kwotas, out cleanKwota))
            {
                Console.WriteLine("To jest błędna warość. Proszę wprowadzić poprawną!");
                goto wyborKwoty;
            }
            decimal kwota = decimal.Parse(kwotas);
            if (kwota < 0 || kwota == 0)
            {
                Console.WriteLine("To jest błędna warość. Proszę wprowadzić poprawną!");
                goto wyborKwoty;
            }
            decimal kwotaDec = decimal.Parse(tablicaPozycja[wybor - 1, 3]);
            decimal przelicznikDec = decimal.Parse(tablicaPozycja[wybor - 1, 1]);
            Console.WriteLine("\n" + kwota + " " + tablicaPozycja[wybor - 1, 2] + " ====> " + kwotaDec * kwota / przelicznikDec+ " PLN");

            Console.WriteLine("\nNaciśnij 'n' i Enter by zamknąć aplikację, lub dowolny inny klawisz i Enter by kontynuować:");
            if (Console.ReadLine() == "n")
            {
                return;
            }
            else
            {
                Console.Clear();
                goto poczatek;
            }
        }
    }
}