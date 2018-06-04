using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    public class Reward
    {
        public Item item;
        public int gold;
        public bool HasItem;
        public Reward(Item i, int g, bool HasItem)
        {
            item = i;
            gold = g;
            this.HasItem = HasItem;
        }
    }
}