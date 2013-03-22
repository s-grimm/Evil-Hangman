using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EvilHangman.Rendering
{
    public static class RenderBodyParts
    {
        private static object mdownref = null;
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
            GameResources.GameCanvas.Children.Clear();

            for (int i = 0; i < GameResources.CurrentWord.Length; ++i)
            {
                GameResources.CurrentWordState[i] = GameResources.CurrentWord[i].ToString();
            }

            UpdateLetters();
            UpdateGuessedLetters();

            Label btn = new Label();
            btn.Content = "Game Over";
            btn.FontFamily = new FontFamily("Rosewood Std");
            btn.FontSize = 60.0;
            btn.Width = 300;
            btn.Height = 100;
            Canvas.SetTop(btn, (GameResources.GameDimensions.Height / 2));
            Canvas.SetLeft(btn, (GameResources.GameDimensions.Width / 2) );

            Label retry = new Label();
            retry.Content = "New Game?";
            retry.FontFamily = new FontFamily("Rosewood Std");
            retry.FontSize = 48.0;
            retry.Width = 300;
            retry.Height = 100;
            Canvas.SetTop(retry, (GameResources.GameDimensions.Height / 2) + 125);
            Canvas.SetLeft(retry, (GameResources.GameDimensions.Width / 2));
            retry.MouseDown += (s, e) =>
            {
                Label lb = s as Label;
                lb.Foreground = GameResources.RedBrush;
                Canvas.SetLeft(s as Label, Canvas.GetLeft(s as Label) + 15);
                mdownref = s;
            };
            retry.MouseUp += Handlers.RetryGameClick;
            retry.MouseLeave += (s, e) =>
            {
                if (mdownref != null && s == mdownref)
                {
                    Label lb = s as Label;
                    lb.Foreground = GameResources.BlackBrush;
                    Canvas.SetLeft(s as Label, Canvas.GetLeft(s as Label) - 15);
                }
                mdownref = null;
            };
            GameResources.GameCanvas.Children.Add(retry);
            GameResources.GameCanvas.Children.Add(btn);
        }

        public static void RenderGameOverWin()
        {
            GameResources.GameCanvas.Children.Clear();
            UpdateLetters();
            UpdateGuessedLetters();

            Label btn = new Label();
            btn.Content = "Winner!";
            btn.FontFamily = new FontFamily("Rosewood Std");
            btn.FontSize = 60.0;
            btn.Width = 300;
            btn.Height = 100;
            Canvas.SetTop(btn, (GameResources.GameDimensions.Height / 2));
            Canvas.SetLeft(btn, (GameResources.GameDimensions.Width / 2));
            Label retry = new Label();
            retry.Content = "New Game?";
            retry.FontFamily = new FontFamily("Rosewood Std");
            retry.FontSize = 48.0;
            retry.Width = 300;
            retry.Height = 100;
            Canvas.SetTop(retry, (GameResources.GameDimensions.Height / 2) + 125);
            Canvas.SetLeft(retry, (GameResources.GameDimensions.Width / 2));
            retry.MouseDown += (s, e) =>
            {
                Label lb = s as Label;
                lb.Foreground = GameResources.RedBrush;
                Canvas.SetLeft(s as Label, Canvas.GetLeft(s as Label) + 15);
                mdownref = s;
            };
            retry.MouseUp += Handlers.RetryGameClick;
            retry.MouseLeave += (s, e) =>
            {
                if (mdownref != null && s == mdownref)
                {
                    Label lb = s as Label;
                    lb.Foreground = GameResources.BlackBrush;
                    Canvas.SetLeft(s as Label, Canvas.GetLeft(s as Label) - 15);
                }
                mdownref = null;
            };
            GameResources.GameCanvas.Children.Add(retry);

            GameResources.GameCanvas.Children.Add(btn);
        }

        public static List<UIElement> LetterList = new List<UIElement>(); 

        public static void UpdateLetters()
        {
            foreach(UIElement uel in LetterList)
            {
                if (GameResources.GameCanvas.Children.Contains(uel))
                    GameResources.GameCanvas.Children.Remove(uel);
            }
            int offset = 25;
            foreach (var letter in GameResources.CurrentWordState)
            {
                Label btn = new Label();
                btn.Content = letter;
                btn.FontFamily = new FontFamily("Rosewood Std");
                btn.FontSize = 60.0;
                btn.Width = 100;
                btn.Height = 100;
                Canvas.SetTop(btn, (GameResources.GameDimensions.Height * .25));
                Canvas.SetLeft(btn, (GameResources.GameDimensions.Width * 0.25) +offset);
                offset += 50;
                LetterList.Add(btn);
                GameResources.GameCanvas.Children.Add(btn);
            }
        }

        public static List<UIElement> GuessedLetterList = new List<UIElement>();

        public static void UpdateGuessedLetters()
        {
            foreach (UIElement uel in LetterList)
            {
                if (GameResources.GameCanvas.Children.Contains(uel))
                    GameResources.GameCanvas.Children.Remove(uel);
            }
            int offset = 25;
            foreach (var letter in GameResources.GuessedLetters)
            {
                Label btn = new Label();
                btn.Content = letter;
                btn.FontFamily = new FontFamily("Rosewood Std");
                btn.FontSize = 55.0;
                btn.Width = 100;
                btn.Height = 100;
                Canvas.SetBottom(btn, (GameResources.GameDimensions.Height * 0.020));
                Canvas.SetLeft(btn, (GameResources.GameDimensions.Width * 0.025) + offset);
                offset += 50;
                GuessedLetterList.Add(btn);
                GameResources.GameCanvas.Children.Add(btn);
            }
        }
    }
}