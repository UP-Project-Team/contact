using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Файл содержит все связаное со сменой состояния игры по таймеру

namespace Contact.Server
{
    // Содержит длительности разных фаз игры
    class Timing
    {
        // State => duration (in milliseconds)
        private static readonly Dictionary<GameState.State, int> StateDurationTime = new Dictionary<GameState.State, int>
            {
                {GameState.State.NotStarted, Int32.MaxValue},
                {GameState.State.HaveCurrentWord, 20*1000},
                {GameState.State.HaveCurrentWordVariant, 5*1000},
                {GameState.State.VotingForPlayersWords, 10*1000}
            };

        public static int Duration(GameState.State state)
        {
            return StateDurationTime[state];
        }
    }

    partial class Room
    {
        private System.Timers.Timer stateTimer;

        public delegate void StateTimeoutDelegate();

        private Dictionary<GameState.State, StateTimeoutDelegate> timeoutFunctions;

        private void InitTiming()
        {
            timeoutFunctions = new Dictionary
            <GameState.State, StateTimeoutDelegate>
            {
                {GameState.State.HaveCurrentWord, this.HaveCurrentWordTimeout},
                {GameState.State.HaveCurrentWordVariant, this.HaveCurrentWordVariantTimeout},
                {GameState.State.VotingForPlayersWords, this.VotingForPlayersWordsTimeout}
            };

            stateTimer = new System.Timers.Timer();
            stateTimer.Elapsed += Timer_Elapsed;
            stateTimer.AutoReset = false;
        }

        private void SetStateTimer(GameState.State state)
        {
            stateTimer.Stop();
            stateTimer.Interval = Timing.Duration(state);
            stateTimer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // TODO: check that state that ended is the state that was sheduled
            //Invoke appropriate function
            timeoutFunctions[gameState.state]();
        }

        #region State timeout functions

        private void HaveCurrentWordTimeout()
        {
            LogSaver.Log("HaveCurrentWord Timeout");
            //TODO: remove stub and do actual logic
            ChangeState(GameState.State.NotStarted);
        }

        private void HaveCurrentWordVariantTimeout()
        {
            LogSaver.Log("HaveCurrentWordVariant Timeout");

            lock (gameState)
            {
                gameState.PrepareForVoting(2);
            }

            ChangeState(GameState.State.VotingForPlayersWords);
        }

        private void VotingForPlayersWordsTimeout()
        {
            LogSaver.Log("VotingForPlayersWords Timeout");
            User contacter, questioner;
            bool win = false;

            lock (gameState)
            {
                bool first = gameState.votings[0].Accepted(gameState.Users.Count);
                bool second = gameState.votings[1].Accepted(gameState.Users.Count);


                // добавить в список используемых
                if (first) //TODO: broadcast this to users
                    gameState.UsedWords.Add(gameState.CurrentWord);

                if (second) //TODO: broadcast this to users
                    gameState.UsedWords.Add(gameState.VarOfCurWord);


                if (first && second) // игроки выиграли
                {
                    gameState.NumberOfOpenChars++;

                    if (gameState.NumberOfOpenChars == gameState.PrimaryWord.Length) ; // TODO: слово отгадано по буквам стейт

                    win = true;
                }


                // сбросить роли
                contacter = gameState.Users.Single(user => user.role == User.Role.Contacter);
                contacter.role = User.Role.None;
                //TODO: раскоментить когда рольв опрошающего будет проставляться
                //questioner = gameState.Users.Single(user => user.role == User.Role.Qwestioner);
                //questioner.role = User.Role.None;
            }

            //разослать изменения
            BroadcastMessage(GameMessage.UserRoleChangedMessage(contacter, User.Role.None));
            //TODO: раскоментить когда рольв опрошающего будет проставляться
            //BroadcastMessage(GameMessage.UserRoleChangedMessage(questioner, User.Role.None));
            if(win) 
                BroadcastMessage(GameMessage.PrimaryWordCharOpened());

            ChangeState(GameState.State.HaveCurrentWord);

        }
        #endregion
    }

}
