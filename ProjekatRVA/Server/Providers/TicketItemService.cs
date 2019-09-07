using Common.AppModel;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Providers
{
    public class TicketItemService : ITicketItemService
    {
        ILogger log = Logger.Instance;
        ITicketItemDB manager = new TicketItemDB();
        object x = new object();

        public BindingList<AppTicketItem> GetAll()
        {
            log.LogMessage(LOG_TYPE.INFO, "Get All Ticket items executed succesfuly.");
            return manager.GetAllTicketItems();
        }
        public int AddTicketItem(AppTicketItem ticket)
        {
            try
            {
                lock (x)
                {
                    
                    int retVal = manager.AddTicketItem(ticket);
                    log.LogMessage(LOG_TYPE.INFO, "Add Ticket item executed succesfuly.");
                    return retVal;
                }
            }
            catch
            {
                log.LogMessage(LOG_TYPE.ERROR, "Add Ticket item executed unsuccesfuly.");
                return -1;
            }

        }

        public bool DeleteTicketItem(AppTicketItem ticket)
        {
            try
            {
                lock (x)
                {
                    bool retVal = manager.DeleteTicketItem(ticket);
                    log.LogMessage(LOG_TYPE.INFO, "Delete Ticket item executed succesfuly.");
                    return retVal;
                }
            }
            catch
            {
                log.LogMessage(LOG_TYPE.ERROR, "Delete Ticket item executed unsuccesfuly.");
                return false;
            }
        }

        public AppTicketItem GetOne(int id)
        {
            try
            {
                AppTicketItem ticket = manager.GetOneTicketItem(id);
                log.LogMessage(LOG_TYPE.INFO, "Get One Ticket item executed succesfuly.");
                return ticket;
            }
            catch
            {
                log.LogMessage(LOG_TYPE.ERROR, "Get One Ticket item executed unsuccesfuly.");
                return null;
            }
        }

        public bool ChangeTicketItem(AppTicketItem ticket)
        {
            try
            {
                lock (x)
                {
                    bool retVal = manager.ChangeTicketItem(ticket);
                    log.LogMessage(LOG_TYPE.INFO, "Change Ticket item executed succesfuly.");
                    return retVal;
                }
            }
            catch
            {
                log.LogMessage(LOG_TYPE.ERROR, "Change Ticket item executed unsuccesfuly.");
                return false;
            }
        }
        public bool CheckExisting()
        {
            log.LogMessage(LOG_TYPE.INFO, "Checking existing Ticket item data.");
            if (manager.GetAllTicketItems().Count == 0)
                return true;
            else
                return false;
        }
    }
}
