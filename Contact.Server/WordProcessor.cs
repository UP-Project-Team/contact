using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Server
{
    class WordProcessor
    {
        public string PrimaryWord;
        public string CurrentWord;
        public string VarOfCurWord;
        public int CountOfOpenChars;
        public List<string> UsedWords = new List<string>();
        public string ChiefWord;
        public string Question;
        public List<char> LettersOfPrimaryWord = new List<char>();


        public bool CheckUsedWords()
        {
            foreach (string a in UsedWords)
            {
                if (CurrentWord == a)
                    return true;
            }
            return false;
        }

        public void AddToTheOpenChars(char a)
        {
            LettersOfPrimaryWord.Add(a);
        }

        public bool CompareWithPrimaryWord()
        {
            if ((CurrentWord == PrimaryWord) && (VarOfCurWord == PrimaryWord))
                return true;
            return false;
        }
    }
}
