using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Server
{
    public class Voting
    {
        public int VotesUp { private set; get; }
        public int VotesDown { private set; get; }
        private readonly List<int> VotedUsers; // holds ids

        public Voting()
        {
            VotesUp = 0;
            VotesDown = 0;
            VotedUsers = new List<int>();
        }

        public void Vote(User user, bool up)
        {
            if(VotedUsers.Contains(user.Id))
                GameException.Throw("Вы не можете голосовать дважды");

            if (up)
                VotesUp++;
            else
                VotesDown++;

            VotedUsers.Add(user.Id);
        }

        public bool Accepted(int amountOfUsers)
        {
            return VotesDown <= amountOfUsers/2;
        }
    }
}
