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
    public class TicketItemDB : ITicketItemDB
    {
        #region TicketItem
        public int AddTicketItem(AppTicketItem ticketItem)
        {
            TicketItem t = new TicketItem(ticketItem);
            using (var dbContext = new TheaterContext())
            {
                int ticketPrice = -1;



                foreach (Ticket item in dbContext.Tickets)
                {
                    if (ticketItem.Ticket == item.Tag)
                    {
                        ticketPrice = item.Price;
                        break;
                    }
                }

                foreach (TicketItem item in dbContext.TicketItems)
                {
                    if (item.Id == t.Id)
                        return -1;

                    if (item.TheaterId == t.TheaterId && item.AuditoriumId == t.AuditoriumId && item.Row == t.Row &&
                        ((int.Parse(item.Seats.Split('-')[0]) <= int.Parse(t.Seats) && int.Parse(item.Seats.Split('-')[1]) >= int.Parse(t.Seats))||
                        ((int.Parse(item.Seats.Split('-')[0]) <= int.Parse(t.Seats)+t.Quantity && int.Parse(item.Seats.Split('-')[1]) >= int.Parse(t.Seats)+t.Quantity)))){
                        return -2;
                    }
                }
                int seat = int.Parse(t.Seats);
                seat += t.Quantity;
                t.Seats += "-" + seat;
                t.TotalPrice = t.Quantity * ticketPrice;
                dbContext.TicketItems.Add(t);
                dbContext.SaveChanges();

                return dbContext.TicketItems.ToList().Last().Id;
            }
        }

        public bool DeleteTicketItem(AppTicketItem TicketItem)
        {
            TicketItem t = new TicketItem(TicketItem);
            using (var dbContext = new TheaterContext())
            {
                foreach (TicketItem item in dbContext.TicketItems)
                {
                    if (item.Id == t.Id)
                        t = item;

                }

                dbContext.TicketItems.Remove(t);
                dbContext.SaveChanges();
                return true;
            }
        }

        public BindingList<AppTicketItem> GetAllTicketItems()
        {
            BindingList<AppTicketItem> retVal = new BindingList<AppTicketItem>();

            using (var dbContext = new TheaterContext())
            {
                List<int> bilateralNums = new List<int>();


                List<int> ids = new List<int>();
                dbContext.Tickets.ToList().ForEach(x => ids.Add(x.Id));
                foreach (TicketItem t in dbContext.TicketItems)
                {
                    AppTicketItem TicketItem = new AppTicketItem(t);
                    if (!ids.Contains(t.TicketId))
                        TicketItem.Ticket = "Ticket is deleted.";
                    retVal.Add(TicketItem);
                }


            }

            return retVal;
        }

        public AppTicketItem GetOneTicketItem(int id)
        {
            AppTicketItem retVal = new AppTicketItem();

            using (var dbContext = new TheaterContext())
            {
                foreach (TicketItem t in dbContext.TicketItems)
                {
                    if (t.Id == id)
                        retVal = new AppTicketItem(t);
                }
            }

            return retVal;
        }

        public bool ChangeTicketItem(AppTicketItem TicketItem)
        {
            TicketItem t = new TicketItem(TicketItem);
            using (var dbContext = new TheaterContext())
            {
                int ticketPrice = -1;

                foreach (Ticket item in dbContext.Tickets)
                {
                    if (TicketItem.Ticket == item.Tag)
                    {
                        ticketPrice = item.Price;
                        break;
                    }
                }
                foreach (TicketItem item in dbContext.TicketItems)
                {
                    if (item.Id != t.Id &&item.TheaterId == t.TheaterId && item.AuditoriumId == t.AuditoriumId && item.Row == t.Row &&
                          ((int.Parse(item.Seats.Split('-')[0]) <= int.Parse(t.Seats) && int.Parse(item.Seats.Split('-')[1]) >= int.Parse(t.Seats)) ||
                          ((int.Parse(item.Seats.Split('-')[0]) <= int.Parse(t.Seats) + t.Quantity && int.Parse(item.Seats.Split('-')[1]) >= int.Parse(t.Seats) + t.Quantity))))
                    {
                        return false;
                    }
                    else if (item.Id == t.Id)
                    {
                        item.Quantity = t.Quantity;
                        int seat = int.Parse(t.Seats);
                        item.Seats = t.Seats;
                        seat += t.Quantity;
                        item.Seats += "-" + seat;
                        item.Row = t.Row;
                        item.TicketId = t.TicketId;
                        item.AuditoriumId = t.AuditoriumId;
                        item.TotalPrice = item.Quantity * ticketPrice;
                        item.TheaterId = t.TheaterId;

                        break;
                    }
                }

                dbContext.SaveChanges();
                return true;
            }
        }

        #endregion
    }
}
