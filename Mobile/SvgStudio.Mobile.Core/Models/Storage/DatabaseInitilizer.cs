using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Mobile.Core.Models.Storage
{
    public static class DatabaseInitilizer
    {
        public static void Init(SQLiteConnectionWithLock connection)
        {
            connection.CreateTable<License>();
        }
    }
}
