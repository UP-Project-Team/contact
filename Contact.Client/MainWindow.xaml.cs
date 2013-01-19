using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Contact.Client.GameService;

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
            // Ведущим становится нажавший эту кнопку.
            ClientControll.gameState.Me.role = User.Role.Host;
            txtPrimaryWord.Text = ClientControll.gameState.PrimaryWord;
        }

        private void btnSubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.GiveCurrentWordVariant(txtAnswer.Text);
            txtAnswer.Text = "";
        }
        
        private void btnAskQuestion_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.AskQuestion(txtQuestion.Text, txtCurrentWord.Text);
            txtQuestion.Text = "";
            txtCurrentWord.Text = "";
        }

        #region States Activate Actions 

        private List<Tuple<UIElement, Func<bool>>> StateActivateActionsList;
        
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StateActivateActionsList = new List<Tuple<UIElement, Func<bool>>>
            {
                new Tuple<UIElement, Func<bool>>(btnStartGame, () => ClientControll.gameState.State==GameState.State.NotStarted),
                new Tuple<UIElement, Func<bool>>(HaveNoCurrentWord, () => ClientControll.gameState.State==GameState.State.HaveNoCurrentWord && ClientControll.gameState.Me.role != User.Role.Host),
                new Tuple<UIElement, Func<bool>>(HostWaitQuestion, () => ClientControll.gameState.State==GameState.State.HaveNoCurrentWord && ClientControll.gameState.Me.role == User.Role.Host),
                new Tuple<UIElement, Func<bool>>(HostChiefWord, () => ClientControll.gameState.State==GameState.State.HaveCurrentWord && ClientControll.gameState.Me.role == User.Role.Host),
                new Tuple<UIElement, Func<bool>>(HaveCurrentWord, () => ClientControll.gameState.State==GameState.State.HaveCurrentWord && ClientControll.gameState.Me.role == User.Role.None),
                new Tuple<UIElement, Func<bool>>(HaveCurrentWordVariant, () => ClientControll.gameState.State==GameState.State.HaveCurrentWordVariant && ClientControll.gameState.Me.role == User.Role.Contacter),
                new Tuple<UIElement, Func<bool>>(VotingForChiefWord, () => ClientControll.gameState.State==GameState.State.VotingForHostWord),              
                new Tuple<UIElement, Func<bool>>(VotingForPlayersWords, () => ClientControll.gameState.State==GameState.State.VotingForPlayersWords),                
                new Tuple<UIElement, Func<bool>>(GameOver, () => ClientControll.gameState.State==GameState.State.GameOver),
                new Tuple<UIElement, Func<bool>>(QwestionerHaveCurrentWord, () => ClientControll.gameState.State==GameState.State.HaveCurrentWord && ClientControll.gameState.Me.role == User.Role.Qwestioner),
               new Tuple<UIElement, Func<bool>>(lstRooms, () => ClientControll.gameState.CurrentRoomId==0)
            };
            UpdateStatesVisibility();
        }

        public void gameState_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName=="State" || e.PropertyName=="Me.role" || e.PropertyName=="CurrentRoomId")
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
        private void LstRooms_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var room = (Room)lstRooms.SelectedItem;
            if (room == null || room.Id==0) return;
            
            ClientControll.GotoRoom(room.Id);
        }

        private void btnHostHaveChiefWord_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.GiveChiefWord(txtChiefWord.Text);
            txtChiefWord.Text = "asd";            
        }

        private void btnVoteForChiefWord_Agree_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVoteForChiefWord_Disagree_Click(object sender, RoutedEventArgs e)
        {

        }        
        
    }
}