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
    public class ChangeTicketViewModel : BindableBase
    {
        public Window Window { get; set; }

        AppTicket ticket = new AppTicket();
        AppTicket oldTicket;

        public AppTicket Ticket
        {
            get { return ticket; }
            set
            {
                ticket = value;
                OnPropertyChanged("Ticket");
            }
        }

        public MyICommand ChangeTicketCommand { get; set; }

        public ChangeTicketViewModel(AppTicket t)
        {
            Ticket = t;
            oldTicket = new AppTicket(t);
            ChangeTicketCommand = new MyICommand(OnChangeTicketExecute, OnChangeTicketUnExecute);
        }

        public void OnChangeTicketExecute(object parameter)
        {
            if (parameter == null)
            {
                LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Change Ticket command.");
                if(Ticket.Price != oldTicket.Price)
                {
                    Ticket.Validate();
                    if (Ticket.IsValid)
                    {
                        if (ClientSideWCF.Instance.TicketProxy.ChangeTicket(Ticket,oldTicket))
                        {
                            LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Change Ticket command done.");
                            CommandHandler.Instance.AddAndExecute(ChangeTicketCommand, oldTicket);
                            MainWindowViewModel.Refresh.Execute(null);
                            Window.Close();
                        }
                        else
                        {
                            if (MessageBox.Show("Would you like to override it?", "Ticket modified or deleted", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                if (ClientSideWCF.Instance.TicketProxy.ChangeTicket2(Ticket))
                                {
                                    LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Force Change Ticket command done.");
                                    CommandHandler.Instance.AddAndExecute(ChangeTicketCommand, oldTicket);
                                    MainWindowViewModel.Refresh.Execute(null);
                                    Window.Close();
                                }
                            }
                            else
                            {
                                LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.ERROR, "Change Ticket command not done.");
                            }
                        }
                    }
                }
            }
            else
            {
                oldTicket = ClientSideWCF.Instance.TicketProxy.GetOne(((AppTicket)parameter).Id);
                if (ClientSideWCF.Instance.TicketProxy.ChangeTicket((AppTicket)parameter,oldTicket))
                {
                    LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Change Ticket command redo done.");
                    CommandHandler.Instance.undoObjects[CommandHandler.Instance.undoObjects.Count - 1] = new AppTicket((AppTicket)oldTicket);
                    MainWindowViewModel.Refresh.Execute(null);
                }
            }
        }

        public void OnChangeTicketUnExecute(object parameter)
        {

            oldTicket = ClientSideWCF.Instance.TicketProxy.GetOne(((AppTicket)parameter).Id);
            if (ClientSideWCF.Instance.TicketProxy.ChangeTicket((AppTicket)parameter,oldTicket))
            {
                LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Change Ticket command undo done.");
                CommandHandler.Instance.redoObjects[CommandHandler.Instance.redoObjects.Count - 1] = new AppTicket(oldTicket);
                MainWindowViewModel.Refresh.Execute(null);
            }
        }
    }
}
