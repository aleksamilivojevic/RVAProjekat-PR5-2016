using Client.ViewModels;
using Common.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for ChangeTicket.xaml
    /// </summary>
    public partial class ChangeTicket : Window
    {
        public ChangeTicket(AppTicket ticket)
        {
            InitializeComponent();
            DataContext = new ChangeTicketViewModel(ticket) { Window = this };
        }
    }
}
