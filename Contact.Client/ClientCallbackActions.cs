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
                {GameMessage.ActionType.UserJoinedRoom, CallbackAction_UserJoinedRoom},
                {GameMessage.ActionType.UserLeftRoom, CallbackAction_UserLeftRoom},
                {GameMessage.ActionType.StateChanged, CallbackAction_StateChanged},
                {GameMessage.ActionType.VarOfCurWordChanged, CallbackAction_VarOfCurWordChanged},
                {GameMessage.ActionType.UserRoleChanged, CallbackAction_UserRoleChanged}
            };

        private static void CallbackAction_UserRoleChanged(object arg)
        {
            var tuple = (Tuple<User, User.Role>) arg;
            var user = tuple.Item1;
            var role = tuple.Item2;

            if (user.Id == gameState.Me.Id)
                gameState.Me.role = role;

            foreach (var curUser in gameState.Users)
            {
                if (curUser.Id == user.Id)
                    curUser.role = role;
            }
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
            LogSaver.Log("User entered room ");
        }

        private static void CallbackAction_UserLeftRoom(object arg)
        {
            var user = (User) arg;
            mainWindow.Dispatcher.Invoke(() => gameState.RemoveUser(user));
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


        // Process message
        public static void ChangeClientView(GameMessage message)
        {
            if(!CallbackActions.ContainsKey(message.actionType))
                throw new NotImplementedException("Callback action for GameMessage.ActionType="+message.actionType.ToString()+" not implemented");

            //Call appropriate function
            CallbackActions[message.actionType](message.actionAgrument);
        }
    }

}
