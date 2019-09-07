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
    public class AddTicketViewModel:BindableBase
    {
        public Window Window { get; set; }

        AppTicket ticket = new AppTicket();

        public AppTicket Ticket
        {
            get { return ticket; }
            set
            {
                ticket = value;
                OnPropertyChanged("Ticket");
            }
        }

        public MyICommand AddTicketCommand { get; set; }

        public AddTicketViewModel()
        {
            AddTicketCommand = new MyICommand(OnAddTicketExecute, OnAddTicketUnExecute);
        }

        public void OnAddTicketExecute(object parameter)
        {
            if (parameter == null)
            {
                LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Add Ticket command.");
                Ticket.Validate();
                if (Ticket.IsValid)
                {
                    int id;
                    if ((id = ClientSideWCF.Instance.TicketProxy.AddTicket(Ticket)) != -1)
                    {
                        LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Add Ticket command done.");
                        AppTicket ticket = ClientSideWCF.Instance.TicketProxy.GetOne(id);
                        CommandHandler.Instance.AddAndExecute(AddTicketCommand, ticket);
                        MainWindowViewModel.Refresh.Execute(null);
                        Window.Close();
                    }
                    else
                    {
                        LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.ERROR, "Add Ticket command (Id alredy exists) not done.");
                    }
                }
                else
                {
                    LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.ERROR, "Add Ticket command (Ticket info not valid) not done.");
                }
            }
            else
            {
                int id;
                if ((id = ClientSideWCF.Instance.TicketProxy.AddTicket((AppTicket)parameter)) != -1)
                {
                    LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Add Ticket command redo done.");
                    AppTicket ticket = ClientSideWCF.Instance.TicketProxy.GetOne(id);
                    CommandHandler.Instance.undoObjects[CommandHandler.Instance.undoObjects.Count - 1] = new AppTicket(ticket);
                    MainWindowViewModel.Refresh.Execute(null);
                }
            }
        }

        public void OnAddTicketUnExecute(object parameter)
        {

            if (ClientSideWCF.Instance.TicketProxy.DeleteTicket((AppTicket)parameter))
            {
                LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Add Ticket command undo done.");
                CommandHandler.Instance.redoObjects[CommandHandler.Instance.redoObjects.Count - 1] = new AppTicket((AppTicket)parameter);
                MainWindowViewModel.Refresh.Execute(null);
            }
        }
    }
}
