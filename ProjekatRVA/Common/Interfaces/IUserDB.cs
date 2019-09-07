using Common.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IUserDB
    {
        #region User
        bool AddUser(AppUser user);
        bool ChangeInfo(AppUser user);
        AppUser Login(string username, string password);
        bool DeleteUser(AppUser user);
        int GetCount();

        #endregion
    }
}
