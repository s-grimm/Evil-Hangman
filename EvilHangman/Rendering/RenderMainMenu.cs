using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EvilHangman.Rendering
{
    public static class RenderMainMenu
    {
        private static object mdownref = null;

        public static void RenderNewGameMenu()
        {
            GameResources.GameCanvas.Children.Clear();
            int offset = 75;

            Image img = new Image();
            BitmapImage bimg;
            try
            {
                bimg = new BitmapImage(new Uri("Images\\Menu.png", UriKind.RelativeOrAbsolute));
                img.Source = bimg;
                img.Width = bimg.PixelWidth;
                img.Height = bimg.PixelHeight;
                GameResources.GameCanvas.Background = new ImageBrush(bimg);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : MainMenuBackground.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            foreach (GameResources.Difficulty d in Enum.GetValues(typeof(GameResources.Difficulty)))
            {
                Label btn = new Label();
                btn.Content = d.ToString();
                btn.FontFamily = new FontFamily("Rosewood Std");
                btn.FontSize = 60.0;
                btn.Width = 250;
                btn.Height = 60;
                btn.Name = "btn" + (int)d;
                Canvas.SetTop(btn, (GameResources.GameDimensions.Height / 2) + offset);
                Canvas.SetLeft(btn, (GameResources.GameDimensions.Width / 2) - (btn.Width ));
                offset += 50;
                btn.MouseDown += (s, e) => {
                    Label lb = s as Label;
                    lb.Foreground = GameResources.RedBrush;
                    Canvas.SetLeft(s as Label, Canvas.GetLeft(s as Label) + 15);
                    mdownref = s;
                };
                btn.MouseUp += Handlers.NewGameButtonClick;
                btn.MouseLeave += (s, e) => {
                    if (mdownref != null && s == mdownref)
                    {
                        Label lb = s as Label;
                        lb.Foreground = GameResources.BlackBrush;
                        Canvas.SetLeft(s as Label, Canvas.GetLeft(s as Label) - 15);
                    }
                    mdownref = null;
                };
                GameResources.GameCanvas.Children.Add(btn);
            }
        }
    }
}
