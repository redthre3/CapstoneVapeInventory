using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VapeInventory
{
    class Program
    {
        //*****************************************************************\\
        //Developer: Philip Lindsay                                        \\
        //Program: Vape\Ecig Database                                      \\
        //Date: 9 DEC 2018                                                 \\
        //*****************************************************************\\
                     
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
            string dataPath;
            while (running)
            {
                DisplayHeader("Main Menu");

                Console.WriteLine("Please select an option below:");
                Console.WriteLine("\t1)View Devices.");
                Console.WriteLine("\t2)Add Device.");
                Console.WriteLine("\t3)Edit Device.");
                Console.WriteLine("\t4)Remove Device.");
                Console.WriteLine("\t5)Open Device Lists.");
                Console.WriteLine("\t6)Save Device Lists.");
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
                        mod = DisplayModOrAtty("open");
                        if (mod)
                        {
                            dataPath = @"Data\mods.csv";
                            mods = DisplayOpenMods(dataPath);

                        }
                        else
                        {
                            dataPath = @"Data\attys.csv";
                            atomizers = DisplayOpenAtty(dataPath);
                        }
                        break;

                    case "6":
                        mod = DisplayModOrAtty("save");
                        if (mod)
                        {
                            dataPath = @"Data\mods.csv";
                            DisplaySaveMods(mods, dataPath);

                        }
                        else
                        {
                            dataPath = @"Data\attys.csv";
                            DisplaySaveAtty(atomizers, dataPath);
                        }
                        break;
                    case "e":
                        running = false;
                        break;
                    default:
                        break;
                }
            }

        }

        static void DisplaySaveAtty(List<Atomizer> atomizers, string dataPath)
        {
            string characterString;

            List<string> characterStringList = new List<string>();

            //
            // build the list to write to the text file line by line
            //
            foreach (Atomizer atomizer in atomizers)
            {
                characterString =
                    atomizer.Brand + "," +
                    atomizer.Model + "," +
                    atomizer.Deck + "," +
                    atomizer.Type + "," +
                    atomizer.CoilCount + "," +
                    atomizer.Using + ",";


                characterStringList.Add(characterString);
            }

            //
            // write the list of strings (characters) to the data file
            //
            try
            {
                File.WriteAllLines(dataPath, characterStringList);
                Console.WriteLine("Atomizer list stored to {0}", dataPath);
                DisplayContinuePrompt();
            }
            catch (Exception) // throw any exception up to the calling method
            {
                throw;
            }
        }

        static void DisplaySaveMods(List<Mods> mods, string dataPath)
        {
            string characterString;

            List<string> characterStringList = new List<string>();

            //
            // build the list to write to the text file line by line
            //
            foreach (Mods mod in mods)
            {
                characterString =
                    mod.Brand + "," +
                    mod.Model + "," +
                    mod.BattQTY + "," +
                    mod.Access + "," +
                    mod.Regulated + "," +
                    mod.TC + "," +
                    mod.WattMax + "," +
                    mod.Using + ",";

                characterStringList.Add(characterString);
            }

            //
            // write the list of strings (characters) to the data file
            //
            try
            {
                File.WriteAllLines(dataPath, characterStringList);
                Console.WriteLine("Mod List saved to {0}", dataPath);
                DisplayContinuePrompt();
            }
            catch (Exception) // throw any exception up to the calling method
            {
                throw;
            }


        }

        static List<Atomizer> DisplayOpenAtty(string dataPath)
        {

            const char delineator = ',';

            List<string> attyStringList = new List<string>();
           Atomizer tempAtty = new Atomizer();
            List<Atomizer> tempAttys = new List<Atomizer>();

            //
            // read each line and put it into an array and convert the array to a list
            //
            try
            {
                attyStringList = File.ReadAllLines(dataPath).ToList();
            }
            catch (Exception) // throw any exception up to the calling method
            {
                throw;
            }

            //
            // create character object for each line of data read and fill in the property values
            //
            foreach (string modString in attyStringList)
            {
                tempAtty = new Atomizer();

                // use the Split method and the delineator on the array to separate each property into an array of properties
                string[] properties = modString.Split(delineator);

                tempAtty.Brand = properties[0];
                tempAtty.Model = properties[1];
                tempAtty.Deck = (Atomizer.DeckTypes)Enum.Parse(typeof(Atomizer.DeckTypes), properties[2]);
                tempAtty.Type = (Atomizer.BuildTypes)Enum.Parse(typeof(Atomizer.BuildTypes), properties[3]);
                tempAtty.CoilCount = Convert.ToInt32(properties[4]);
                tempAtty.Using = Convert.ToBoolean(properties[5]);

                tempAttys.Add(tempAtty);
            }
            Console.WriteLine("Atomizer List loaded from file, {0}.", dataPath);
            DisplayContinuePrompt();
            return tempAttys;
        }

        static List<Mods> DisplayOpenMods(string dataPath)
        {

            const char delineator = ',';

            List<string> modStringList = new List<string>();
            Mods tempMod = new Mods(); 
            List <Mods> tempMods = new List<Mods>();

            //
            // read each line and put it into an array and convert the array to a list
            //
            try
            {
                modStringList = File.ReadAllLines(dataPath).ToList();
            }
            catch (Exception) // throw any exception up to the calling method
            {
                throw;
            }

            //
            // create character object for each line of data read and fill in the property values
            //
            foreach (string modString in modStringList)
            {
                tempMod = new Mods();
                
                // use the Split method and the delineator on the array to separate each property into an array of properties
                string[] properties = modString.Split(delineator);

                tempMod.Brand = properties[0];
                tempMod.Model = properties[1];
                tempMod.BattQTY = Convert.ToInt32(properties[2]);
                tempMod.Access = (Mods.BatteryAccess)Enum.Parse(typeof(Mods.BatteryAccess), properties[3]);
                tempMod.Regulated = Convert.ToBoolean(properties[4]);
                tempMod.TC = Convert.ToBoolean(properties[5]);
                tempMod.WattMax = Convert.ToDouble(properties[6]);
                tempMod.Using = Convert.ToBoolean(properties[7]);

                tempMods.Add(tempMod);
            }
            Console.WriteLine("Mod List loaded from file, {0}.",dataPath);
            DisplayContinuePrompt();
            return tempMods;
        }

        private static void DisplayRemoveAtty(List<Atomizer> atomizers)
        {
            string model;
            Console.WriteLine("BRAND__________MODEL_____");
            foreach (Atomizer atomizer in atomizers)
            {
                Console.WriteLine(atomizer.Brand.PadRight(15) + " " + atomizer.Model.PadRight(10));
            }
            Console.WriteLine();
            Console.WriteLine("Enter a model to view details: ");
            model = Console.ReadLine();

            foreach (Atomizer atomizer in atomizers)
            {
                if (model == atomizer.Model)
                {
                    atomizers.Remove(atomizer);

                    Console.WriteLine("The atomizer {0} has been removed from the inventory.", atomizer.Model);
                    break;
                }
                else
                {
                    Console.WriteLine("It seems you havent entered the right model.");
                }

            }
            DisplayContinuePrompt();

        }

        private static void DisplayRemoveMods(List<Mods> mods)
        {
            string model;
            DisplayHeader("Remove Mod");
            foreach (Mods mod in mods)
            {
                Console.WriteLine(mod.Brand.PadRight(15) + " " + mod.Model);
            }
            Console.WriteLine();
            Console.WriteLine("Enter a model to remove:");
            model = Console.ReadLine();
            foreach (Mods mod in mods)
            {
                if (model == mod.Model)
                {


                    mods.Remove(mod);

                    Console.WriteLine("The mod {0} has been removed from the inventory.", mod.Model);



                    break;
                }
                else
                {
                    Console.WriteLine("It seems you havent entered the right model.");
                }

            }
            DisplayContinuePrompt();

        }

        private static void DisplayEditAtty(List<Atomizer> atomizers)
        {
            string model;
            DisplayHeader("DisplayEditAtty Atomizer");
            Console.WriteLine("BRAND__________MODEL_____");
            foreach (Atomizer atomizer in atomizers)
            {
                Console.WriteLine(atomizer.Brand.PadRight(15) + " " + atomizer.Model.PadRight(10));
            }
            Console.WriteLine();
            Console.WriteLine("Enter a model to view details: ");
            model = Console.ReadLine();

            foreach (Atomizer atomizer in atomizers)
            {
                if (model == atomizer.Model)
                {
                    atomizers.Remove(atomizer);
                    int intVal;


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
                    Console.WriteLine("The atomizer {0} has been updated in the inventory.", atomizer.Model);
                    break;
                }
                else
                {
                    Console.WriteLine("It seems you havent entered the right model.");
                }

            }
            DisplayContinuePrompt();
        }

        private static void DisplayEditMods(List<Mods> mods)
        {
            string model;
            int intVal;
            double dblVal;
            DisplayHeader("Edit Mod.");
            foreach (Mods mod in mods)
            {
                Console.WriteLine(mod.Brand.PadRight(15) + " " + mod.Model);
            }
            Console.WriteLine();
            Console.WriteLine("Enter a model to edit details:");
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

                    Console.WriteLine();
                    mods.Remove(mod);
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
                        Console.WriteLine("Failed to enter valid response");
                        mod.TC = false;
                    }

                    mods.Add(mod);

                    Console.WriteLine("The mod {0} has been updated to the inventory.", mod.Model);



                    break;
                }
                else
                {
                    Console.WriteLine("It seems you havent entered the right model.");
                }

            }
            DisplayContinuePrompt();

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
            Console.Write("Enter Access Type:");
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
            Console.WriteLine("BRAND__________MODEL_____");
            foreach (Atomizer atomizer in atomizers)
            {
                Console.WriteLine(atomizer.Brand.PadRight(15) + " " + atomizer.Model.PadRight(10));
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
            Console.WriteLine("BRAND__________MODEL_____");
            foreach (Mods mod in mods)
            {
                Console.WriteLine(mod.Brand.PadRight(15) + " " + mod.Model);
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
