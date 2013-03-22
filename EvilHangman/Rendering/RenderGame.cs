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
    public static class RenderGame
    {
        private static object mdownref;
        public static void RenderStartGame()
        {
            GameResources.GameCanvas.Children.Clear();

            BitmapImage bimg;
            try
            {
                bimg = new BitmapImage(new Uri("Images\\GameBegin.png", UriKind.RelativeOrAbsolute));
                GameResources.GameCanvas.Background = new ImageBrush(bimg);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : Scene", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            TextBox txtGuess = new TextBox();
            txtGuess.Name = "txtGuess";
            txtGuess.Width = GameResources.GameDimensions.Width / 8;
            txtGuess.Height = 60;
            txtGuess.FontFamily = new FontFamily("Rosewood Std");
            txtGuess.FontSize = 60.0;
            txtGuess.MaxLength = 1;
            txtGuess.Opacity = 0.75;
            Canvas.SetBottom(txtGuess, (2 * 50));
            Canvas.SetRight(txtGuess, GameResources.GameDimensions.Width / 2 + (-GameResources.GameDimensions.Width / 6));
            GameResources.GameCanvas.Children.Add(txtGuess);

            Label btn = new Label();
            btn.Content = "Sumbit";
            btn.Height = 60;
            btn.FontFamily = new FontFamily("Rosewood Std");
            btn.FontSize = 60.0;

            Canvas.SetLeft(btn, GameResources.GameDimensions.Width / 2 + (GameResources.GameDimensions.Width / 6));
            Canvas.SetBottom(btn, (2 * 50));
            GameResources.GameCanvas.Children.Add(btn);


            btn.MouseDown += (s, e) =>
            {
                Label lb = s as Label;
                lb.Foreground = GameResources.RedBrush;
                Canvas.SetLeft(s as Label, Canvas.GetLeft(s as Label) + 15);
                mdownref = s;
            };
            btn.MouseUp += Handlers.GuessButtonHandler;
            btn.MouseLeave += (s, e) =>
            {
                if (mdownref != null && s == mdownref)
                {
                    
                    Label lb = s as Label;
                    if (lb.Foreground != GameResources.BlackBrush)
                    {
                        lb.Foreground = GameResources.BlackBrush;
                        Canvas.SetLeft(s as Label, Canvas.GetLeft(s as Label) - 15);
                    }
                }
                mdownref = null;
            };
        }
    }
}
