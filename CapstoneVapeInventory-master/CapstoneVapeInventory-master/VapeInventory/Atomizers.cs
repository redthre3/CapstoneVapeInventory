using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeInventory
{
    class Atomizer
    {
        #region ENUMS
        public enum BuildTypes
        {
            RDA,
            SUB_OHM,
            RTA,
            RDTA,

        }
        public enum DeckTypes
        {
            THREE_POST,
            TWO_POST,
            VELOCITY,
            PETRI,
            PHENOTYPE_L,
            POSTLESS,
            GENESIS,
            T,
            SCREW,
            SPLIT_CENTER,
            MARQUIS,
            MESH


        }
        #endregion

        #region FIELDS
        private string _brand;

        private string _model;

        private int _coilcount;

        private DeckTypes _deck;

        private BuildTypes _type;

        private bool _using;

       

       


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
        public int CoilCount
        {
            get { return _coilcount; }
            set { _coilcount = value; }
        }
        public DeckTypes Deck
        {
            get { return _deck; }
            set { _deck = value; }
        }
        public BuildTypes Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public bool Using
        {
            get { return _using; }
            set { _using = value; }
        }

        #endregion

        #region CONSTRUCTORS
        public Atomizer()
        {

        }
        #endregion

        #region METHODS
        public void DisplayDeckTypes()
        {

            List<String> types = new List<string>();
            types.Add("Three_Post");
            types.Add("Two_Post");
            types.Add("Velocity");
            types.Add("Petri");
            types.Add("Phenotype L");
            types.Add("Postless");
            types.Add("Genesis");
            types.Add("T");
            types.Add("Screw");
            types.Add("Split Center");
            types.Add("Marquis");
            types.Add("Mesh");





            Console.WriteLine("****Deck Type****");
            foreach (string deck in types)
            {
                Console.WriteLine("\t" + deck);
            }



        }
        #endregion











    }

}
