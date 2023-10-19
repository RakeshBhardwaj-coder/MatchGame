using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

// above called directives
//namespace keyword
namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBlock firstTextBlock;
        bool getFirstTextBlock;

        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        int winPoint = 8;

        static float lowestTime=1000;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timerTextBlock.Text = (tenthsOfSecondsElapsed/10F).ToString("0.0s");
           
            if(matchesFound == winPoint)
            {
                timer.Stop();
                timerTextBlock.Text = timerTextBlock.Text + " : PlayAgain ▶️";
                UpdateScore(tenthsOfSecondsElapsed/10F);
            }
        }
        public void UpdateScore(float score)
        {
           
            if(lowestTime>score)
            {
                lowestTime = score;
                LowestTime.Text = "Lowest : " + lowestTime.ToString("0.0s");
            }
            else
            {
            }
        }

        public void SetUpGame()
        {
            List<string> animalEmojis = new List<string>()
            {
                "🐒","🐒",
                "🐈","🐈",
                "🦝","🦝",
                "🐘","🐘",
                "🦉","🦉",
                "🐣","🐣",
                "🦜","🦜",
                "🐝","🐝",
            };

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timerTextBlock" && textBlock.Name != "LowestTime")
                {
                    textBlock.Visibility = Visibility.Visible;
                    Random random = new Random();
                    int randomIndex = random.Next(animalEmojis.Count);
                    textBlock.Text = animalEmojis[randomIndex];
                    animalEmojis.RemoveAt(randomIndex);
                }
            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }
        private void TimerTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == winPoint)
            {
                SetUpGame();
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if(!getFirstTextBlock)
            {
                firstTextBlock = textBlock;
                firstTextBlock.Visibility = Visibility.Hidden;
                getFirstTextBlock = true;
            }
            else if(textBlock.Text==firstTextBlock.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                matchesFound++;
                getFirstTextBlock = false;
            }
            else
            {
                firstTextBlock.Visibility = Visibility.Visible;
                getFirstTextBlock = false;
            }

        }

      
    }
}
