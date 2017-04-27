using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using System.IO;

namespace Odx
{
    class DbTools
    {
        public bool CreateSpeedBotDb()
        {
            string connectionString;
            string fileName = "SpeedBot.sdf";
            string password = "Sp33dB0t";
 
            if (File.Exists(fileName))
            {
                return false;
            }

            connectionString = string.Format(
            "DataSource=\"{0}\"; Password='{1}'", fileName, password);

            SqlCeEngine en = new SqlCeEngine(connectionString);
            en.CreateDatabase();


            return true;
        }
    }
}
