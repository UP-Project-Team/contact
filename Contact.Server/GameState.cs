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
            VotingForPlayersWords
        }

        [DataMember]
        public readonly List<User> Users = new List<User>();

        [DataMember]
        public State state { get; set; }


        public string PrimaryWord; //не передавать юзерам ключевое слово целиком

        [DataMember]
        public string CurrentWord;

        [DataMember]
        public string VarOfCurWord;

        [DataMember]
        public int CountOfOpenChars=0;

        [DataMember]
        public List<string> UsedWords = new List<string>();

        [DataMember]
        public string ChiefWord;

        [DataMember]
        public string Question;

        /*
        [DataMember]
        public string PrimaryWordKnownLetters
        {
            get { return PrimaryWord.Substring(0, CountOfOpenChars); }
        }
        */

        public void AddWordToUsedlist(string a)
        {
            UsedWords.Add(a);
        }

        public bool CheckUsedWords()
        {
            return UsedWords.Any(a => CurrentWord == a);
        }

        public GameState()
        {
            state = State.NotStarted;
        }

    }
}
