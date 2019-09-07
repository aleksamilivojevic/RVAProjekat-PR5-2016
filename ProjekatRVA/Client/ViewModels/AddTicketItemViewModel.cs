using Client.Helpers;
using Common.AppModel;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class AddTicketItemViewModel :BindableBase
    {
        public Window Window { get; set; }
        AppTicketItem ticketItem = new AppTicketItem();

        string selectedTheater;

        public string SelectedTheater
        {
            get { return selectedTheater; }
            set
            {
                selectedTheater = value;


                BindingList<string> temp = new BindingList<string>();

                ClientSideWCF.Instance.Proxy.GetAllAuditoriums(value).ToList().ForEach(x =>
                {
                    temp.Add(x.Number.ToString());
                });

                Auditoriums = temp;

                OnPropertyChanged("SelectedTheater");
            }
        }
        public AppTicketItem TicketItem
        {
            get { return ticketItem; }
            set
            {
                ticketItem = value;

               


                OnPropertyChanged("TicketItem");
            }
        }

        BindingList<string> auditoriums = new BindingList<string>();

        public BindingList<string> Auditoriums
        {
            get { return auditoriums; }
            set
            {
                auditoriums = value;
                OnPropertyChanged("Auditoriums");
            }
        }

        BindingList<string> tickets = new BindingList<string>();

        public BindingList<string> Tickets
        {
            get { return tickets; }
            set
            {
                tickets = value;
                OnPropertyChanged("Tickets");
            }
        }

        BindingList<string> theaters = new BindingList<string>();

        public BindingList<string> Theaters
        {
            get { return theaters; }
            set
            {
                theaters = value;

               
                OnPropertyChanged("Theaters");
            }
        }

        public MyICommand AddTicketItemCommand { get; set; }

        public AddTicketItemViewModel()
        {
            BindingList<string> temp = new BindingList<string>();
            ClientSideWCF.Instance.TicketProxy.GetAll().ToList().ForEach(x =>
            {
                temp.Add(x.Tag);
            });

            Tickets = temp;
            temp = new BindingList<string>();

            ClientSideWCF.Instance.Proxy.GetAllTheaters().ToList().ForEach(x =>
            {
                temp.Add(x.Name);
            });

            Theaters = temp;

            AddTicketItemCommand = new MyICommand(OnAddTicketItem);
        }

        public void OnAddTicketItem(object parameter)
        {
            TicketItem.Theater = SelectedTheater;
            TicketItem.Validate();
            int value;
            if (TicketItem.IsValid)
            {
                if ((value =ClientSideWCF.Instance.TicketItemProxy.AddTicketItem(TicketItem)) != -1)
                {
                    if(value == -2)
                    {
                        MessageBox.Show("Seats are taken.");
                    }
                    else
                    {
                        MainWindowViewModel.Refresh.Execute(null);
                        LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.ERROR, "Add ticket success.");
                        Window.Close();
                    }
                }
                else
                {
                    LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.ERROR,"Add ticket error.");
                }
            }
        }
    }
}
