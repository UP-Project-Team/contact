using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Contact.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //TODO: move this in XAML
            Closed += MainWindow_Closed;
            Loaded += MainWindow_Loaded;

            //TODO: this not supposed to be here
            // "Login"
            string name, password;
            name = "login";
            password = "password";
            ClientControll.Login(name,password);
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            ClientControll.Logoff();
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.StartGame();
        }

        private void btnSubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.GiveCurrentWordVariant(txtAnswer.Text);
        }


        #region States Activate Actions 

        private List<Tuple<UIElement, Func<bool>>> StateActivateActionsList;
        
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StateActivateActionsList = new List<Tuple<UIElement, Func<bool>>>
            {
                new Tuple<UIElement, Func<bool>>(btnStartGame, () => ClientControll.gameState.State==GameService.GameState.State.NotStarted),
                new Tuple<UIElement, Func<bool>>(HaveCurrentWord, () => ClientControll.gameState.State==GameService.GameState.State.HaveCurrentWord),
                new Tuple<UIElement, Func<bool>>(HaveCurrentWordVariant, () => ClientControll.gameState.State==GameService.GameState.State.HaveCurrentWordVariant)
            };
            UpdateStatesVisibility();
        }

        public void gameState_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName=="State")
                UpdateStatesVisibility();
        }

        void UpdateStatesVisibility()
        {
            foreach (var t in StateActivateActionsList)
            {
                t.Item1.Visibility = t.Item2() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        #endregion
    }
}
