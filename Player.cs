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

    public class Player
    {
        public Player(Canvas c, BitmapImage b, int xPos, int yPos, Image image)
        {
            ImageSource imageSource = b;
            image.Source = imageSource;
            Draw(c, xPos, yPos, image);
        }

        void Draw(Canvas c, int x, int y, Image image)
        {
            Canvas.SetTop(image, y * 30);
            Canvas.SetLeft(image, x * 30);
            Canvas.SetZIndex(image, 2);
        }
    }
}
