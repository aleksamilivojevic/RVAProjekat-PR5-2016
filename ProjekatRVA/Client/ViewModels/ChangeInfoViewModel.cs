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
    public class ChangeInfoViewModel : BindableBase
    {
        public Window Window { get; set; }

        AppUser user = new AppUser();

        public AppUser User
        {
            get { return user; }
            set
            {
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

        public MyICommand ChangeInfoCommand { get; set; }

        public ChangeInfoViewModel()
        {
            User = CurrentUser.Instance;
            ChangeInfoCommand = new MyICommand(OnChangeInfo);
        }

        public void OnChangeInfo(object parameter)
        {
            LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Change User info command.");

            CheckUsername = "";
            User.Validate();

            if (User.IsValid)
            {
                if (ClientSideWCF.Instance.UserProxy.ChangeInfo(User))
                {
                    MessageBox.Show("Success");
                    LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.INFO, "Change User info command done.");

                    CurrentUser.Instance.FirstName = User.FirstName;
                    CurrentUser.Instance.LastName = User.LastName;
                    CurrentUser.Instance.Username = User.Username;
                    CurrentUser.Instance.Password = User.Password;

                    Window.Close();
                }
                else
                {
                    LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.ERROR, "Change User info command not done.");
                }
            }
            else
            {
                LoggerHelper.Instance.LogManagerLogging(LOG_TYPE.ERROR, "Change User info command (User info not valid) not done.");
            }
        }
    }
}
