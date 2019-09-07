using Common.AppModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ITicketDB
    {
        #region Ticket
        int AddTicket(AppTicket ticket);
        bool DeleteTicket(AppTicket ticket);
        BindingList<AppTicket> GetAllTickets();
        BindingList<AppTicket> Search(string priceContent, string priceCondition);
        AppTicket GetOneTicket(int id);
        bool ChangeTicket(AppTicket ticket,AppTicket oldTicket);
        void CloneTicket(AppTicket ticket);
        
        bool DeleteTicket2(AppTicket ticket);

        bool ChangeTicket2(AppTicket ticket);

        #endregion
    }
}
