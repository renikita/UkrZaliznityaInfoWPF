using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UkrZaliznityaInfo
{
    /// <summary>
    /// Логика взаимодействия для RegistWindow.xaml
    /// </summary>
    public partial class RegistWindow : Window
    {
        MainWindow main = new MainWindow();
        public RegistWindow()
        {
            InitializeComponent();
        }

        public void RegShow()
        {

        }

        private void AmountSymbols(object sender, KeyEventArgs e)
        {
            var n = MailBox;
            var n1 = PassBox;
            if(n.Text.Length >= 25 && n.Text.Length <= 36)
            {
                n.FontSize = 15;
                n.Height = 30;
            }
            else if(n.Text.Length >= 37)
            {
                n.FontSize = 13;
                n.Height = 25;
            }
            else
            {
                n.FontSize = 20;
                n.Height = 35;
            }
            // PassBox
            if (n1.Text.Length >= 25 && n1.Text.Length <= 36)
            {
                n1.FontSize = 15;
                n1.Height = 30;
            }
            else if (n1.Text.Length >= 37)
            {
                n1.FontSize = 13;
                n1.Height = 25;
            }
            else
            {
                n1.FontSize = 20;
                n1.Height = 35;
            }
        }
        private bool CheckButton = false;
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            main.Show();
            CheckButton = true;
            Close();
            CheckButton = false;
        }

        private void ClosedMain(object sender, EventArgs e)
        {
            if(CheckButton == false)
                main.Close();
        }
    }
}
