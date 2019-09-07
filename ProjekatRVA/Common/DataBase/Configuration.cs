using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataBase
{
    public class Configuration : DbMigrationsConfiguration<TheaterContext>
    {
        #region Constructor
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "TheaterContext";
        }
        #endregion
    }
}
