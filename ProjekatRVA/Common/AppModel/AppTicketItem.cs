using Common.DataBase;
using Common.Helpers;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.AppModel
{
    [DataContract]
    public class AppTicketItem : ValidationBase
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Quantity { get; set; }
        [DataMember]
        public string Seats { get; set; }
        [DataMember]
        public string Ticket { get; set; }
        [DataMember]
        public string Row { get; set; }
        [DataMember]
        public string Auditorium { get; set; }
        [DataMember]
        public string Theater { get; set; }
        [DataMember]
        public int TotalPrice { get; set; }

        public AppTicketItem() { }

        public AppTicketItem(TicketItem  ticket)
        {
            using (var dbContext = new TheaterContext())
            {
                foreach (Ticket item in dbContext.Tickets)
                {
                    
                     if (item.Id == ticket.TicketId)
                        this.Ticket = item.Tag;
                }
                foreach (Theater item in dbContext.Theaters)
                {
                    if (item.Tag == ticket.TheaterId)
                        this.Theater = item.Name;
                }
                foreach (Auditorium item in dbContext.Auditoriums)
                {
                    if (item.Id == ticket.Id)
                        this.Auditorium = item.Number.ToString();
                }
            }
            this.Id = ticket.Id;
            this.Quantity = ticket.Quantity.ToString();
            this.Seats = ticket.Seats;
            this.Row = ticket.Row.ToString();
            this.TotalPrice = ticket.TotalPrice;
        }

        public AppTicketItem(AppTicketItem ticket)
        {
            this.Id = ticket.Id;
            this.Quantity = ticket.Quantity;
            this.Seats = ticket.Seats;
            this.Row = ticket.Row;
            this.Ticket = ticket.Ticket;
            this.Auditorium = ticket.Auditorium;
            this.TotalPrice = ticket.TotalPrice;
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this.Quantity.ToString()) || String.IsNullOrEmpty(Quantity.ToString()))
            {
                this.ValidationErrors["Quantity"] = "Quantity cannot be empty.";
            }
            else
            {
                try
                {
                    int.Parse(this.Quantity);
                }
                catch
                {
                    this.ValidationErrors["Quantity"] = "Input is not a number.";
                }
            }

            if (string.IsNullOrWhiteSpace(this.Row.ToString()) || String.IsNullOrEmpty(Row.ToString()))
            {
                this.ValidationErrors["Row"] = "Row cannot be empty.";
            }
            else
            {
                try
                {
                    int.Parse(this.Row);
                }
                catch
                {
                    this.ValidationErrors["Row"] = "Input is not a number.";
                }
            }

            if (string.IsNullOrWhiteSpace(this.Seats.ToString()) || String.IsNullOrEmpty(Seats.ToString()))
            {
                this.ValidationErrors["Seats"] = "Seats cannot be empty.";
            }

            if (string.IsNullOrWhiteSpace(this.Ticket.ToString()) || String.IsNullOrEmpty(Ticket.ToString()))
            {
                this.ValidationErrors["Ticket"] = "Ticket cannot be empty.";
            }

            if (string.IsNullOrWhiteSpace(this.Auditorium.ToString()) || String.IsNullOrEmpty(Auditorium.ToString()))
            {
                this.ValidationErrors["Auditorium"] = "Auditorium cannot be empty.";
            }

            if (string.IsNullOrWhiteSpace(this.Theater.ToString()) || String.IsNullOrEmpty(Theater.ToString()))
            {
                this.ValidationErrors["Theater"] = "Theater cannot be empty.";
            }
        }
    }
}
