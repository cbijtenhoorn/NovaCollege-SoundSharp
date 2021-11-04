using System;
using System.Collections.Generic;

namespace SoundSharp
{
    class Program
    {
        public enum InlogStatus
        {
            Ok,
            NietOk,
            Onbekend,
        }

        public static InlogStatus LogIn()
        {
            InlogStatus status = InlogStatus.Onbekend;

            Console.WriteLine("Wat is uw naam?");
            string name = Console.ReadLine();
            Console.WriteLine("Wat is uw wachtwoord?");

            int i = 1;
            int max = 3;
            while (i <= max)
            {
                if (i > 1)
                {
                    Console.WriteLine($"Poging {i} van {max}");
                    if (i == max)
                    {
                        Console.WriteLine("LET OP: Laatste poging!");
                    }
                }

                string password = Console.ReadLine();
                switch (password)
                {
                    case "SOUNDSHARP":
                    case "":
                        status = InlogStatus.Ok;
                        Device.Initialize();
                        i += max;
                        break;
                    default:
                        status = InlogStatus.NietOk;
                        i++;
                        break;
                }
            }

            string result = (status == InlogStatus.Ok)
                ? $"Welkom bij SoundSharp, {name}"
                : "Wachtwoord is onjuist.";
            Console.WriteLine(result);

            return status;
        }

        static void ShowMenu()
        {
            Console.WriteLine("Menu:\n1. Overzicht mp3 spelers \n2. Overzicht voorraad \n3. Muteer voorraad \n" +
                              "4. Statistieken \n5. Toevoegen mp3 speler \n8. Toon menu \n9. Exit");

            bool isTrue = true;
            while (isTrue)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                char value = input.KeyChar;

                switch (value)
                {
                    case '1':
                        Device.PrintDevices();
                        break;

                    case '2':
                        Device.PrintStock();
                        break;

                    case '3':
                        Device.StockMutate();
                        break;

                    case '4':
                        Device.Statistics();
                        break;

                    case '5':
                        Device.AddMp3();
                        break;

                    case '8':
                        ShowMenu();
                        break;

                    case '9':
                        Console.WriteLine("Exit.");
                        isTrue = !isTrue;
                        break;

                    default:
                        Console.WriteLine("Geen actie mogelijk, kies een andere optie. (Kies 8 voor menu)");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
           
            if (args.Length == 2 && args[0] == "admin" && args[1] == "SOUNDSHARP")
            {
                Device.Initialize();
                Device.PrintDevices();
                Device.PrintStock();
                Device.Statistics();
            }
            else
            {
               InlogStatus status = LogIn();
               if (status == InlogStatus.Ok)
               {
                   ShowMenu();
               }
            }
        }
    }
}