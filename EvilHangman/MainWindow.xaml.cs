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
    public class Dimensions
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GameResources.GameDimensions.Width = (int)cvsGameWindow.Width;
            GameResources.GameDimensions.Height = (int)cvsGameWindow.Height;
            GameResources.GameCanvas = cvsGameWindow;
            Rendering.RenderMainMenu.RenderNewGameMenu();
        }
    }
}
