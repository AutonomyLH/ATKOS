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
    public partial class Name : Window
    {

        public string playerName;

        public Name()
        {
            InitializeComponent();
        }

        public void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            inputName.IsEnabled = false;
            if (inputName.Text.Contains(" "))
            {
                inputName.Text.Replace(" ", "_");
            }
            else if (inputName.Text.Contains("​"))
            {
                inputName.Text.Replace("​","");
            }
            if (inputName.Text.Length == 0)
            {
                inputName.Text = "Player";
            }
            playerName = inputName.Text;
            Close();
        }

        private void inputName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (inputName.Text.Contains(" "))
            {
                inputName.Text = inputName.Text.Remove(inputName.Text.Length - 1);
                inputName.Text += "_";
                inputName.SelectionStart = inputName.Text.Length;
            }
            else if (inputName.Text.Contains("​"))
            {
                inputName.Text = inputName.Text.Remove(inputName.Text.Length - 1);
                inputName.SelectionStart = inputName.Text.Length;
            }
        }
    }
}
