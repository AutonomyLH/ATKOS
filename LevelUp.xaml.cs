using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Final_Project
{
    /// <summary>
    /// Interaction logic for LevelUp.xaml
    /// </summary>
    public partial class LevelUp : Window
    {
        SolidColorBrush newGreen = new SolidColorBrush(Color.FromRgb(255,45,124));
        public int con;
        int conBase;
        public int str;
        int strBase;
        public int agi;
        int agiBase;
        public int inte;
        int intBase;
        public int def;
        int defBase;
        public int cha;
        int chaBase;
        public int pTotal;
        public int pRem;
        int level;
        public LevelUp(int constitution, int strength, int agility, int intelligence, int defense, int charisma, int remPoints, int alevel)
        {
            InitializeComponent();
            textConstitution.ToolTip = "Constitution is responsible for your health and how much stamina you lose when acting";
            textStrength.ToolTip = "Strength is responsible for melee damage and what kind of equipment you can wear";
            textAgility.ToolTip = "Agility is responsible for your stamina, ranged damage, and what kind of weaponry you can use";
            textIntelligence.ToolTip = "Intelligence is responsible for your magic, and what power your spells have";
            textDefense.ToolTip = "Defense is responsible for how much damage you take, primarily is given to the player by equipment";
            textCharisma.ToolTip = "Charisma is responsible for lowering shop prices, and allows you to coerce people into doing certain things";
            level = alevel;
            con = constitution;
            conBase = con;
            str = strength;
            strBase = str;
            agi = agility;
            agiBase = agi;
            inte = intelligence;
            intBase = inte;
            def = defense;
            defBase = def;
            cha = charisma;
            chaBase = cha;
            pRem = remPoints + 2;
            pTotal = pRem;
            UpdateText();
        }

        private void UpdateText()
        {
            conDisplay.Content = con.ToString();
            strDisplay.Content = str.ToString();
            agiDisplay.Content = agi.ToString();
            intDisplay.Content = inte.ToString();
            defDisplay.Content = def.ToString();
            chaDisplay.Content = cha.ToString();
            textRemaining.Text = "Points Remaining: " + pRem;
            textSpent.Text = "Points Total: " + pTotal;
            textInfo.Text = "You are now level " + level + "!" + Environment.NewLine + "Allocate your stat points!";


            if (pRem <= 0)
            {
                textRemaining.Foreground = new SolidColorBrush(Color.FromRgb(121, 0, 0));
            }
            else
            {
                textRemaining.Foreground = new SolidColorBrush(Color.FromRgb(45, 124, 0));
            }
        }

        private bool NotZero(string stat, int change)
        {
            if ((pRem + change >= 0) && (change != -1) && (pRem != 0))
            {
                switch (stat)
                {
                    case "constitution":
                        if ((con + change) >= conBase)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case "strength":
                        if ((str + change) >= strBase)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case "agility":
                        if ((agi + change) >= agiBase)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case "intelligence":
                        if ((inte + change) >= intBase)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case "defense":
                        if ((def + change) >= defBase)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case "charisma":
                        if ((cha + change) >= chaBase)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    default:
                        return false;
                }
            }
            else if (change == -1 && stat == "constitution")
            {
                if (con > 0)
                {
                    return true;
                }
                return false;
            }
            else if (change == -1 && stat == "strength")
            {
                if (str > 0)
                {
                    return true;
                }
                return false;
            }
            else if (change == -1 && stat == "agility")
            {
                if (agi > 0)
                {
                    return true;
                }
                return false;
            }
            else if (change == -1 && stat == "intelligence")
            {
                if (inte > 0)
                {
                    return true;
                }
                return false;
            }
            else if (change == -1 && stat == "defense")
            {
                if (def > 0)
                {
                    return true;
                }
                return false;
            }
            else if (change == -1 && stat == "charisma")
            {
                if (cha > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonConPlus_Click(object sender, RoutedEventArgs e)
        {
            if (NotZero("constitution", 1))
            {
                con += 1;
                pRem -= 1;
                UpdateText();
            }
        }

        private void buttonConMin_Click(object sender, RoutedEventArgs e)
        {
            if (NotZero("constitution", -1))
            {
                con -= 1;
                pRem += 1;
                UpdateText();
            }
        }

        private void buttonStrPlus_Click(object sender, RoutedEventArgs e)
        {
            if (NotZero("strength", 1))
            {
                str += 1;
                pRem -= 1;
                UpdateText();
            }
        }

        private void buttonStrMin_Click(object sender, RoutedEventArgs e)
        {
            if (NotZero("strength", -1))
            {
                str -= 1;
                pRem += 1;
                UpdateText();
            }
        }

        private void buttonAgiPlus_Click(object sender, RoutedEventArgs e)
        {
            if (NotZero("agility", 1))
            {
                agi += 1;
                pRem -= 1;
                UpdateText();
            }
        }

        private void buttonAgiMin_Click(object sender, RoutedEventArgs e)
        {
            if (NotZero("agility", -1))
            {
                agi -= 1;
                pRem += 1;
                UpdateText();
            }
        }

        private void buttonIntPlus_Click(object sender, RoutedEventArgs e)
        {
            if (NotZero("intelligence", 1))
            {
                inte += 1;
                pRem -= 1;
                UpdateText();
            }
        }

        private void buttonIntMin_Click(object sender, RoutedEventArgs e)
        {
            if (NotZero("intelligence", -1))
            {
                inte -= 1;
                pRem += 1;
                UpdateText();
            }
        }

        private void buttonDefMax_Click(object sender, RoutedEventArgs e)
        {
            if (NotZero("defense", 1))
            {
                def += 1;
                pRem -= 1;
                UpdateText();
            }
        }

        private void buttonDefMin_Click(object sender, RoutedEventArgs e)
        {
            if (NotZero("defense", -1))
            {
                def -= 1;
                pRem += 1;
                UpdateText();
            }
        }

        private void buttonChaPlus_Click(object sender, RoutedEventArgs e)
        {
            if (NotZero("charisma", 1))
            {
                cha += 1;
                pRem -= 1;
                UpdateText();
            }
        }

        private void buttonChaMin_Click(object sender, RoutedEventArgs e)
        {
            if (NotZero("charisma", -1))
            {
                cha -= 1;
                pRem += 1;
                UpdateText();
            }
        }
    }
}
