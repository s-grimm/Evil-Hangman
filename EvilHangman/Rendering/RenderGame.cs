using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace EvilHangman.Rendering
{
    public static class RenderGame
    {
        public static void RenderStartGame()
        {
            GameResources.GameCanvas.Children.Clear();

            TextBox txtGuess = new TextBox();
            txtGuess.Name = "txtGuess";
            txtGuess.Width = GameResources.GameDimensions.Width / 2;
            txtGuess.Height = 25;
            Canvas.SetBottom(txtGuess, (2 * 50));
            Canvas.SetRight(txtGuess, GameResources.GameDimensions.Width / 2 + (-GameResources.GameDimensions.Width / 6));
            GameResources.GameCanvas.Children.Add(txtGuess);

            Button btn = new Button();
            btn.Content = "Guess";
        }
    }
}
