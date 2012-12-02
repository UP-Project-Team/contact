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


        public void AddWordToUsedlist(string a)
        {
            UsedWords.Add(a);
        }

        public bool CheckUsedWords()
        {
            return UsedWords.Any(a => CurrentWord == a);
        }

        public void ViewOpenChars()
        {
            string s = PrimaryWord.Substring(0, length: CountOfOpenChars);
        }
    }
}
