using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Client.GameService;

namespace Contact.Client
{

    /*
     * Часть контроллера которая занимается обработкой сообщений пришедших с сервера
     */

    public static partial class ClientControll
    {
        private delegate void CallbackAction(object arg);

        private static Dictionary<GameMessage.ActionType, CallbackAction> CallbackActions = new Dictionary
            <GameMessage.ActionType, CallbackAction>
            {
                {GameMessage.ActionType.LogoffUser,CallbackAction_LogoffUser},
                {GameMessage.ActionType.UserJoinedRoom, CallbackAction_UserJoinedRoom},
                {GameMessage.ActionType.UserLeftRoom, CallbackAction_UserLeftRoom},
                {GameMessage.ActionType.StateChanged, CallbackAction_StateChanged},
                {GameMessage.ActionType.VarOfCurWordChanged, CallbackAction_VarOfCurWordChanged},
                {GameMessage.ActionType.UserRoleChanged, CallbackAction_UserRoleChanged},
                {GameMessage.ActionType.PrimaryWordGiven, CallbackAction_PrimaryWordGiven},
                {GameMessage.ActionType.PrimaryWordCharOpened, CallbackAction_PrimaryWordCharOpened},
                {GameMessage.ActionType.UsedWordAdded, CallbackAction_UsedWordAdded},
                {GameMessage.ActionType.QuestionAsked, CallbackAction_QuestionAsked},
                {GameMessage.ActionType.WeHaveChiefWord, CallbackAction_WeHaveChiefWord},
                {GameMessage.ActionType.AddedRoom, CallbackAction_AddedRoom},
                {GameMessage.ActionType.ChatMessage, CallbackAction_ChatMessage}
            };
        private static void CallbackAction_AddedRoom(object arg)
        {
            LogSaver.Log("NewRoomAdded");
            var tuple=(Tuple<string, int>)arg;                              
            Room room = new Room();
            room.Id=tuple.Item2;
            room.Name = tuple.Item1;
            mainWindow.Dispatcher.Invoke(() =>
            {
                gameState.Rooms.Add(room);
            });

        }

        private static void CallbackAction_QuestionAsked(object arg)
        {
            var tuple = (Tuple<string, string>)arg;
            var question = tuple.Item1;
            var word = tuple.Item2;

            mainWindow.Dispatcher.Invoke(() =>
                {
                    gameState.Question = question;
                    gameState.CurrentWord = word;
                });
        }
        private static void CallbackAction_WeHaveChiefWord(object arg)
        {
            var word = (string)arg;
            mainWindow.Dispatcher.Invoke(() => { gameState.ChiefWord = word; });
        }
        private static void CallbackAction_UsedWordAdded(object arg)
        {
            var word = (string) arg;
            mainWindow.Dispatcher.Invoke(() => gameState.UsedWords.Add(word));
        }
        private static void CallbackAction_PrimaryWordGiven(object arg)
        {
            var word = (string)arg;
            mainWindow.Dispatcher.Invoke(() => gameState.PrimaryWord = word);
        }
        private static void CallbackAction_PrimaryWordCharOpened(object arg)
        {
            mainWindow.Dispatcher.Invoke(() => { gameState.NumberOfOpenChars++; });
        }

        private static void CallbackAction_UserRoleChanged(object arg)
        {
            var tuple = (Tuple<User, User.Role>) arg;
            var user = tuple.Item1;
            var role = tuple.Item2;

            mainWindow.Dispatcher.Invoke(() =>
                {
                    if (user.Id == gameState.Me.Id)
                        gameState.Me.role = role;

                    foreach (var curUser in gameState.Users)
                    {
                        if (curUser.Id == user.Id)
                            curUser.role = role;
                    }
                });
        }

        private static void CallbackAction_VarOfCurWordChanged(object arg)
        {
            var word = (string) arg;
            mainWindow.Dispatcher.Invoke(() =>
                {
                    gameState.VarOfCurWord = word;
                });

            LogSaver.Log("VarOfCurWordChanged "+word);
        }

        private static void CallbackAction_UserJoinedRoom(object arg)
        {
            var user = (User) arg;
            mainWindow.Dispatcher.Invoke(() => gameState.AddUser(user));
            gameState.AddChatMessage(null, "В комнату заходит " + user.Name);
            LogSaver.Log("User entered room ");
        }

        private static void CallbackAction_UserLeftRoom(object arg)
        {
            var user = (User) arg;
            mainWindow.Dispatcher.Invoke(() => gameState.RemoveUser(user));
            gameState.AddChatMessage(null, user.Name + " покидает комнату");
            LogSaver.Log("User left room");
        }

        private static void CallbackAction_StateChanged(object arg)
        {
            var state = (GameState.State) arg;
            mainWindow.Dispatcher.Invoke(() =>
                {
                    gameState.State = state;
                });

            LogSaver.Log("State changed. New = " + gameState.State);
        }

        private static void CallbackAction_LogoffUser(object arg)
        {
            mainWindow.Dispatcher.Invoke(() =>
            {
                mainWindow.IsEnabled = false;
            });
            loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        private static void CallbackAction_ChatMessage(object arg)
        {
            var tuple = (Tuple<string, string>)arg;

            mainWindow.Dispatcher.Invoke(() =>
            {
                gameState.AddChatMessage(tuple.Item1, tuple.Item2);
            });

        }

        // Process message
        public static void ChangeClientView(GameMessage message)
        {
            if (!CallbackActions.ContainsKey(message.actionType))
            {
                LogSaver.Log("Callback action for GameMessage.ActionType=" +
                                                  message.actionType.ToString() + " not implemented");
                throw new NotImplementedException("Callback action for GameMessage.ActionType=" +
                                                  message.actionType.ToString() + " not implemented");
            }
            //Call appropriate function
            CallbackActions[message.actionType](message.actionAgrument);
        }
    }

}
