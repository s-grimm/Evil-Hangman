using EvilHangmanLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace EvilHangman.Rendering
{
    public static class Handlers
    {
        public static void NewGameButtonClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string name = btn.Name.Replace("btn", "");
            int length = 0;
            bool res = int.TryParse(name, out length);
            if (res)
            {
                var word = Evil.GetWordsForLength(length);
                Random random = new Random();
                int wordNumber = random.Next(0, word.Count() - 1);
                GameResources.CurrentWord = word[wordNumber];
                GameResources.GuessesLeft = 5;
                GameResources.WordLength = length;

                RenderGame.RenderStartGame();
            }
        }

        public static void GuessButtonHandler(object sender, EventArgs e)
        {
            
        }
    }
}
