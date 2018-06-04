using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    // item, quantity, quantitymax
    public class ItemStack
    {
        public Item item;
        public int quantity;
        public int max;
        public ItemStack(Item i, int q, int qm)
        {
            item = i;
            quantity = q;
            max = qm;
        }
    }
}
