using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SortingAlgorithms
{
    public partial class MainWindow : Window
    {
        List<float> lines = new();

        Random rnd = new Random();

        DispatcherTimer timer = new DispatcherTimer();

        int timerIndex = 0;

        //change both of them to increase or decrease the number of lines
        int linesNumber = 70;
        int recaleIndex = 70;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(0.01);
            timer.Tick += Timer_Tick;
        }


        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            generateLines(layout, lines);
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            timerIndex = 0;
            recaleIndex = linesNumber;
            timer.Start();
        }


       
        void generateLines(Panel parentControl, List<float> lines)
        {
            //clearing all lines
            lines.Clear();
            parentControl.Children.Clear();

            //generating random line heights
            for (int i = 0; i < linesNumber; i++)
            {
                lines.Add(rnd.Next(0, (int)Height));
            }

            //drawing lines
            for (int i = 0; i < lines.Count; i++)
            {
                Line line = new Line()
                {
                    X1 = i,
                    Y1 = -20,
                    X2 = i,
                    Y2 = Height - lines[i] - 40,
                    StrokeThickness = 3,
                    Stroke = Brushes.Black
                };
                parentControl.Children.Add(line);
            }
        }

        public void refresh(Panel parentControl, List<float> lines)
        {
            int i = 0;
            parentControl.Children.Clear();
            foreach (float line in lines)
            {
                Line line1 = new Line()
                {
                    X1 = i, Y1 = 0, X2 = i, Y2 = Height - line - 40, StrokeThickness = 3, Stroke = Brushes.Black
                };
                parentControl.Children.Add(line1);
                i++;
            }
        }

        public void swap(int index1, int index2, List<float> list, Panel parentControle)
        {
            float temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
            refresh(parentControle, lines);
        }
        //drawing loop function
        private void Timer_Tick(object? sender, EventArgs e)
        {
            if(recaleIndex == 0)
            {
                timer.Stop();
                MessageBox.Show("Sorted with bubble sort successfuly!","Done",MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (timerIndex < linesNumber - 1 && timerIndex < recaleIndex)
            {
                if (lines[timerIndex] > lines[timerIndex + 1])
                {
                    swap(timerIndex, timerIndex + 1, lines, layout);
                }
                timerIndex++;
            }
            else
            {
                timerIndex = 0;
                recaleIndex--;
            }
        }
       
    }
}
