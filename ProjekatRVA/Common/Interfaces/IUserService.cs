using Common.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        AppUser Login(string username, string password);

        [OperationContract]
        bool AddUser(AppUser user);

        [OperationContract]
        bool ChangeInfo(AppUser user);

        [OperationContract]
        bool DeleteUser(AppUser user);
    }
}
