using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;
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
            if (ClientControll.gameState.Users.Count > 2)
            {
                ClientControll.StartGame();
            }
            else
            {
                MessageBox.Show("В комнате должно быть не меньше 3 человек", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
                new Tuple<UIElement, Func<bool>>(panelUsedWords, () => ClientControll.gameState.CurrentRoomId!=0),
                new Tuple<UIElement, Func<bool>>(HaveNoPrimaryWord, () => ClientControll.gameState.State==GameState.State.HaveNoPrimaryWord && ClientControll.gameState.Me.role == User.Role.Host),
                new Tuple<UIElement, Func<bool>>(HaveNoCurrentWord, () => ClientControll.gameState.State==GameState.State.HaveNoCurrentWord && ClientControll.gameState.Me.role != User.Role.Host),
                new Tuple<UIElement, Func<bool>>(HostWaitQuestion, () => ClientControll.gameState.State==GameState.State.HaveNoCurrentWord && ClientControll.gameState.Me.role == User.Role.Host),
                new Tuple<UIElement, Func<bool>>(HostChiefWord, () => ClientControll.gameState.State==GameState.State.HaveCurrentWord && ClientControll.gameState.Me.role == User.Role.Host || ClientControll.gameState.State==GameState.State.HaveCurrentWordVariant &&  ClientControll.gameState.Me.role == User.Role.Host),
                new Tuple<UIElement, Func<bool>>(HaveCurrentWord, () => ClientControll.gameState.State==GameState.State.HaveCurrentWord && ClientControll.gameState.Me.role == User.Role.None),
                new Tuple<UIElement, Func<bool>>(HaveCurrentWordVariant, () => ClientControll.gameState.State==GameState.State.HaveCurrentWordVariant && ClientControll.gameState.Me.role == User.Role.Contacter),
                new Tuple<UIElement, Func<bool>>(VotingForChiefWord, () => ClientControll.gameState.State==GameState.State.VotingForHostWord),              
                new Tuple<UIElement, Func<bool>>(VotingForPlayersWords, () => ClientControll.gameState.State==GameState.State.VotingForPlayersWords),                
                new Tuple<UIElement, Func<bool>>(GameOver, () => ClientControll.gameState.State==GameState.State.GameOver),
                new Tuple<UIElement, Func<bool>>(QwestionerHaveCurrentWord, () => ClientControll.gameState.State==GameState.State.HaveCurrentWord && ClientControll.gameState.Me.role == User.Role.Qwestioner),
                new Tuple<UIElement, Func<bool>>(Lobby, () => ClientControll.gameState.CurrentRoomId==0),
                new Tuple<UIElement, Func<bool>>(btnLeaveRoom, () => ClientControll.gameState.CurrentRoomId!=0)
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

            // Clear chat area before entering the room
            ClientControll.gameState.ClearChat();
            ClientControll.GotoRoom(room.Id);
        }

        private void btnHostHaveChiefWord_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.GiveChiefWord(txtChiefWord.Text);                  
        }

        private void btnVoteForChiefWord_Agree_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.VoteForChiefWord(true);
        }

        private void btnVoteForChiefWord_Disagree_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.VoteForChiefWord(false);
        }

        private void btnCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.CreateNewRoom(txtRoomName.Text);
        }

        private void btnLeaveRoom_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.gameState.ClearChat();
            ClientControll.LeaveRoom();
        }

        private void btnChatSend_Click(object sender, RoutedEventArgs e)
        {
            if (txtChatInput.Text != "")
            {
                ClientControll.SendChatMessage(txtChatInput.Text);
                txtChatInput.Text = "";
            }
            txtChatInput.Focus();
        }

        private void txtChatInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnChatSend_Click(null, null);
        }        
        
        private void btnSetPrimaryWord_Click(object sender, RoutedEventArgs e)
        {
            ClientControll.SetPrimaryWord(txtSetPrimaryWord.Text);
            txtPrimaryWord.Text = txtSetPrimaryWord.Text;
        }
    }
}
