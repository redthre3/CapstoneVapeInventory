using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeInventory
{
    class Mods
    {
        #region ENUMS
        public enum BatteryAccess
        {
            TRAP_DOOR,
            MAGNETIC_PLATE,
            SCREW_BASE,
            SEALED
        }
        #endregion

        #region FIELDS
        private string _brand;
        private string _model;
        private int _battQTY;
        private double _wattageMAX;
        private bool _supportTC;
        private bool _using;
        private bool _regulated;
        private BatteryAccess _access;

        











        #endregion

        #region PROPERTIES
        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
        public int BattQTY
        {
            get { return _battQTY; }
            set { _battQTY = value; }
        }
        public double WattMax
        {
            get { return _wattageMAX; }
            set { _wattageMAX = value; }
        }
        public bool TC
        {
            get { return _supportTC; }
            set { _supportTC = value; }
        }
        public bool Using
        {
            get { return _using; }
            set { _using = value; }
        }
        public bool Regulated
        {
            get { return _regulated; }
            set { _regulated = value; }
        }
        public BatteryAccess Access
        {
            get { return _access; }
            set { _access = value; }
        }

        #endregion

        #region CONSTRUCTORS
        public Mods()
        {

        }
        #endregion

        #region METHODS
        public void DisplayBatteryAccessType()
        {
            List<String> battAccesses = new List<string>();
            battAccesses.Add("Trap Door");
            battAccesses.Add("Magnetic Plate");
            battAccesses.Add("Screw Base");
            battAccesses.Add("Sealed");

            Console.WriteLine("****Battery Access Type****");
            foreach (string battAccess in battAccesses)
            {
                Console.WriteLine("\t" + battAccess);
            }




        }

          


        #endregion




    }
}
