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

        //temporary solution
        private GameServiceClient proxy;

        public MainWindow()
        {
            InitializeComponent();
            this.Closed += MainWindow_Closed;

            //create coonection to server
            var instanceContext = new InstanceContext(new ClientCallback());
            proxy = new GameServiceClient(instanceContext);

            // "Login"
            proxy.Login("dumb", "asd123");
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            proxy.Logoff();
        }

        private void btnGetState_Click(object sender, RoutedEventArgs e)
        {
            var result = proxy.GetState();
            lstUsersOnline.DataContext = result.Users;
        }
    }
}
