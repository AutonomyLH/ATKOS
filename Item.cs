using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    // name, description, value, type, intensity
    public class Item
    {
        public string name;
        public string description;
        public int value;
        public string type;
        public int intensity;
        public Item(string n, string d, int v, string t, int i)
        {
            name = n;
            description = d;
            value = v;
            type = t;
            intensity = i;
        }
    }
}
