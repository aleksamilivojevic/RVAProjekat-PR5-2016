using Common.Interfaces;
using Server.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ServerSideWCF
    {
        #region Fields

        private static ServiceHost UserServiceHost;
        private static ServiceHost TicketItemServiceHost;
        private static ServiceHost ServiceHost;
        private static ServiceHost TicketServiceHost;
        private static ServiceHost LogServiceHost;

        #endregion
        public ServerSideWCF()
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

            UserServiceHost = new ServiceHost(typeof(UserService));
            UserServiceHost.AddServiceEndpoint(typeof(IUserService), binding, new Uri("net.tcp://localhost:11000/IUserService"));

            TicketItemServiceHost = new ServiceHost(typeof(TicketItemService));
            TicketItemServiceHost.AddServiceEndpoint(typeof(ITicketItemService), new NetTcpBinding(), new Uri("net.tcp://localhost:11000/ITicketItemService"));

            TicketServiceHost = new ServiceHost(typeof(TicketService));
            TicketServiceHost.AddServiceEndpoint(typeof(ITicketService), new NetTcpBinding(), new Uri("net.tcp://localhost:11000/ITicketService"));

            ServiceHost = new ServiceHost(typeof(Service));
            ServiceHost.AddServiceEndpoint(typeof(IService), new NetTcpBinding(), new Uri("net.tcp://localhost:11000/IService"));

            LogServiceHost = new ServiceHost(typeof(Logger));
            LogServiceHost.AddServiceEndpoint(typeof(ILogger), new NetTcpBinding(), new Uri("net.tcp://localhost:11000/ILogger"));


        }

        public void Open()
        {
            UserServiceHost.Open();
            TicketItemServiceHost.Open();
            TicketServiceHost.Open();
            ServiceHost.Open();
            LogServiceHost.Open();
            Console.WriteLine("Service hosts are opened at " + DateTime.Now);
        }

        public void Close()
        {
            UserServiceHost.Close();
            TicketItemServiceHost.Close();
            TicketServiceHost.Close();
            ServiceHost.Close();
            LogServiceHost.Close();
            Console.WriteLine("Service hosts are closed at " + DateTime.Now);
        }
    }
}
