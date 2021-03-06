﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace EvilHangman
{
    public static class GameResources
    {
        public static Canvas GameCanvas { get; set; }
        public static Dimensions GameDimensions { get; set; }

        public static List<string> PossibleWords { get; set; }

        public enum Difficulty { Easy = 5, Medium = 6, Hard = 7 };
        public enum State { Menu = 1, Start = 2, Head = 3, Body = 4, LeftLeg = 5, RightLeg = 6, LeftArm = 7, RightArm = 8, GameOver = 9, GameWon = 10 };

        public static State GameState = State.Menu;

        public static int GuessesLeft { get; set; }
        public static string CurrentWord {get;set;}
        public static int WordLength { get; set; }
        public static string[] CurrentWordState { get; set; }
        public static int SolvedLetters { get; set; }

        public static List<char> GuessedLetters { get; set; }

        public static SolidColorBrush BlackBrush = new SolidColorBrush(Colors.Black);
        public static SolidColorBrush RedBrush = new SolidColorBrush(Colors.Red);
        public static SolidColorBrush GreenBrush = new SolidColorBrush(Colors.Green);
        public static SolidColorBrush BlueBrush = new SolidColorBrush(Colors.Blue);

        static GameResources()
        {
            GameDimensions = new Dimensions();
            GuessedLetters = new List<char>();
        }
    }
}
