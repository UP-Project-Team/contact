using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
using Contact.Client.GameService;

namespace Contact.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ClientControll clientControll;

        public MainWindow(ClientControll clientControll)
        {
            InitializeComponent();
            //TODO: move this in XAML
            Closed += MainWindow_Closed;

            this.clientControll = clientControll;

            //TODO: this not supposed to be here
            // "Login"
            clientControll.Login();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            clientControll.Logoff();
        }
    }
}
