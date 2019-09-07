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
    public class TicketService : ITicketService
    {
        ILogger log = Logger.Instance;
        ITicketDB manager = new TicketDB();
        object x = new object();

        public BindingList<AppTicket> GetAll()
        {
            log.LogMessage(LOG_TYPE.INFO, "Get All Tickets executed succesfuly.");
            return manager.GetAllTickets();
        }
        public int AddTicket(AppTicket ticket)
        {
            try
            {
                lock (x)
                {
                    int retVal = manager.AddTicket(ticket);
                    log.LogMessage(LOG_TYPE.INFO, "Add Ticket executed succesfuly.");
                    return retVal;
                }
            }
            catch
            {
                log.LogMessage(LOG_TYPE.ERROR, "Add Ticket executed unsuccesfuly.");
                return -1;
            }

        }

        public bool DeleteTicket(AppTicket ticket)
        {
            try
            {
                lock (x)
                {
                    bool retVal = manager.DeleteTicket(ticket);
                    log.LogMessage(LOG_TYPE.INFO, "Delete Ticket executed succesfuly.");
                    return retVal;
                }
            }
            catch
            {
                log.LogMessage(LOG_TYPE.ERROR, "Delete Ticket executed unsuccesfuly.");
                return false;
            }
        }
        public bool DeleteTicket2(AppTicket ticket)
        {
            try
            {
                lock (x)
                {
                    bool retVal = manager.DeleteTicket2(ticket);
                    log.LogMessage(LOG_TYPE.INFO, "Delete Ticket executed succesfuly.");
                    return retVal;
                }
            }
            catch
            {
                log.LogMessage(LOG_TYPE.ERROR, "Delete Ticket executed unsuccesfuly.");
                return false;
            }
        }

        public AppTicket GetOne(int id)
        {
            try
            {
                AppTicket ticket = manager.GetOneTicket(id);
                log.LogMessage(LOG_TYPE.INFO, "Get One Ticket executed succesfuly.");
                return ticket;
            }
            catch
            {
                log.LogMessage(LOG_TYPE.ERROR, "Get One Ticket executed unsuccesfuly.");
                return null;
            }
        }

        public bool ChangeTicket(AppTicket ticket,AppTicket oldTicket)
        {
            try
            {
                lock (x)
                {
                    bool retVal = manager.ChangeTicket(ticket,oldTicket);
                    log.LogMessage(LOG_TYPE.INFO, "Change Ticket executed succesfuly.");
                    return retVal;
                }
            }
            catch
            {
                log.LogMessage(LOG_TYPE.ERROR, "Change Ticket executed unsuccesfuly.");
                return false;
            }
        }
        public BindingList<AppTicket> Search(string priceContent, string priceCondition)
        {
            try
            {
                BindingList<AppTicket> retVal = manager.Search(priceContent, priceCondition);
                log.LogMessage(LOG_TYPE.INFO, "Change Ticket executed succesfuly.");
                return retVal;
            }
            catch (Exception e)
            {
                log.LogMessage(LOG_TYPE.ERROR, "Change Ticket executed unsuccesfuly.");
                return GetAll();
            }
        }

        public bool ChangeTicket2(AppTicket ticket)
        {
            try
            {
                lock (x)
                {
                    bool retVal = manager.ChangeTicket2(ticket);
                    log.LogMessage(LOG_TYPE.INFO, "Change Ticket executed succesfuly.");
                    return retVal;
                }
            }
            catch(Exception e)
            {
                log.LogMessage(LOG_TYPE.ERROR, "Change Ticket executed unsuccesfuly.");
                return false;
            }
        }
        public bool CheckExisting()
        {
            log.LogMessage(LOG_TYPE.INFO, "Checking existing Ticket data.");
            if (manager.GetAllTickets().Count == 0)
                return true;
            else
                return false;
        }

        public void CloneTicket(AppTicket ticket)
        {

            try
            {
                lock (x)
                {
                    manager.CloneTicket(ticket);
                    log.LogMessage(LOG_TYPE.INFO, "Clone Bilateral executed succesfuly.");
                }
            }
            catch
            {
                log.LogMessage(LOG_TYPE.ERROR, "Clone Bilateral executed unsuccesfuly.");

            }
        }
    }
}
