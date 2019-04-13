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

namespace NAI_PROJECT1
{
    
    public partial class MainWindow : Window
    {
        private IEnumerable<Button> Buttons;
        private Network network;
        public MainWindow()
        {
            InitializeComponent();
            Buttons = FindVisualChildren<Button>(this);
             network = new Network();
            network.InitiateNetwork();
            network.StartLearning();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.Background == Brushes.Gray)
            {
                button.Background = Brushes.Red;
            }
            else
            {
                button.Background = Brushes.Gray;
            }
            
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            //TO nie działa ani tez nie dziala pobranie Buttons.remove(SubmitBtn);
            //var toRemove = Buttons.ToList().FirstOrDefault(b => b.Name == "SubmitBtn");
            //Console.WriteLine(Buttons.Count());
            //if (toRemove != null) Buttons.ToList().Remove(toRemove);
            //Console.WriteLine(Buttons.Count());
            Block();
            var input = new List<double>();
            foreach(Button b in Buttons)
            {
                if (b.Background == Brushes.Red)
                {
                    input.Add(1);
                }
                else
                {
                    input.Add(0);
                }
            }
            input.ForEach(Console.WriteLine);
            network.Test(input);
            Reset();
        }
        private void Block()
        {
          
            foreach(Button button in Buttons)
            {
                button.IsEnabled = false;
                
            }  
        }
        private void Reset()
        {
            foreach (Button button in Buttons)
            {
                button.IsEnabled = true;
                button.Background = Brushes.Gray;
            }
        }
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T )
                    {
                        var ch = child as Button;
                        if(ch.Name!= "SubmitBtn")
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
