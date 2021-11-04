using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace SoundSharp
{
    public class Device
    {
        public static List<Device> Stats = new List<Device>();
        public int ID;
        public string Make;
        public string Model;
        public int MBSize;
        public double Price;
        public int Voorraad;

        public static void Initialize()
        {
            Stats.Add(new Device(1, "GET technologies.inc", "HF 410",4096, 129.95, 500));
            Stats.Add(new Device(2, "Far & Loud", "XM 600", 8192, 224.95, 500));
            Stats.Add(new Device(3, "Innotivative", "Z3", 512, 79.95, 500));
            Stats.Add(new Device(4, "Resistance S.A.", "3001", 4096, 124.95, 500));
            Stats.Add(new Device(5, "CBA", "NXT Volume", 2048, 159.05, 500));
        }

        public static void StockMutate()
        {
            try
            {
                Console.WriteLine("Voer ID van MP3 speler in:");
                int IDinput = int.Parse(Console.ReadLine());

                if (IDinput > Stats.Count || IDinput <= 0)
                {
                    Console.WriteLine("ID niet aanwezig");
                    StockMutate();
                }
                else
                {
                    Console.WriteLine("Voer de mutatie van de voorraad in:");
                    int mutatie = int.Parse(Console.ReadLine());

                    foreach (Device mp3 in Stats)
                    {
                        if (mp3.ID == IDinput)
                        {
                            if ((mp3.Voorraad + mutatie) >= 0)
                            {
                                mp3.Voorraad += mutatie;
                                Console.WriteLine("Mutatie succesvol!");
                            }
                            else
                            {
                                Console.WriteLine("Mutatie niet uitgevoerd: voorraad mag niet negatief worden.");
                                StockMutate();
                            }
                        }
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine($"Error, voer een getal in.");
                StockMutate();
            }
        }

        public static void Statistics()
        {
            int stocktotal = Stats.Sum(x => x.Voorraad);
            double stockvalue = Stats.Sum(x => x.Price * x.Voorraad);
            double valueaverage = Stats.Average(x => x.Price);
            Device bestvalue = Stats.MinBy(x => x.Price / x.MBSize).First();

            Console.WriteLine($"Het totale aantal mp3 spelers dat in voorraad is: {stocktotal}");
            Console.WriteLine($"De totale waarde van de voorraad mp3 spelers is: ${stockvalue:0.00}");
            Console.WriteLine($"De gemiddelde prijs van een mp3 speler bij SoundSharp is: ${valueaverage:0.00}");
            Console.WriteLine($"De mp3 speler met de beste prijs per mB is: {bestvalue.Make} {bestvalue.Model}");
        }

        public static void AddMp3()
        {
            try
            {
                Console.WriteLine("Voer het merk van de mp3 speler in:");
                string merk = Console.ReadLine();
                Console.WriteLine("Voer het model van de mp3 speler in:");
                string model = Console.ReadLine();
                Console.WriteLine("Voer de opslagcapaciteit van de mp3 speler in:");
                int opslag = int.Parse(Console.ReadLine());
                Console.WriteLine("Voer de prijs van de mp3 speler in:");
                double prijs = double.Parse(Console.ReadLine());
                Console.WriteLine("Voer de voorraad van de mp3 speler in:");
                int voorraad = int.Parse(Console.ReadLine());

                Stats.Add(new Device(Stats.Count + 1, merk, model, opslag, prijs, voorraad));
                Console.WriteLine("Nieuwe mp3 speler toegevoegd!");
            }
            catch (Exception)
            {
                Console.WriteLine("Foutieve invoer.");
                AddMp3();
            }
        }
        
            

        public Device()
        {
        }

        public Device(int ID, string Make, string Model, int MBSize, double Price, int Voorraad)
        {
            this.ID = ID;
            this.Make = Make;
            this.Model = Model;
            this.MBSize = MBSize;
            this.Price = Price;
            this.Voorraad = Voorraad;
        }

        public static void PrintDevices()
        {
            foreach (Device mp3 in Stats)
            {
                Console.WriteLine($"\nID: {mp3.ID} \nMake: {mp3.Make} \nModel: {mp3.Model} \nMBSize: {mp3.MBSize}MB \nPrice: ${mp3.Price}");
            }
        }

        public static void PrintStock()
        {
            foreach (Device mp3 in Stats)
            {
                Console.WriteLine($"\nID: {mp3.ID} \nVoorraad: {mp3.Voorraad}");
            }
        }
    }
}