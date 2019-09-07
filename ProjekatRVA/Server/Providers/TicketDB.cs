using Common.AppModel;
using Common.DataBase;
using Common.Interfaces;
using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Providers
{
    public class TicketDB : ITicketDB
    {
        #region Ticket
        public int AddTicket(AppTicket ticket)
        {
            Ticket t = new Ticket(ticket);
            int oldId = t.Id;
            using (var dbContext = new TheaterContext())
            {
                foreach (Ticket item in dbContext.Tickets)
                {
                    if (item.Id == t.Id)
                        return -1;
                }

                dbContext.Tickets.Add(t);
                dbContext.SaveChanges();
            }
            using (var dbContext = new TheaterContext())
            {
                foreach (TicketItem item in dbContext.TicketItems)
                {
                    if (item.TicketId == oldId)
                        item.TicketId = t.Id;
                }
                
                dbContext.SaveChanges();
                return dbContext.Tickets.ToList().Last().Id;
            }
           
            
        }

        public bool DeleteTicket(AppTicket ticket)
        {
            Ticket t = new Ticket(ticket);
            bool found = false;
            using (var dbContext = new TheaterContext())
            {
                foreach (Ticket item in dbContext.Tickets)
                {
                    if (item.Id == t.Id && item.Price == t.Price && item.Tag == t.Tag)
                    {
                        t = item;
                        found = true;
                    }
                }
                if (found)
                {

                    dbContext.Tickets.Remove(t);
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteTicket2(AppTicket ticket)
        {
            Ticket t = new Ticket(ticket);
            using (var dbContext = new TheaterContext())
            {
                foreach (Ticket item in dbContext.Tickets)
                {
                    if (item.Id == t.Id)
                        t = item;

                }

                dbContext.Tickets.Remove(t);
                dbContext.SaveChanges();
                return true;
            }
        }

        public BindingList<AppTicket> GetAllTickets()
        {
            BindingList<AppTicket> retVal = new BindingList<AppTicket>();

            using (var dbContext = new TheaterContext())
            {
                List<int> bilateralNums = new List<int>();

              

                foreach (Ticket t in dbContext.Tickets)
                {
                    AppTicket ticket = new AppTicket(t);
                    retVal.Add(ticket);
                }



            }

            return retVal;
        }

        public AppTicket GetOneTicket(int id)
        {
            AppTicket retVal = new AppTicket();

            using (var dbContext = new TheaterContext())
            {
                foreach (Ticket t in dbContext.Tickets)
                {
                    if (t.Id == id)
                        retVal = new AppTicket(t);
                }
            }

            return retVal;
        }

        public bool ChangeTicket(AppTicket ticket,AppTicket oldTicket)
        {
            Ticket t = new Ticket(ticket);
            Ticket t2 = new Ticket(oldTicket);
            bool found = false;
            using (var dbContext = new TheaterContext())
            {
                foreach (Ticket item in dbContext.Tickets)
                {
                    if (item.Id == t2.Id && item.Price == t2.Price && item.Tag == t2.Tag)
                    {
                        item.Tag = t.Tag;
                        item.Price = t.Price;
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    foreach (TicketItem item in dbContext.TicketItems)
                    {
                        if (item.TicketId == t.Id)
                        {
                            item.TotalPrice = t.Price * item.Quantity;
                        }
                    }


                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ChangeTicket2(AppTicket ticket)
        {
            Ticket t = new Ticket(ticket);
            int oldId = t.Id;
            bool found = false;
            using (var dbContext = new TheaterContext())
            {
                foreach (Ticket item in dbContext.Tickets)
                {
                    if (item.Id == t.Id)
                    {
                        item.Tag = t.Tag;
                        item.Price = t.Price;
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    foreach (TicketItem item in dbContext.TicketItems)
                    {
                        if (item.TicketId == t.Id)
                        {
                            item.TotalPrice = t.Price * item.Quantity;
                        }
                    }

                }
                else
                {
                    dbContext.Tickets.Add(t);
                    

                
                }
                dbContext.SaveChanges();
               
            }
            using (var dbContext = new TheaterContext())
            {
                foreach (TicketItem item in dbContext.TicketItems)
                {
                    if (item.TicketId == oldId)
                        item.TicketId = t.Id;
                
                    if (item.TicketId == t.Id)
                        item.TotalPrice = t.Price * item.Quantity;
                }
                dbContext.SaveChanges();
                return true;
            }
        }
        public BindingList<AppTicket> Search(string priceContent, string priceCondition)
        {
            BindingList<AppTicket> retVal = new BindingList<AppTicket>();
            using (var dbContext = new TheaterContext())
            {
                switch (priceCondition)
                {
                    case "<":
                        {
                            foreach (Ticket item in dbContext.Tickets)
                            {
                                if (item.Price <= int.Parse(priceContent))
                                    retVal.Add(new AppTicket(item));
                            }
                        }
                        break;
                    case "=":
                        {
                            foreach (Ticket item in dbContext.Tickets)
                            {
                                if (item.Price == int.Parse(priceContent))
                                    retVal.Add(new AppTicket(item));
                            }
                        }
                        break;
                    case ">":
                        {
                            foreach (Ticket item in dbContext.Tickets)
                            {
                                if (item.Price >= int.Parse(priceContent))
                                    retVal.Add(new AppTicket(item));
                            }
                        }
                        break;
                    default:
                        break;
                }
                

                return retVal;
            }
        }

        public void CloneTicket(AppTicket ticket)
        {
            Ticket t = new Ticket(ticket);
            Ticket clonedTicket = (Ticket)t.Clone();

            using (var context = new TheaterContext())
            {
                context.Tickets.Add(clonedTicket);
                context.SaveChanges();
            }
        }

        #endregion
    }
}
