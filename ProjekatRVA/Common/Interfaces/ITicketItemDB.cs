using Common.AppModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ITicketItemDB
    {
        int AddTicketItem(AppTicketItem ticket);
        bool DeleteTicketItem(AppTicketItem ticket);
        BindingList<AppTicketItem> GetAllTicketItems();
        AppTicketItem GetOneTicketItem(int id);
        bool ChangeTicketItem(AppTicketItem ticket);
        
    }
}
