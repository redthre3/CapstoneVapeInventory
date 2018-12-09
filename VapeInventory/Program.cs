using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeInventory
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayOpeningScreen();
            DisplayMenu();
            DisplayClosingScreen();



        }

        static void DisplayMenu()
        {
            List<Atomizer> atomizers = new List<Atomizer>();
            List<Mods> mods = new List<Mods>();
            bool running = true;
            bool mod = false;

            while (running)
            {
                DisplayHeader("Main Menu");

                Console.WriteLine("Please select an option below:");
                Console.WriteLine("\t1)View Devices.");
                Console.WriteLine("\t2)Add Device.");
                Console.WriteLine("\t3)Edit Device.");
                Console.WriteLine("\t4)Remove Device.");
                Console.WriteLine("\t5)Pair Device.");
                Console.WriteLine("\t6)Unpair Devices.");
                Console.WriteLine("\tE)Exit.");

                switch (Console.ReadLine().ToLower())
                {
                    case "1":
                        mod = DisplayModOrAtty("view");
                        running = true;
                        if (mod)
                        {
                            DisplayViewMods(mods);

                        }
                        else
                        {
                            DisplayViewAtty(atomizers);
                        }
                        break;
                    case "2":
                        running = true;
                        mod = DisplayModOrAtty("add");
                        if (mod)
                        {
                            DisplayAddMods(mods);

                        }
                        else
                        {
                            DisplayAddAtty(atomizers);
                        }
                        break;
                    case "3":
                        running = true;
                        mod = DisplayModOrAtty("edit");
                        if (mod)
                        {
                            DisplayEditMods(mods);

                        }
                        else
                        {
                            DisplayEditAtty(atomizers);
                        }
                        break;
                    case "4":
                        mod = DisplayModOrAtty("remove");
                        if (mod)
                        {
                            DisplayRemoveMods(mods);

                        }
                        else
                        {
                            DisplayRemoveAtty(atomizers);
                        }
                        break;
                    case "5":

                    case "6":

                    default:
                        break;
                }
            }













        }

        private static void DisplayRemoveAtty(List<Atomizer> atomizers)
        {

        }

        private static void DisplayRemoveMods(List<Mods> mods)
        {
            throw new NotImplementedException();
        }

        private static void DisplayEditAtty(List<Atomizer> atomizers)
        {
            throw new NotImplementedException();
        }

        private static void DisplayEditMods(List<Mods> mods)
        {
            throw new NotImplementedException();
        }

        private static void DisplayAddAtty(List<Atomizer> atomizers)
        {
            int intVal;
            Atomizer atomizer = new Atomizer();
            DisplayHeader("Add Atomizer.");
            Console.Write("Enter Brand name:");
            atomizer.Brand = Console.ReadLine().ToUpper();
            Console.Write("Enter Model:");
            atomizer.Model = Console.ReadLine().ToUpper();
            Console.Write("Enter Atomizer Type:");
            Enum.TryParse(Console.ReadLine().Replace(" ", "_"), out Atomizer.BuildTypes buildType);
            atomizer.Type = buildType;
            atomizer.DisplayDeckTypes();
            Console.WriteLine("Enter Deck Type:");
            Enum.TryParse(Console.ReadLine().Replace(" ", "_").ToUpper(), out Atomizer.DeckTypes deckType);
            atomizer.Deck = deckType;
            Console.Write("How many coils?");
            int.TryParse(Console.ReadLine(), out intVal);
            atomizer.CoilCount = intVal;
            atomizers.Add(atomizer);

            Console.WriteLine("The atomizer {0} has been added to the inventory.", atomizer.Model);

            DisplayContinuePrompt();
            //change test 2



        }

        private static void DisplayAddMods(List<Mods> mods)
        {
            int intVal;
            double dblVal;
            Mods mod = new Mods();
            DisplayHeader("Add Mod.");
            Console.Write("Enter Brand name:");
            mod.Brand = Console.ReadLine().ToUpper();
            Console.Write("Enter Model:");
            mod.Model = Console.ReadLine().ToUpper();
            Console.Write("Enter the number of batteries:");
            int.TryParse(Console.ReadLine(), out intVal);
            mod.BattQTY = intVal;
            mod.DisplayBatteryAccessType();
            Console.Write("Enter Access Type");
            Enum.TryParse(Console.ReadLine().ToUpper().Replace(" ", "_"), out Mods.BatteryAccess batteryAccess);
            mod.Access = batteryAccess;
            Console.Write("Enter maximum wattage:");
            double.TryParse(Console.ReadLine(), out dblVal);
            mod.WattMax = dblVal;
            Console.Write("Is the device regulated?");
            if (Console.ReadLine().ToLower() == "yes")
            {
                mod.Regulated = true;
            }
            else if (Console.ReadLine().ToLower() == "no")
            {
                mod.Regulated = false;
            }
            else
            {
                Console.WriteLine("failed to enter valid response");
                mod.Regulated = false;
            }
            Console.Write("Does device support Temp Control?");
            if (Console.ReadLine().ToLower() == "yes")
            {
                mod.TC = true;
            }
            else if (Console.ReadLine().ToLower() == "no")
            {
                mod.TC = false;
            }
            else
            {
                Console.WriteLine("failed to enter valid response");
                mod.TC = false;
            }
            
            mods.Add(mod);

            Console.WriteLine("The mod {0} has been added to the inventory.", mod.Model);

            DisplayContinuePrompt();
        }

        private static void DisplayViewAtty(List<Atomizer> atomizers)
        {
            string model;
            DisplayHeader("Atomizer List");
            foreach (Atomizer atomizer in atomizers)
            {
                Console.WriteLine(atomizer.Brand + " " + atomizer.Model);
            }
            Console.WriteLine();
            Console.WriteLine("Enter a model to view details:");
            model = Console.ReadLine();
            foreach (Atomizer atomizer in atomizers)
            {
                if (model == atomizer.Model)
                {
                    Console.WriteLine("Brand: " + atomizer.Brand);
                    Console.WriteLine("Model: " + atomizer.Model);
                    Console.WriteLine("Type: " + atomizer.Type);
                    Console.WriteLine("Deck: " + atomizer.Deck);
                    Console.WriteLine("Coils: " + atomizer.CoilCount);
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.WriteLine("It seems you havent entered the right model.");
                }

            }
            DisplayContinuePrompt();
        }

        private static void DisplayViewMods(List<Mods> mods)
        {
            string model;
            DisplayHeader("Mod List.");
            foreach (Mods mod in mods)
            {
                Console.WriteLine(mod.Brand + mod.Model);
            }
            Console.WriteLine();
            Console.WriteLine("Enter model name to view details:");
            Console.WriteLine();
            model = Console.ReadLine();
            foreach (Mods mod in mods)
            {
                if (model == mod.Model)
                {
                    Console.WriteLine("Brand: " + mod.Brand);
                    Console.WriteLine("Model: " + mod.Model);
                    Console.WriteLine("Batt QTY: " + mod.BattQTY);
                    Console.WriteLine("Batt Access: " + mod.Access);
                    Console.WriteLine("Regulated: " + mod.Regulated);
                    Console.WriteLine("TC Support: " + mod.TC);
                    Console.WriteLine("Max Wattage: " + mod.WattMax);
                    Console.WriteLine("In Use:     " + mod.Using);
                    Console.WriteLine();
                    break;



                }
                else
                {
                    Console.WriteLine("It seems you havent entered the right model.");
                }
            }

            DisplayContinuePrompt();




        }

        static bool DisplayModOrAtty(string v)
        {
            bool mod = false;
            bool validResponse = false;
            DisplayHeader("Which List?");
            while (!validResponse)
            {
                Console.WriteLine("Select which list you want to {0}.", v);
                Console.WriteLine();
                Console.WriteLine("\t1 Mods.");
                Console.WriteLine("\t2 Atomizers.");
                switch (Console.ReadLine())
                {
                    case "1":
                        mod = true;
                        validResponse = true;
                        break;
                    case "2":
                        mod = false;
                        validResponse = true;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid response.");
                        validResponse = false;
                        break;
                }
            }

            return mod;
        }
















        #region HELPER METHODS
        /// <summary>
        /// display opening screen
        /// </summary>
        static void DisplayOpeningScreen()
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("\t\tWelcome to Vape/E-cig Inventory 1.0");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("\t\tThanks for using Vape/E-cig Inventory");
            Console.WriteLine();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display header
        /// </summary>
        static void DisplayHeader(string headerTitle)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerTitle);
            Console.WriteLine();
        }

        #endregion

    }
}
