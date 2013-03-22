using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EvilHangman.Rendering
{
    public static class RenderBodyParts
    {
        public static void RenderScene()
        {
            //GameResources.GuessesLeft
            BitmapImage bimg;
            try
            {
                switch (GameResources.GuessesLeft)
                {
                    case 5:
                        bimg = new BitmapImage(new Uri("Images\\GameHead.png", UriKind.RelativeOrAbsolute));
                        break;

                    case 4:
                        bimg = new BitmapImage(new Uri("Images\\GameBody.png", UriKind.RelativeOrAbsolute));
                        break;

                    case 3:
                        bimg = new BitmapImage(new Uri("Images\\GameOneLeg.png", UriKind.RelativeOrAbsolute));
                        break;

                    case 2:
                        bimg = new BitmapImage(new Uri("Images\\GameTwoLeg.png", UriKind.RelativeOrAbsolute));
                        break;

                    case 1:
                        bimg = new BitmapImage(new Uri("Images\\GameOneArm.png", UriKind.RelativeOrAbsolute));
                        break;

                    case 0:
                        bimg = new BitmapImage(new Uri("Images\\GameTwoArm.png", UriKind.RelativeOrAbsolute));
                        break;

                    default:
                        bimg = new BitmapImage(new Uri("Images\\GameBegin.png", UriKind.RelativeOrAbsolute));
                        break;
                }

                GameResources.GameCanvas.Background = new ImageBrush(bimg);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : Scene", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void RenderGameOver()
        {
        }
    }
}