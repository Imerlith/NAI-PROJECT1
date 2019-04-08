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
       
        public MainWindow()
        {
            InitializeComponent();
            Buttons = FindVisualChildren<Button>(this);
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
           
            Block();
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
           
        }
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
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
