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
    public interface ITicketItemService
    {
        [OperationContract]
        BindingList<AppTicketItem> GetAll();

        [OperationContract]
        AppTicketItem GetOne(int id);

        [OperationContract]
        int AddTicketItem(AppTicketItem ticket);


        [OperationContract]
        bool DeleteTicketItem(AppTicketItem ticket);

        [OperationContract]
        bool ChangeTicketItem(AppTicketItem ticket);
    }
}
