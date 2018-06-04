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
    public class Terrain
    {
        public Terrain(Canvas c, BitmapImage b, string type, int xPos, int yPos)
        {
            //create img ctrl
            ImageSource imageSource = b;
            Image ctrl = new Image();
            ctrl.Source = imageSource;
            ctrl.ToolTip = "(" + (xPos + 1) + ", " + (yPos + 1) + ")";
            c.Children.Add(ctrl);
            Draw(c, xPos, yPos, ctrl);
        }

        void Draw(Canvas c, int x, int y, Image ctrl)
        {
            Canvas.SetTop(ctrl, y*30);
            Canvas.SetLeft(ctrl, x*30);
            Canvas.SetZIndex(ctrl, 1);
        }
    }
   
}
