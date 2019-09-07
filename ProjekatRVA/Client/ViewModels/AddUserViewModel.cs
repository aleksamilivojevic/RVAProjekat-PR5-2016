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
    public class AddUserViewModel : BindableBase
    {
        public Window Window { get; set; }

        AppUser user=new AppUser();
        public AppUser User
        {
            get { return user; }
            set {
                user = value;
                OnPropertyChanged("User");
            }
        }

        string checkUsername;

        public string CheckUsername
        {
            get { return checkUsername; }
            set
            {
                checkUsername = value;
                OnPropertyChanged("CheckUsername");
            }
        }

        public MyICommand AddUserCommand {get;set;}

        public AddUserViewModel()
        {
            AddUserCommand = new MyICommand(OnAddUser);
        }

        public void OnAddUser(object parameter)
        {
                CheckUsername = "";
                user.Validate();

                if (user.IsValid)
                {
                    User.Type = USER_TYPE.USER;


                    if (ClientSideWCF.Instance.UserProxy.AddUser(User))
                    {
                        MessageBox.Show("Success");
                    LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Add User info command done.");
                      
                        Window.Close();
                    }
                    else
                    {
                        LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.ERROR, "Add User info command (username alredy exists) not done.");
                        CheckUsername = "Username alredy exists.";
                    }
                }
            }
         
    }
}
