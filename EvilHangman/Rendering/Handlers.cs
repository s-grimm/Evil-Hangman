using EvilHangmanLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace EvilHangman.Rendering
{
    public class LetterPos
    {
        public string letter { get; set; }

        public int position { get; set; }

        public LetterPos()
        {
            letter = "";
        }
    }

    public static class Handlers
    {
        #region Check If there is no  Alphabet and no non-Alphanumeric //character

        public static bool CheckAlphabet(string text)
        {
            Regex pattern = new Regex(@"^[a-zA-Z]+$");

            return pattern.IsMatch(text);
        }

        #endregion Check If there is no  Alphabet and no non-Alphanumeric //character

        public static void RetryGameClick(object sender, EventArgs e)
        {
            Rendering.RenderMainMenu.RenderNewGameMenu();
        }

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

                GameResources.GuessedLetters = new List<char>();

                for (int i = 0; i < length; ++i)
                {
                    GameResources.CurrentWordState[i] = "_";
                }
                GameResources.PossibleWords = word;
                RenderGame.RenderStartGame();
                RenderBodyParts.UpdateLetters();
            }
        }

        public static void GuessButtonHandler(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            if (lb.Foreground != GameResources.BlackBrush)
            {
                lb.Foreground = GameResources.BlackBrush;
                Canvas.SetLeft(sender as Label, Canvas.GetLeft(sender as Label) - 15);
            }
            TextBox box = null;
            foreach (UIElement el in GameResources.GameCanvas.Children)
            {
                if (el.GetType() == typeof(TextBox) && ((TextBox)el).Name == "txtGuess")
                {
                    box = el as TextBox;
                }
            }

            if (box == null || box.Text.Trim() == "" || !CheckAlphabet(box.Text.Trim()))
            {
                box.Clear();
                return; //empty!
            }
            char letter = box.Text.ToLower().ToCharArray()[0];
            if (!GameResources.GuessedLetters.Contains(letter))
            {
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
                        GameResources.CurrentWordState[x + offset] = letter.ToString();
                        if (x == cWord.Length - 1) break;
                        else
                        {
                            offset = x + 1;
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
                RenderBodyParts.UpdateGuessedLetters();
                RenderBodyParts.UpdateLetters();

                List<LetterPos> lpList = new List<LetterPos>();
                for (int i = 0; i < GameResources.CurrentWordState.Length; ++i)
                {
                    if (GameResources.CurrentWordState[i] == "_") continue;

                    LetterPos lp = new LetterPos();
                    lp.letter = GameResources.CurrentWordState[i];
                    lp.position = i;
                    lpList.Add(lp);
                }

                List<string> newWordList = new List<string>();
                foreach (string word in GameResources.PossibleWords)
                {
                    string modWord = word;
                    bool isValid = true;
                    int offset = 0;
                    foreach (var lp in lpList)
                    {
                        if (word[lp.position].ToString() != lp.letter)
                        {
                            isValid = false;
                            break;
                        }
                        else
                        {
                            modWord = modWord.Remove(lp.position - offset, 1);
                            ++offset;
                        }
                    }
                    if (isValid)
                    {
                        bool isSuperValid = true;
                        foreach (var let in GameResources.GuessedLetters)
                        {
                            if (modWord.Contains(let))
                            {
                                isSuperValid = false;
                                break;
                            }
                        }
                        if (isSuperValid)
                        {
                            newWordList.Add(word);
                        }
                    }
                }
                GameResources.PossibleWords = newWordList;
                if (GameResources.PossibleWords.Count > 1)
                {
                    Random random = new Random();
                    int wordNumber = random.Next(0, GameResources.PossibleWords.Count - 1);
                    GameResources.CurrentWord = GameResources.PossibleWords[wordNumber];
                }
            }
            if (GameResources.SolvedLetters == GameResources.WordLength)
            {
                //render winning scene!
                RenderBodyParts.RenderGameOverWin();
            }
            else
            {
                box.Clear();
            }
        }
    }
}