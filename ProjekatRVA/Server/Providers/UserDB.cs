using Common.AppModel;
using Common.Model;
using Common.DataBase;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Providers
{
    public class UserDB : IUserDB
    {
        #region User
        public bool AddUser(AppUser user)
        {
            user.Username = user.Username.ToLower();
            using (var dbContext = new TheaterContext())
            {
                foreach (User u in dbContext.Users)
                {
                    if (u.Username.ToLower() == user.Username.ToLower())
                        return false;

                }

                dbContext.Users.Add(new User(user));
                dbContext.SaveChanges();
                return true;
            }
        }

        public bool ChangeInfo(AppUser user)
        {
            user.Username = user.Username.ToLower();
            using (var dbContext = new TheaterContext())
            {
                foreach (User u in dbContext.Users)
                {
                    if (u.Username.ToLower() == user.Username.ToLower())
                    {
                        u.FirstName = user.FirstName;
                        u.LastName = user.LastName;
                        u.Password = user.Password;
                        break;
                    }


                }


                dbContext.SaveChanges();
                return true;
            }
        }

        public AppUser Login(string username, string password)
        {
            using (var dbContext = new TheaterContext())
            {
                foreach (User user in dbContext.Users)
                {
                    if (username.ToLower() == user.Username.ToLower() && password == user.Password)
                    {

                        return new AppUser(user);
                    }
                }
                return null;
            }
        }

        public bool DeleteUser(AppUser user)
        {
            User us = null;
            using (var dbContext = new TheaterContext())
            {
                foreach (User u in dbContext.Users)
                {
                    if (u.Username == user.Username)
                    {
                        us = u;
                        break;
                    }
                }
                if (us == null)
                {
                    return false;
                }
                else
                {
                    dbContext.Users.Remove(us);
                    dbContext.SaveChanges();
                    return true;
                }
            }
        }

        public int GetCount()
        {
            using (var dbContext = new TheaterContext())
            {
                return dbContext.Users.Count();
            }
        }

        #endregion
    }
}
