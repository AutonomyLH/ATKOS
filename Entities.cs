using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Final_Project
{

    public class Entities
    {
        public int objLevel;
        public int objCon;
        public int objStr;
        public int objAgi;
        public int objInt;
        public int objDef;
        public int objCha;
        public string objName;
        public List<Skill> Objskills;
        public Entities(int level, int con, int str, int agi, int inte, int def, int cha, string name, List<Skill> skills)
        {
            objLevel = level;
            objCon = con;
            objStr = str;
            objAgi = agi;
            objInt = inte;
            objDef = def;
            objCha = cha;
            objName = name;
            Objskills = skills;
        }
    }
}
