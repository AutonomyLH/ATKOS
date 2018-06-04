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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Final_Project
{
    public partial class Start : Window
    {
        string ver = "0.15";
        string appName = "ATKOS";
        BitmapImage[] iconset = {
            new BitmapImage(new Uri("./Images/player.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/plains.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/ocean.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/mountain.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/desert.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/valley.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/forest.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/hill.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/island.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/camp.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/town.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/bridgeh.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/bridgev.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/river.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/riverv.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/questalert.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/questcomplete.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/roadh.png", UriKind.Relative)),
            new BitmapImage(new Uri("./Images/roadv.png", UriKind.Relative))
        };
        String[] nameset =
        {
            "player.png",
            "plains.png",
            "ocean.png",
            "mountain.png",
            "desert.png",
            "valley.png",
            "forest.png",
            "hill.png",
            "island.png",
            "camp.png",
            "town.png",
            "bridgeh.png",
            "bridgev.png",
            "river.png",
            "riverv.png",
            "questalert.png",
            "questcomplete.png",
            "roadh.png",
            "roadv.png",
        };
        public Start()
        {
            InitializeComponent();

            this.Title = appName + " - " + ver;
            double imageTotal = iconset.Length;
            for (int i = 0; i < (imageTotal - 1); i++)
            {
                ImageSource imageSource = iconset[i];
                Image ctrl = new Image();
                ctrl.Source = imageSource;
                ctrl.ToolTip = nameset[i];
                canvasDisplay.Children.Add(ctrl);
                Canvas.SetTop(ctrl, 500);
                Canvas.SetLeft(ctrl, i * 30);
                Canvas.SetZIndex(ctrl, 1);
            }
        }

        private void button_QuitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button_StartClick(object sender, RoutedEventArgs e)
        {
            Name win = new Name();
            win.Owner = this;
            win.ShowDialog();

            string playerName = win.playerName;
            this.WindowState = WindowState.Minimized;
            MainWindow mainWin = new MainWindow(playerName);
            mainWin.Owner = this;
            mainWin.WindowState = WindowState.Normal;
            mainWin.ShowDialog();
            this.WindowState = WindowState.Normal;

        }
    }
}
