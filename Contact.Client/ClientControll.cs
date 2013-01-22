using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Contact.Client.GameService;

namespace Contact.Client
{
    public static partial class ClientControll
    {
        private static GameServiceClient proxy;
        public static MainWindow mainWindow;
        public static GameView gameState;
        public static LoginWindow loginWindow;

        // actual application start point at the moment
        // MOVE IT SOMEWHERE ELSE? maybe make it static?
        public static void Run()
        {
            // create connection to server and callback
            var instanceContext = new InstanceContext(new ClientCallback());
            proxy = new GameServiceClient(instanceContext);

            mainWindow = new MainWindow();
            gameState = new GameView();
            mainWindow.DataContext = gameState;
            gameState.PropertyChanged += mainWindow.gameState_PropertyChanged;
            mainWindow.Show();

            //create login form
            loginWindow = new LoginWindow();
            loginWindow.Show();         
            
            // create and show main window
            
            loginWindow.Focus();
            mainWindow.IsEnabled = false;

        }
        public static async void Registration(string name, string password)
        {
            LogSaver.Log("Trying to registr");
            try
            {
                await proxy.RegistrationAsync(name, password);
            }
            catch (FaultException<GameException> e)
            {
                MessageBox.Show(e.Detail.Message);
            }
        } 
        public static async void Login(string name, string password)
        {            
            LogSaver.Log("Trying to login");            
            try
            {
                gameState.Me = await proxy.LoginAsync(name, password);

                GotoLobby();
                mainWindow.IsEnabled = true;
                loginWindow.Close();
            }
            catch (FaultException<GameException> e)
            {
                MessageBox.Show(e.Detail.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void Logoff()
        {
            proxy.Logoff(gameState.Me.Token);
        }

        public static void GetState()
        {
            var state = proxy.GetState(gameState.Me.Token);
            gameState.UpdateFromGameState(state);
        }

        public static void StartGame()
        {
            proxy.StartGame(gameState.Me.Token);
        }

        public static void GotoLobby()
        {
            GotoRoom(0);

            var rooms = proxy.GetRoomsList(gameState.Me.Token);
            gameState.Rooms = new ObservableCollection<Room>(rooms);
        }

        public static async void LeaveRoom()
        {
            try
            {
                await proxy.LeaveRoomAsync(gameState.Me.Token);
            }
            catch (FaultException<GameException> e)
            {
                MessageBox.Show(e.Detail.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            GetState();
            gameState.CurrentRoomId = 0;

            try
            {
                var rooms = proxy.GetRoomsList(gameState.Me.Token);
                gameState.Rooms = new ObservableCollection<Room>(rooms);
            }
            catch (FaultException<GameException> e)
            {
                MessageBox.Show(e.Detail.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static async void GotoRoom(int roomId)
        {
            try
            {
                await proxy.GotoRoomAsync(gameState.Me.Token, roomId);
            }
            catch (FaultException<GameException> e)
            {
                MessageBox.Show(e.Detail.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            GetState();
            gameState.CurrentRoomId = roomId;
        }

        public static async void SetPrimaryWord(string primaryWord)
        {
            try
            {
                await proxy.SetPrimaryWordAsync(gameState.Me.Token, primaryWord);
            }
            catch (FaultException<GameException> e)
            {
                MessageBox.Show(e.Detail.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static async void AskQuestion(string question, string word)
        {
            try
            {
                await proxy.AskQuestionAsync(gameState.Me.Token, question, word);
              
            }
            catch (FaultException<GameException> e)
            {
                MessageBox.Show(e.Detail.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        public static async void GiveChiefWord(string chiefword)
        {
            try
            {
                await proxy.GiveChiefWordAsync(gameState.Me.Token, chiefword);
            }
            catch (FaultException<GameException> e)
            {
                MessageBox.Show(e.Detail.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static async void GiveCurrentWordVariant(string answer)
        {
            try
            {
                await proxy.GiveCurrentWordVariantAsync(gameState.Me.Token, answer);
            }
            catch (FaultException<GameException> e)
            {
                MessageBox.Show(e.Detail.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static async void VoteForPlayerWord(int wordId, bool up)
        {
            try
            {
                await proxy.VoteForPlayerWordAsync(gameState.Me.Token, wordId, up);
            }
            catch (FaultException<GameException> e)
            {
                MessageBox.Show(e.Detail.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static async void VoteForChiefWord(bool up)
        {
            try
            {
                await proxy.VoteForChiefWordAsync(gameState.Me.Token, up);
            }
            catch (FaultException<GameException> e)
            {
                MessageBox.Show(e.Detail.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static async void CreateNewRoom(string name)
        {
            try
            {
                await proxy.AddRoomAsync(gameState.Me.Token, name);
            }
            catch (FaultException<GameException> e)
            {
                MessageBox.Show(e.Detail.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static async void SendChatMessage(string message)
        {
            try
            {
                await proxy.ReceiveChatMessageAsync(gameState.Me.Token, message);
            }
            catch (FaultException<GameException> e)
            {
                MessageBox.Show(e.Detail.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}