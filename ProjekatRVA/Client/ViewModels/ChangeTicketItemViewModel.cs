using Client.Helpers;
using Common.AppModel;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class ChangeTicketItemViewModel : BindableBase
    {
        public Window Window { get; set; }

        AppTicketItem ticketItem = new AppTicketItem();
        AppTicketItem oldTicketItem;

        

        public AppTicketItem TicketItem
        {
            get { return ticketItem; }
            set
            {
                ticketItem = value;
                OnPropertyChanged("TicketItem");
            }
        }

        public MyICommand ChangeTicketItemCommand { get; set; }

        public ChangeTicketItemViewModel(AppTicketItem t)
        {
            oldTicketItem = new AppTicketItem(t);
            TicketItem = t;
            ChangeTicketItemCommand = new MyICommand(OnChangeTicketItem);
        }

        public void OnChangeTicketItem(object parameter)
        {

            LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Change TicketItem command.");
            if (TicketItem.Quantity == oldTicketItem.Quantity && TicketItem.Seats == oldTicketItem.Seats && TicketItem.Row == oldTicketItem.Row)
            { }
            else {
                if (TicketItem.Seats == oldTicketItem.Seats)
                    TicketItem.Seats = TicketItem.Seats.Split('-')[0];
                TicketItem.Validate();
                
                if (TicketItem.IsValid)
                {
                    if (ClientSideWCF.Instance.TicketItemProxy.ChangeTicketItem(TicketItem))
                    {
                        LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Change TicketItem command done.");
                        
                        MainWindowViewModel.Refresh.Execute(null);
                        Window.Close();
                    }
                    else
                    {
                        MessageBox.Show("Seats are taken.");
                        LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Change TicketItem command (TicketItem info not valid) not done.");
                    }
                }
            }
            
           
        }

    }
}
