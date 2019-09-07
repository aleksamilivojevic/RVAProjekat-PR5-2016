using Common.AppModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Ticket : Prototype
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int Price { get; set; }
        public string Tag { get; set; }

        public Ticket() { }

        public Ticket(AppTicket ticket)
        {
            try
            {
                this.Id = ticket.Id;
            }
            catch { }
            this.Price = int.Parse(ticket.Price);
            this.Tag = ticket.Tag;
        }

        public Ticket(Ticket ticket)
        {
            this.Id = ticket.Id;
            this.Price = ticket.Price;
            this.Tag = ticket.Tag;
        }

        public override Prototype Clone()
        {
            Ticket retVal =  (Ticket)this.MemberwiseClone();
            retVal.Tag += "-copy";
            return retVal;
        }
    }
}
