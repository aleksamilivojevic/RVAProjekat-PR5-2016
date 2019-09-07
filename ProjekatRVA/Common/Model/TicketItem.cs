using Common.AppModel;
using Common.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class TicketItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Seats { get; set; }
        public int TicketId { get; set; } 
        public int Row { get; set; }
        public int AuditoriumId { get; set; }
        public string TheaterId { get; set; }
        public int TotalPrice { get; set; }

        public TicketItem() { }

        public TicketItem(AppTicketItem ticket)
        {
            using (var dbContext = new TheaterContext())
            {
                foreach (Ticket item in dbContext.Tickets)
                {
                  
                    if (item.Tag == ticket.Ticket)
                        this.TicketId = item.Id;
                }
                foreach (Auditorium item in dbContext.Auditoriums)
                {
                    if (item.Number == int.Parse(ticket.Auditorium))
                        this.AuditoriumId = item.Id;
                }
                foreach (Theater item in dbContext.Theaters)
                {
                    if (item.Name == ticket.Theater)
                        this.TheaterId = item.Tag;
                }
            }
            this.Id = ticket.Id;
            this.Quantity = int.Parse(ticket.Quantity);
            this.Seats = ticket.Seats;
            this.Row = int.Parse(ticket.Row);
            this.TotalPrice = ticket.TotalPrice;
        }
        public TicketItem(TicketItem ticket)
        {
            this.Id = ticket.Id;
            this.Quantity = ticket.Quantity;
            this.Seats = ticket.Seats;
            this.Row = ticket.Row;
            this.TicketId = ticket.TicketId;
            this.AuditoriumId = ticket.AuditoriumId;
            this.TotalPrice = ticket.TotalPrice;
            this.TheaterId = ticket.TheaterId;
        }
    }
}
