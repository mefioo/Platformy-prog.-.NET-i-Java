using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Lab01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Person> people = new ObservableCollection<Person>
        {
           // new Person { Text1 = "P1", Text2 = "P11", Text3 = "im5.jpg" },
           // new Person { Text1 = "P2", Text2 = "P22", Text3 = "im2.jpg" },
        };

        public ObservableCollection<Person> Items
        {
            get => people;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

       

        string path;
        private void Loadbtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }
            path = op.FileName;

        }
        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            people.Add(new Person { Text2 = TextBox2.Text, Text1 = TextBox1.Text, Text3 = path });
        }

        private void Panel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var test = sender as StackPanel;
            
            var txt1 = ((System.Windows.Controls.TextBlock)test.Children[0]).Text;
            TextBox1.Text = txt1;
            var txt2 = ((System.Windows.Controls.TextBlock)test.Children[1]).Text;
            TextBox2.Text = txt2;
            var img = ((System.Windows.Controls.Image)test.Children[2]).Source;
            imgPhoto.Source = img;
            path = imgPhoto.Source.ToString();
        }
    }
}