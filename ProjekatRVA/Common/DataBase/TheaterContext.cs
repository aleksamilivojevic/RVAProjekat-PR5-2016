using Common.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataBase
{
    public class TheaterContext : DbContext
    {
        #region Fields
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<TicketItem> TicketItems { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Auditorium> Auditoriums { get; set; }
        #endregion

        #region Constructor
        public TheaterContext() : base("TheaterContext") { }
        #endregion
    }
}
