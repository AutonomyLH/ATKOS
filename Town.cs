using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    public class Town
    {
        static Random random = new Random();

        const string Hall_B = "hall";
        const string Shop_B = "shop";
        const string Tavern_B = "tavern";
        decimal modifier = 0;
        public List<string> buildings = new List<string>();
        public string name;
        public Town()
        {
            AddBuilding(Hall_B);

            if (random.Next(1, 101) > 50)
            {
                AddBuilding(Tavern_B);
            }
            if (random.Next(1, 101) > 50)
            {
                AddBuilding(Shop_B);
            }

            modifier = (random.Next(0, 100) % 100);
            name = NameGenerator();
        }

        public void AddBuilding(string building)
        {
            buildings.Add(building);
        }

        private string NameGenerator()
        {
            string ret = "";
            string[] prefix = {
                "Aber",
                "Ac",
                "Avon",
                "Ar",
                "Ash",
                "Ashe",
                "Ast",
                "Auc",
                "Aucht",
                "Axe",
                "Ay",
                "Bal",
                "Bek",
                "Bean",
                "Berg",
                "Bex",
                "Blen",
                "Bog",
                "Bost",
                "Bourne",
                "Brad",
                "Bre",
                "Bury",
                "By",
                "Carden",
                "Caer",
                "Caster",
                "Cheap",
                "Combe",
                "Coed",
                "Cott",
                "Craig",
                "Cul",
                "Cwe",
                "Dal",
                "Dale",
                "Dean",
                "Den",
                "Din",
                "Dock",
                "Dol",
                "Don",
                "Drum",
                "Dubh",
                "Dum",
                "Dun",
                "Eccles",
                "Eilan",
                "Eidan",
                "Ey",
                "Eg",
                "Ea",
                "Eig",
                "Field",
                "Fin",
                "Frith",
                "Firth",
                "Ford",
                "Fos",
                "Force",
                "Forest",
                "Gate",
                "Garth",
                "Gat",
                "Gill",
                "Glen",
                "Gowt",
                "Ham",
                "Hayes",
                "Hilte",
                "Holm",
                "Hope",
                "Howe",
                "Hurst",
                "Inch",
                "Inner",
                "Keld",
                "Keth",
                "Kil",
                "Kin",
                "King",
                "Kirk",
                "Knock",
                "Kyl",
                "Lhan",
                "Lan",
                "Lang",
                "Law",
                "Low",
                "Leigh",
                "Lea",
                "Lin",
                "Lily",
                "Llyn",
                "Loch",
                "Lough",
                "Magna",
                "Magma",
                "Mawr",
                "Mere",
                "Minister",
                "More",
                "Moss",
                "Mouth",
                "Nan",
                "Nant",
                "Ness",
                "Nor",
                "Pant",
                "Parva",
                "Parven",
                "Pen",
                "Pol",
                "Pont",
                "Pool",
                "Port",
                "Shaw",
                "Shep",
                "Ship",
                "Stan",
                "Stead",
                "Ster",
                "Stoke",
                "Stone",
                "Stow",
                "Strath",
                "Strat",
                "Strathe",
                "Street",
                "Sut",
                "Sud",
                "Swin",
                "Swim",
                "Tarn",
                "Tam",
                "Thorp",
                "Thorn",
                "Thwaite",
                "Tra",
                "Tre",
                "Tilly",
                "Tilli",
                "Tili",
                "Tily",
                "Tilt",
                "Toft",
                "Toft",
                "Treath",
                "Tun",
                "Ton",
                "Uppen",
                "Weald",
                "Wold",
                "Worg",
                "Wog",
                "Wes",
                "Wick",
                "Wech",
                "Wik",
                "Whel",
                "Wil",
                "Wen",
                "Woft",
                "Worthy",
                "Yynis",
            };

            string[] mid =
            {
                "",
                " ",
                "-",
                "s",
                "er",
                "ing",
                "ton",
            };
            if (random.Next(0, 4) != 3)
            {
                for (int i = 0; i < mid.Length; i++)
                {
                    mid[i] = "";
                }
            }

            string[] suffix =
            {
                "wood",
                "ton",
                "ing",
                "ville",
                "burg",
                "grotto",
                "hall",
                "watch",
                "ring",
                "sight",
                "ham",
                "brad",
                "borough",
                "keep",
                "by",
                "worth",
                "cester",
                "ferd",
                "mouth",
                "stead",
                "wich",
                "shire",
                "anton",
            };
            int p = random.Next(prefix.Length);
            int m = random.Next(mid.Length);
            int s = random.Next(suffix.Length);
            // No Duplicates
            if (mid[m] == suffix[s])
            {
                mid[m] = "";
            }
            else if (prefix[p].ToLower() == suffix[s])
            {
                suffix[s] = "on";
            }
            // Remove double s
            if (mid[m] == "s")
            {
                char[] prefixExt = prefix[p].ToCharArray();
                char[] sChar = "s".ToCharArray();
                if (prefixExt[prefixExt.Length - 1].Equals("s"))
                {
                    prefixExt[prefixExt.Length - 1].Equals("");
                }
            }

            if (suffix[s] == "ing")
            {
                char[] prefixExt = prefix[p].ToCharArray();
                char[] eChar = "e".ToCharArray();
                if (prefixExt[prefixExt.Length - 1].Equals("e"))
                {
                    prefixExt[prefixExt.Length - 1].Equals("");
                }
            }
            // Capitalize suffix
            if (mid[m] == "-" || mid[m] == " ")
            {
                char[] suffixExt = suffix[s].ToCharArray();
                suffixExt[0] = char.ToUpper(suffixExt[0]);
                suffix[s] = new string(suffixExt);
            }
            ret = prefix[p] + mid[m] + suffix[s];
            return ret;
        }
    }
}
