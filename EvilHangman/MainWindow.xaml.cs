using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EvilHangmanLibrary;

namespace EvilHangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    struct Dimensions
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public partial class MainWindow : Window
    {
        private string currentWord = String.Empty;
        private int wordLength = 5;
        enum Difficulty { Easy=5, Medium=6, Hard=7};
        enum State { Menu=1, Start=2, Head=3, Body=4, LeftLeg=5, RightLeg=6, LeftArm=7, RightArm=8, GameOver=9, GameWon=10 };

        private State GameState = State.Menu;

        private Dimensions windowDimensions = new Dimensions();

        /*
         * Brushes
         */

        private SolidColorBrush _blackBrush = new SolidColorBrush(Colors.Black);
        private SolidColorBrush _redBrush = new SolidColorBrush(Colors.Red);
        private SolidColorBrush _greenBrush = new SolidColorBrush(Colors.Green);
        private SolidColorBrush _blueBrush = new SolidColorBrush(Colors.Blue);

        public MainWindow()
        {
            InitializeComponent();
            windowDimensions.Width = (int)cvsGameWindow.Width;
            windowDimensions.Height = (int)cvsGameWindow.Height;
            int offset = -50;
            foreach( Difficulty d in Enum.GetValues(typeof(Difficulty)))
            {
                Button btn = new Button();
                btn.Content = d.ToString();
                btn.Width = 60;
                btn.Height = 35;
                btn.Name = "btn" + (int)d;
                Canvas.SetTop(btn, (windowDimensions.Height / 2) + offset);
                Canvas.SetLeft(btn, (windowDimensions.Width / 2) - (btn.Width / 2));
                offset += 50;
                btn.Click +=btn_Click;
                cvsGameWindow.Children.Add(btn);
            }
            
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string name = btn.Name.Replace("btn", "");
            int length = 0;
            bool res = int.TryParse(name, out length);
            if(res)
            {
                initGame(length);
            }
        }

        void window_Resize(object sender, EventArgs e)
        {
            windowDimensions.Width = (int)cvsGameWindow.Width;
            windowDimensions.Height = (int)cvsGameWindow.Height;
            RenderWindow();
        }

        private void initGame(int length)
        {
            cvsGameWindow.Children.Clear();
            wordLength = length;
            var res = Evil.GetWordsForLength(wordLength);
            Random random = new Random();
            int wordNumber = random.Next(0, res.Count() - 1);
            currentWord = res[wordNumber];
            RenderWindow();
        }

        private void DrawNewGameWindow()
        {
            //do the actual Drawing here
            TextBox txtGuess = new TextBox();
            txtGuess.Name = "txtGuess";
            txtGuess.Width = windowDimensions.Width / 2;
            txtGuess.Height = 25;
            Canvas.SetBottom(txtGuess, (2 * 50));
            Canvas.SetRight(txtGuess, windowDimensions.Width / 2 + (-windowDimensions.Width / 6));
            cvsGameWindow.Children.Add(txtGuess);
        }

        private void RenderWindow()
        {
            if(GameState == State.Start)
            {
                DrawNewGameWindow();
                return;
            }
        }

        void guess_btn_Click(object sender, EventArgs e)
        {
            
        }

        void Guess()
        {
            
        }
    }
}
