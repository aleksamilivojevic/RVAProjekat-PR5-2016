using Client.ViewModels;
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
    /// Interaction logic for AddTicket.xaml
    /// </summary>
    public partial class AddTicket : Window
    {
        public AddTicket()
        {
            InitializeComponent();
            DataContext = new AddTicketViewModel() { Window = this };
        }
    }
}
