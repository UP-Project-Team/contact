using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Contact.Server
{
    [DataContract]
    public class GameState
    {
        public enum State
        {
            NotStarted,
            HaveCurrentWord,
            HaveCurrentWordVariant,
            VotingForHostWord,
            VotingForPlayersWords,
            GameOver
        }

        [DataMember]
        public readonly List<User> Users = new List<User>();

        [DataMember]
        public State state { get; set; }

        [DataMember]
        public string PrimaryWord=""; //передаем целиком, чтобы показывать ведущему

        [DataMember]
        public string CurrentWord = "";

        [DataMember]
        public string VarOfCurWord = "";

        [DataMember]
        public int NumberOfOpenChars=0;

        [DataMember]
        public List<string> UsedWords = new List<string>();

        [DataMember]
        public string ChiefWord = "";

        [DataMember]
        public string Question = "";

        public string PrimaryWordKnownLetters
        {
            get { return PrimaryWord.Substring(0, NumberOfOpenChars); }

            private set { }  // need this for WCF. do not use and do not remove
        }
        

        public void AddWordToUsedlist(string a)
        {
            UsedWords.Add(a);
        }

        public bool CheckUsedWords()
        {
            return UsedWords.Any(a => CurrentWord == a);
        }

        public List<Voting> votings; 
        public void PrepareForVoting(int amountOfVotings)
        {
            votings = new List<Voting>();
            for (var i = 0; i < amountOfVotings; ++i)
                votings.Add(new Voting());
        }

        public GameState()
        {
            state = State.NotStarted;

            //TODO: remove this
            PrimaryWord = "жел";
            NumberOfOpenChars = 2;
            Question = "Куй!";
            CurrentWord = "железо";   
        }


    }
}
