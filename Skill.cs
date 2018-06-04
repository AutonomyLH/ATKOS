using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    public class Skill
    {
        public string element;
        public string effectType;
        public int effectStrength;
        public Skill(string e, string eT, int eS)
        {
            element = e;
            effectType = eT;
            effectStrength = eS;
        }
    }
}
