using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Server
{
    //Controls user accounts, loads and safes them to DB
    class AccountControll
    {
        
        /* Check user name and password
         * If correct load user data from DB, add user to OnlineUsers
         * 
         * @return UserId
         */
        private static int userCnt; // remove this when DB support will be added
        public static int LoginUser(string name, string password)
        {
            // TODO: add actual DB support (now it accepts everything and gives consecutive user ids)
            var user = new User(userCnt++, name);
            RoomControll.AddOnlineUser(user);
            return user.Id;
        }

    }
}
