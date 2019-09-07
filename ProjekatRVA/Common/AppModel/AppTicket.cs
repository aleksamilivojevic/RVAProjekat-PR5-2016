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
    public class AppTicket : ValidationBase
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Price { get; set; }
        [DataMember]
        public string Tag { get; set; }


        public AppTicket() { }

        public AppTicket(Ticket ticket)
        {
            try
            {
                this.Id = ticket.Id;
            }
            catch { }
            this.Price = ticket.Price.ToString();
            this.Tag = ticket.Tag;
        }

        public AppTicket(AppTicket ticket)
        {
            this.Id = ticket.Id;
            this.Price = ticket.Price;
            this.Tag = ticket.Tag;
        }
        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this.Price.ToString()) || String.IsNullOrEmpty(Price.ToString()))
            {
                this.ValidationErrors["Price"] = "Price cannot be empty.";
            }
            else
            {
                try
                {
                    int.Parse(this.Price);
                }
                catch
                {
                    this.ValidationErrors["Price"] = "Input is not a number.";
                }
            }

            if (string.IsNullOrWhiteSpace(this.Tag.ToString()) || String.IsNullOrEmpty(Tag.ToString()))
            {
                this.ValidationErrors["Tag"] = "Tag cannot be empty.";
            }
            


        }
    }
}
