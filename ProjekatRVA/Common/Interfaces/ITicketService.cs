using Common.AppModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface ITicketService
    {
        [OperationContract]
        BindingList<AppTicket> GetAll();

        [OperationContract]
        BindingList<AppTicket> Search(string priceContent, string priceCondition);

        [OperationContract]
        AppTicket GetOne(int id);

        [OperationContract]
        int AddTicket(AppTicket ticket);
        [OperationContract]
        void CloneTicket(AppTicket ticket);

        [OperationContract]
        bool DeleteTicket(AppTicket ticket);

        [OperationContract]
        bool ChangeTicket(AppTicket ticket,AppTicket oldTicket);
        [OperationContract]
        bool DeleteTicket2(AppTicket ticket);

        [OperationContract]
        bool ChangeTicket2(AppTicket ticket);
    }
}
