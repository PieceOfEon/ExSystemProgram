using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ExSystemProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool settingsPress = false;
        bool processPress = false;
        public MainWindow()
        {
            InitializeComponent();

        }
        
        private async void RunningApplications()
        {
            await Task.Run(() =>
            {
                Process[] processes = Process.GetProcesses();

                foreach (Process process in processes)
                {
                    // обновляем пользовательский интерфейс в UI-потоке
                    Dispatcher.InvokeAsync(() =>
                    {
                        ProcessInfText.Text += process.ProcessName + " | " + process.StartTime +"\n";
                       
                    });
                    
                        
                }
                Task.WaitAll();

            });
           
        }
        private void WriteFileLog()
        {
            //MessageBox.Show(ProcessInfText.Text.ToString());
            if(ProcessInfText.Text!=null)
            {
                File.AppendAllText("Log.txt", ProcessInfText.Text);
            }

                
            
        }
        private async void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            await Dispatcher.InvokeAsync(() =>
            {
                ProcessInfText.Text += e.Key + "\n";
                //Обработка нажатия клавиши
            });

            await Task.Delay(1);

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(settingsPress==true)
            {
                
                if (!borderDown.IsMouseOver)
                {
                    Storyboard storyboard = new Storyboard();
                    storyboard.Duration = TimeSpan.FromSeconds(1.5);
                    storyboard.AutoReverse = false;
                    storyboard.RepeatBehavior = new RepeatBehavior(1);

                    DoubleAnimation animation = new DoubleAnimation();
                    animation.From = -150;
                    animation.To = -500;
                    animation.Duration = TimeSpan.FromSeconds(1.5);
                    animation.DecelerationRatio = 1;

                    Storyboard.SetTargetName(animation, "borderDown");
                    Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Top)"));

                    storyboard.Children.Add(animation);

                    borderDown.BeginStoryboard(storyboard);

                    settingsPress = false;
                }

            }

            if(processPress==true)
            {
                if (!border3.IsMouseOver)
                {
                    Storyboard storyboard = new Storyboard();
                    storyboard.Duration = TimeSpan.FromSeconds(1.5);
                    storyboard.AutoReverse = false;
                    storyboard.RepeatBehavior = new RepeatBehavior(1);

                    DoubleAnimation animation = new DoubleAnimation();
                    animation.From = -343;
                    animation.To = -620;
                    animation.Duration = TimeSpan.FromSeconds(1.5);
                    animation.DecelerationRatio = 1;

                    Storyboard.SetTargetName(animation, "border3");
                    Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Left)"));

                    storyboard.Children.Add(animation);

                    border3.BeginStoryboard(storyboard);

                    processPress = false;
                }
            }
           
        
        }

        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {
            settingsPress = true;
        }

        private async void buttonProcess_Click(object sender, RoutedEventArgs e)
        {
            processPress = true;
            RunningApplications();
            
            if (settingsStatistics.IsChecked == true)
            {
                WriteFileLog();
            }

        }
    }
}
