using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace EvilHangman.Rendering
{
    public static class RenderMainMenu
    {
        public static void RenderNewGameMenu()
        {
            GameResources.GameCanvas.Children.Clear();
            int offset = -50;
            foreach (GameResources.Difficulty d in Enum.GetValues(typeof(GameResources.Difficulty)))
            {
                Button btn = new Button();
                btn.Content = d.ToString();
                btn.Width = 60;
                btn.Height = 35;
                btn.Name = "btn" + (int)d;
                Canvas.SetTop(btn, (GameResources.GameDimensions.Height / 2) + offset);
                Canvas.SetLeft(btn, (GameResources.GameDimensions.Width / 2) - (btn.Width / 2));
                offset += 50;
                btn.Click += Handlers.NewGameButtonClick;
                GameResources.GameCanvas.Children.Add(btn);
            }
        }
    }
}
