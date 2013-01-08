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
                new Tuple<UIElement, Func<bool>>(HaveCurrentWordVariant, () => ClientControll.gameState.State==GameService.GameState.State.HaveCurrentWordVariant),
                new Tuple<UIElement, Func<bool>>(VotingForPlayersWords, () => ClientControll.gameState.State==GameService.GameState.State.VotingForPlayersWords),
                new Tuple<UIElement, Func<bool>>(GameOver, () => ClientControll.gameState.State==GameService.GameState.State.GameOver)
            };
            UpdateStatesVisibility();
        }

        public void gameState_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName=="State" || e.PropertyName=="Me.role")
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

        private void btnCurrentWordVoteUp_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.VoteForPlayerWord(0, true);
        }

        private void btnVarOfCurWordVoteUp_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.VoteForPlayerWord(1, true);
        }

        private void btnCurrentWordVoteDown_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.VoteForPlayerWord(0, false);
        }

        private void btnVarOfCurWordVoteDown_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.VoteForPlayerWord(1, false);
        }

        private void Test_OnClick(object sender, RoutedEventArgs e)
        {
            ClientControll.gameState.NumberOfOpenChars++;
        }

    }
}
