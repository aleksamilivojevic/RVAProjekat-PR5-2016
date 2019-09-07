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
    public class Service : IService
    {
        public BindingList<Auditorium> GetAllAuditoriums(string theater)
        {
            BindingList<Auditorium> retVal = new BindingList<Auditorium>();
            using (var context = new TheaterContext())
            {
                string theaterTag= null;

                foreach (Theater item in context.Theaters)
                {
                    if (item.Name == theater)
                        theaterTag = item.Tag;
                }
                foreach (Auditorium item in context.Auditoriums)
                {
                    if(item.TheaterTag == theaterTag)
                        retVal.Add(item);
                }
            }

            return retVal;
        }

        public BindingList<Theater> GetAllTheaters()
        {
            BindingList<Theater> retVal = new BindingList<Theater>();
            using (var context = new TheaterContext())
            {
                foreach (Theater item in context.Theaters)
                {
                    retVal.Add(item);
                }
            }

            return retVal;
        }

        public void AddTheater(Theater theater)
        {
            using (var context = new TheaterContext())
            {
                context.Theaters.Add(theater);
                context.SaveChanges();
            }
        }

        public bool CheckExistingTheaters()
        {
            using (var context = new TheaterContext())
            {
                return context.Theaters.ToList().Count() == 0? true : false;
            }
        }

        public bool CheckExistingAuditoriums()
        {
            using (var context = new TheaterContext())
            {
                return context.Auditoriums.ToList().Count() == 0 ? true : false;
            }
        }

        public void AddAuditorium(Auditorium auditorium)
        {
            using (var context = new TheaterContext())
            {
                context.Auditoriums.Add(auditorium);
                context.SaveChanges();
            }
        }
    }
}
