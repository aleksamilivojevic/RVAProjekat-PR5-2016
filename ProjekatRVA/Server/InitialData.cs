using Common.AppModel;
using Common.Model;
using Server.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class InitialData
    {
        public InitialData()
        {
            CheckAndFillData();
        }
        private void CheckAndFillData()
        {
            UserService userService = new UserService();
            if (userService.CheckExisting())
            {
                userService.AddUser(new AppUser { FirstName = "Admin", Username = "Admin", LastName = "Admin", Password = "Admin" });
                userService.AddUser(new AppUser { FirstName = "Aleksa", Username = "aleksam", LastName = "Milivojevic", Password = "aleksammm" });
                userService.AddUser(new AppUser { FirstName = "Marija", Username = "marijas", LastName = "Semerad", Password = "marijasss" });
            }

            TicketService ticketService = new TicketService();
            if (ticketService.CheckExisting())
            {
                ticketService.AddTicket(new AppTicket() { Tag = "2D-STANDARD",Price="200" });
                ticketService.AddTicket(new AppTicket() { Tag = "3D-STANDARD", Price = "500" });
                ticketService.AddTicket(new AppTicket() { Tag = "4D-STANDARD", Price = "800" });
                ticketService.AddTicket(new AppTicket() { Tag = "2D-VIKEND", Price = "300" });
                ticketService.AddTicket(new AppTicket() { Tag = "3D-VIKEND", Price = "600" });
                ticketService.AddTicket(new AppTicket() { Tag = "4D-VIKEND", Price = "900" });
                ticketService.AddTicket(new AppTicket() { Tag = "2D-UTORAK", Price = "150" });
                ticketService.AddTicket(new AppTicket() { Tag = "3D-UTORAK", Price = "400" });
                ticketService.AddTicket(new AppTicket() { Tag = "4D-UTORAK", Price = "700" });
            }

            Service service = new Service();
            if (service.CheckExistingTheaters())
            {
                service.AddTheater(new Theater() { CityTag = "NS", Name = "BIG Cinestar" , Tag="000000001"});
                service.AddTheater(new Theater() { CityTag = "NS", Name = "Arena Cinestar", Tag = "000000002" });
                service.AddTheater(new Theater() { CityTag = "NS", Name = "Promenada Cineplex", Tag = "000000003" });
                service.AddTheater(new Theater() { CityTag = "BG", Name = "Usce Cinestar", Tag = "000000004" });
                service.AddTheater(new Theater() { CityTag = "BG", Name = "DeltaCity Cineplex", Tag = "000000005" });
            }

            if (service.CheckExistingAuditoriums())
            {
                service.AddAuditorium(new Auditorium() { Number=1, TheaterTag= "000000001" });
                service.AddAuditorium(new Auditorium() { Number = 2, TheaterTag = "000000001" });
                service.AddAuditorium(new Auditorium() { Number = 3, TheaterTag = "000000001" });

                service.AddAuditorium(new Auditorium() { Number = 1, TheaterTag = "000000002" });
                service.AddAuditorium(new Auditorium() { Number = 2, TheaterTag = "000000002" });
                service.AddAuditorium(new Auditorium() { Number = 3, TheaterTag = "000000002" });

                service.AddAuditorium(new Auditorium() { Number = 1, TheaterTag = "000000003" });
                service.AddAuditorium(new Auditorium() { Number = 2, TheaterTag = "000000003" });
                service.AddAuditorium(new Auditorium() { Number = 3, TheaterTag = "000000003" });

                service.AddAuditorium(new Auditorium() { Number = 1, TheaterTag = "000000004" });
                service.AddAuditorium(new Auditorium() { Number = 2, TheaterTag = "000000004" });
                service.AddAuditorium(new Auditorium() { Number = 3, TheaterTag = "000000004" });

                service.AddAuditorium(new Auditorium() { Number = 1, TheaterTag = "000000005" });
                service.AddAuditorium(new Auditorium() { Number = 2, TheaterTag = "000000005" });
                service.AddAuditorium(new Auditorium() { Number = 3, TheaterTag = "000000005" });

            }
            TicketItemService ticketItemService = new TicketItemService();
            if (ticketItemService.CheckExisting())
            {
                ticketItemService.AddTicketItem( new AppTicketItem{ Theater = "BIG Cinestar", Auditorium="1", Ticket= "2D-STANDARD", Row="1",Seats="1", Quantity="5" });
                ticketItemService.AddTicketItem(new AppTicketItem { Theater = "BIG Cinestar", Auditorium = "2", Ticket = "2D-STANDARD", Row = "2", Seats = "1", Quantity = "5" });
                ticketItemService.AddTicketItem(new AppTicketItem { Theater = "BIG Cinestar", Auditorium = "3", Ticket = "2D-UTORAK", Row = "3", Seats = "5", Quantity = "3" });
                ticketItemService.AddTicketItem(new AppTicketItem { Theater = "Arena Cinestar", Auditorium = "1", Ticket = "3D-STANDARD", Row = "4", Seats = "10", Quantity = "8" });
                ticketItemService.AddTicketItem(new AppTicketItem { Theater = "Arena Cinestar", Auditorium = "2", Ticket = "4D-STANDARD", Row = "5", Seats = "15", Quantity = "2" });
                ticketItemService.AddTicketItem(new AppTicketItem { Theater = "Arena Cinestar", Auditorium = "3", Ticket = "2D-VIKEND", Row = "6", Seats = "20", Quantity = "4" });
                ticketItemService.AddTicketItem(new AppTicketItem { Theater = "Usce Cinestar", Auditorium = "1", Ticket = "3D-UTORAK", Row = "7", Seats = "8", Quantity = "3" });
                ticketItemService.AddTicketItem(new AppTicketItem { Theater = "Usce Cinestar", Auditorium = "2", Ticket = "4D-VIKEND", Row = "8", Seats = "16", Quantity = "2" });
                ticketItemService.AddTicketItem(new AppTicketItem { Theater = "Usce Cinestar", Auditorium = "3", Ticket = "2D-STANDARD", Row = "9", Seats = "18", Quantity = "2" });
            }


        }
    }
}
