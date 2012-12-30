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

        #if DEBUG
        private static int userCnt = 0;
        #endif

        private static object syncObject = new object();
        public static UserData LoginUser(string name, string password)
        {
            User user;
            // TODO: add actual DB support (now it accepts everything and gives consecutive user ids)
            lock (syncObject) //добавлено чтобы не путать OperatinContext, хотя не понятно насколько работает
            {
            #if DEBUG
            user = new User(userCnt, name);
            ++userCnt;            
            #else
            user = DBAccess.CheckUser(name, password);
            #endif

            if (user.Id != -1)            
            RoomControll.AddOnlineUser(user);
            
            }
            

            return new UserData(user);
        }

    }
}
