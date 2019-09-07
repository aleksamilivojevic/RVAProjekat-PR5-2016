using Client.Helpers;
using Client.Views;
using Common.AppModel;
using Common.Helpers;
using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public Window Window { get; set; }
        string adminVisibility;


        public string AdminVisibility
        {
            get { return adminVisibility; }
            set
            {
                adminVisibility = value;
                OnPropertyChanged("AdminVisibility");
            }
        }

        private BindingList<Theater> theaters { get; set; }
        public BindingList<Theater> Theaters
        {
            get
            {
                return theaters;
            }
            set
            {
                theaters = value;
                OnPropertyChanged("Theaters");
            }
        }

        private BindingList<Log> loggers { get; set; }
        public BindingList<Log> Loggers
        {
            get
            {
                return loggers;
            }
            set
            {
                loggers = value;
                OnPropertyChanged("Loggers");
            }
        }

        private BindingList<AppTicket> tickets { get; set; }
        public BindingList<AppTicket> Tickets
        {
            get
            {
                return tickets;
            }
            set
            {
                tickets = value;
                OnPropertyChanged("Tickets");
            }
        }
        private BindingList<string> priceConditionContent { get; set; }
        public BindingList<string> PriceConditionContent
        {
            get
            {
                return priceConditionContent;
            }
            set
            {
                priceConditionContent = value;
                OnPropertyChanged("PriceConditionContent");
            }
        }

        private string selectedPriceContent;
        public string SelectedPriceContent
        {
            get { return selectedPriceContent; }
            set
            {
                selectedPriceContent = value;
                OnPropertyChanged("SelectedPriceContent");
            }
        }
        private string priceContent;
        public string PriceContent
        {
            get { return priceContent; }
            set
            {
                priceContent = value;
                OnPropertyChanged("PriceContent");
            }
        }
        private BindingList<AppTicketItem> ticketItems { get; set; }
        public BindingList<AppTicketItem> TicketItems
        {
            get
            {
                return ticketItems;
            }
            set
            {
                ticketItems = value;
                OnPropertyChanged("TicketItems");
            }
        }

        public AppTicket SelectedTicket { get; set; }
        public AppTicketItem SelectedTicketItem { get; set; }
        public MyICommand AddUserCommand { get; set; }
        public MyICommand ChangeInfoCommand { get; set; }
        public MyICommand LogoutCommand { get; set; }

        public MyICommand AddTicketCommand { get; set; }

        public MyICommand ChangeTicketCommand { get; set; }

        public MyICommand AddTicketItemCommand { get; set; }

        public MyICommand ChangeTicketItemCommand { get; set; }

        public MyICommand DeleteTicketItemCommand { get; set; }

        public MyICommand DeleteTicketCommand { get; set; }

        public MyICommand SearchCommand { get; set; }
        public MyICommand UndoCommand { get; set; }
        public MyICommand RedoCommand { get; set; }
        public MyICommand RefreshCommand { get; set; }

        public MyICommand CloneTicketCommand { get; set; }

        public static MyICommand Refresh { get; set; }
        public MainWindowViewModel()
        {
            LoggerHelper.Instance.MainWindowViewModel = this;
            Loggers = new BindingList<Log>();
            PriceConditionContent = new BindingList<string>() { "<", "=", ">" };
            Theaters = ClientSideWCF.Instance.Proxy.GetAllTheaters();
            Tickets = ClientSideWCF.Instance.TicketProxy.GetAll();
            TicketItems = ClientSideWCF.Instance.TicketItemProxy.GetAll();
            if (CurrentUser.Instance.Type == USER_TYPE.ADMIN)
            {
                AdminVisibility = "Visible";
                AddUserCommand = new MyICommand(OnAddUser);
            }
            else
            {
                AdminVisibility = "Hidden";
            }
            ChangeInfoCommand = new MyICommand(OnChangeInfo);
            LogoutCommand = new MyICommand(OnLogout);
            AddTicketCommand = new MyICommand(OnAddTicket);
            ChangeTicketCommand = new MyICommand(OnChangeTicket);
            AddTicketItemCommand = new MyICommand(OnAddTicketItem);
            ChangeTicketItemCommand = new MyICommand(OnChangeTicketItem);
            DeleteTicketCommand = new MyICommand(OnDeleteTicketExecute, OnDeleteTicketUnExecute);
            DeleteTicketItemCommand = new MyICommand(OnDeleteTicketItemExecute, OnDeleteTicketItemUnExecute);
            CloneTicketCommand = new MyICommand(OnCloneTicket);
            SearchCommand = new MyICommand(OnSearch);
            RefreshCommand = new MyICommand(OnRefresh);
            Refresh = new MyICommand(OnRefresh);
            UndoCommand = new MyICommand(OnUndo);
            RedoCommand = new MyICommand(OnRedo);
        }

        public void OnSearch(object parameter)
        {
            if (SelectedPriceContent != null && (priceContent != null || priceContent != "") )
            {
                try
                {
                    int.Parse(priceContent);
                    Tickets = ClientSideWCF.Instance.TicketProxy.Search(priceContent, selectedPriceContent);
                }
                catch
                {
                    PriceContent = null;
                    PriceConditionContent = null;
                    PriceConditionContent = new BindingList<string>() { "<", "=", ">" };
                }
                
                
            }
        }
        public void OnCloneTicket(object parameter)
        {
            if (SelectedTicket != null)
            {
                ClientSideWCF.Instance.TicketProxy.CloneTicket(SelectedTicket);

                OnRefresh(null);
            }
        }

        public bool CanUndo
        {
            get
            {
                if (CommandHandler.Instance.undoCommands.Count != 0)
                    return true;
                else
                    return false;
            }
        }

        public void OnUndo(object parameter)
        {
            LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Undo operation.");
            if (CanUndo)
            {
                CommandHandler.Instance.Undo();
            }
        }



        public bool CanRedo
        {
            get
            {
                if (CommandHandler.Instance.redoCommands.Count != 0)
                    return true;
                else
                    return false;
            }
        }


       

        public void OnRedo(object parameter)
        {
            LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Redo operation.");
            if (CanRedo)
            {
                CommandHandler.Instance.Redo();
            }
        }
        public void OnRefresh(object parameter)
        {
            PriceContent = null;
            PriceConditionContent = null;
            PriceConditionContent = new BindingList<string>() { "<", "=", ">" };

            Theaters = ClientSideWCF.Instance.Proxy.GetAllTheaters();
            Tickets = ClientSideWCF.Instance.TicketProxy.GetAll();
            TicketItems = ClientSideWCF.Instance.TicketItemProxy.GetAll();
        }

        public void OnDeleteTicketExecute(object parameter)
        {

            if (parameter == null)
            {

                if (SelectedTicket != null)
                {
                    LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Delete Ticket command.");
                    if (ClientSideWCF.Instance.TicketProxy.DeleteTicket(SelectedTicket))
                    {
                        CommandHandler.Instance.AddAndExecute(DeleteTicketCommand, SelectedTicket);
                        LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Delete Ticket command done.");
                        OnRefresh(null);
                    }
                    else
                    {
                        if (MessageBox.Show("Would you like to override it?", "Ticket modified or deleted", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            if (ClientSideWCF.Instance.TicketProxy.DeleteTicket2(SelectedTicket))
                            {
                                LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Force delete Ticket command done.");
                                CommandHandler.Instance.AddAndExecute(DeleteTicketCommand, SelectedTicket);
                               
                            }
                        }
                        else
                        {
                            LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.ERROR, "Change Ticket command not done.");
                        }
                        MainWindowViewModel.Refresh.Execute(null);
                    }
                }
                else
                {
                    MessageBox.Show("Select ticket first.");
                }
            }
            else
            {
                if (ClientSideWCF.Instance.TicketProxy.DeleteTicket((AppTicket)parameter))
                {
                    LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Delete Ticket command redo done.");
                    CommandHandler.Instance.undoObjects[CommandHandler.Instance.undoObjects.Count - 1] = new AppTicket((AppTicket)parameter);
                    OnRefresh(null);
                }
            }
        }

        public void OnDeleteTicketUnExecute(object parameter)
        {
            int id;
            if ((id = ClientSideWCF.Instance.TicketProxy.AddTicket((AppTicket)parameter)) != -1)
            {
                LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Delete Ticket command undo done.");
                AppTicket ticket = ClientSideWCF.Instance.TicketProxy.GetOne(id);
                CommandHandler.Instance.redoObjects[CommandHandler.Instance.redoObjects.Count - 1] = new AppTicket(ticket);
                OnRefresh(null);
            }
        }

        public void OnDeleteTicketItemExecute(object parameter)
        {

            if (parameter == null)
            {

                if (SelectedTicketItem != null)
                {
                    LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Delete Ticket Item command.");
                    if (ClientSideWCF.Instance.TicketItemProxy.DeleteTicketItem(SelectedTicketItem))
                    {
                        CommandHandler.Instance.AddAndExecute(DeleteTicketItemCommand, SelectedTicketItem);
                        LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Delete Ticket Item command done.");
                        OnRefresh(null);
                    }
                    else
                    {
                        LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.ERROR, "Delete Ticket Item command not done.");
                    }
                }
                else
                    MessageBox.Show("Select ticket item first.");
            }
            else
            {
                if (ClientSideWCF.Instance.TicketItemProxy.DeleteTicketItem((AppTicketItem)parameter))
                {
                    LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Delete Ticket Item command redo done.");
                    CommandHandler.Instance.undoObjects[CommandHandler.Instance.undoObjects.Count - 1] = new AppTicketItem((AppTicketItem)parameter);
                    OnRefresh(null);
                }
            }
        }

        public void OnDeleteTicketItemUnExecute(object parameter)
        {
            int id;
            if ((id = ClientSideWCF.Instance.TicketItemProxy.AddTicketItem((AppTicketItem)parameter)) != -1)
            {
                LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Delete Ticket Item command undo done.");
                AppTicketItem ticket = ClientSideWCF.Instance.TicketItemProxy.GetOne(id);
                CommandHandler.Instance.redoObjects[CommandHandler.Instance.redoObjects.Count - 1] = new AppTicketItem(ticket);
                OnRefresh(null);
            }
        }

        public void OnLogout(object parameter)
        {
            CurrentUser.Instance = null;
            new Login().Show();
            Window.Close();
        }
        public void OnAddUser(object parameter)
        {
            new AddUser().ShowDialog();
        }

        public void OnAddTicket(object parameter)
        {
            new AddTicket().ShowDialog();
        }

        public void OnChangeTicket(object parameter)
        {
            if (SelectedTicket != null)
            {
                new ChangeTicket(SelectedTicket).ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select ticket first");
            }

        }

        public void OnAddTicketItem(object parameter)
        {
            new AddTicketItem().ShowDialog();
        }

        public void OnChangeTicketItem(object parameter)
        {
            if (SelectedTicketItem != null)
            {
                new ChangeTicketItem(SelectedTicketItem).ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select ticket item first");
            }
           
        }

        public void OnChangeInfo(object parameter)
        {
            new ChangeInfo().ShowDialog();
        }

    }
}
