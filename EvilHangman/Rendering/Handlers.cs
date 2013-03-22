using EvilHangmanLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace EvilHangman.Rendering
{
    public static class Handlers
    {
        public static void NewGameButtonClick(object sender, EventArgs e)
        {
            Label btn = sender as Label;
            string name = btn.Name.Replace("btn", "");
            int length = 0;
            bool res = int.TryParse(name, out length);
            if (res)
            {
                var word = Evil.GetWordsForLength(length);
                Random random = new Random();
                int wordNumber = random.Next(0, word.Count() - 1);
                GameResources.CurrentWord = word[wordNumber];
                GameResources.GuessesLeft = 6;
                GameResources.WordLength = length;
                GameResources.CurrentWordState = new string[length];
                GameResources.SolvedLetters = 0;
                for (int i = 0; i < length; ++i)
                {
                    GameResources.CurrentWordState[i] = "_";
                }

                RenderGame.RenderStartGame();
                RenderBodyParts.UpdateLetters();
            }
        }

        public static void GuessButtonHandler(object sender, EventArgs e)
        {
            TextBox box = null;
            foreach (UIElement el in GameResources.GameCanvas.Children)
            {
                if (el.GetType() == typeof(TextBox) && ((TextBox)el).Name == "txtGuess")
                {
                    box = el as TextBox;
                }
            }

            if (box == null || box.Text.Trim() == "") return; //empty!

            char letter = box.Text.ToLower().ToCharArray()[0];

            GameResources.GuessedLetters.Add(letter);

            if (GameResources.CurrentWord.ToLower().Contains(letter))
            {
                //our word contains the letter!
                string cWord = GameResources.CurrentWord.ToLower();
                int offset = 0;
                while (cWord.Contains(letter))
                {
                    GameResources.SolvedLetters++;
                    int x = cWord.IndexOf(letter);
                    GameResources.CurrentWordState[x+offset] = letter.ToString();
                    if (x == cWord.Length - 1) break;
                    else
                    {
                        offset = x;
                        cWord = cWord.Substring(x + 1);
                    }
                } 
            }
            else
            {
                GameResources.GuessesLeft--;
                if (GameResources.GuessesLeft == 0)
                {
                    //game over
                    RenderBodyParts.RenderScene();
                    RenderBodyParts.RenderGameOver();
                }
                else
                {
                    RenderBodyParts.RenderScene();
                }
            }

            RenderBodyParts.UpdateLetters();

            if (GameResources.SolvedLetters == GameResources.WordLength)
            {
                //render winning scene!
            }
            else
            {
                box.Clear();
                
            }
        }
    }
}
