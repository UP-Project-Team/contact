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
using System.Windows.Shapes;

namespace Contact.Client
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();            
        }
       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                ClientControll.Login(LoginTB.Text, PasswordTB.Text);         
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClientControll.Registration(LoginTB.Text, PasswordTB.Text);
        }
    }
}
