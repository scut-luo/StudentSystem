using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DatabaseControlLib;

namespace ManagementSystem
{
    class ConnectionInfo
    {
        public static string serverName = "(local)";
        public static string dbName = "StudentSystem";
    }
    public class SQLConnection
    {
        public static DBase GetDataBase()
        {
            return (new SQLServerDatabase(ConnectionInfo.dbName,
                ConnectionInfo.serverName));
        }
    }
}
