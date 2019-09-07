using Common.DataBase;
using Common.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Database
            String path = Directory.GetCurrentDirectory();
            path = path.Substring(0, path.LastIndexOf("bin"));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            #endregion

            ServerSideWCF services = new ServerSideWCF();
            new InitialData();

           

            services.Open();

            Console.ReadLine();

            services.Close();
        }
    }
}
