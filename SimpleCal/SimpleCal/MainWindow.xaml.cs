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
using System.Windows.Forms;

namespace SimpleCal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InAnalize first;
        public MainWindow()
        {
            InitializeComponent();
            first = new InAnalize();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string b = textBlock1.Text;
            textBlock2.Text=first.getcal(b);
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string a=textBox1.Text;
            textBlock1.Text = InAnalize.lex(a);
        }




    }
}
