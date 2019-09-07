
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client.Helpers
{
    public class ClientSideWCF
    {
        public IUserService UserProxy { get; set; }
        public ITicketItemService TicketItemProxy{ get; set; }
        public IService Proxy { get; set; }
        public ITicketService TicketProxy { get; set; }
        public ILogger LogProxy { get; set; }

        private static ClientSideWCF instance;

        //Singleton
        public static ClientSideWCF Instance
        {
            get
            {
                if (instance == null)
                    instance = new ClientSideWCF();
                return instance;
            }

        }

        public ClientSideWCF()
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

	
            ChannelFactory<IUserService> channelFactoryUser = new ChannelFactory<IUserService>(binding, new EndpointAddress("net.tcp://localhost:11000/IUserService"));
            UserProxy = channelFactoryUser.CreateChannel();

            ChannelFactory<ITicketItemService> channelFactoryTicketItem = new ChannelFactory<ITicketItemService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:11000/ITicketItemService"));
            TicketItemProxy = channelFactoryTicketItem.CreateChannel();

            ChannelFactory<ITicketService> channelFactoryTicket = new ChannelFactory<ITicketService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:11000/ITicketService"));
            TicketProxy = channelFactoryTicket.CreateChannel();

            ChannelFactory<IService> channelFactory = new ChannelFactory<IService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:11000/IService"));
            Proxy = channelFactory.CreateChannel();
            ChannelFactory<ILogger> channelFactoryLogger = new ChannelFactory<ILogger>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:11000/ILogger"));
            LogProxy = channelFactoryLogger.CreateChannel();
        }
    }
}
