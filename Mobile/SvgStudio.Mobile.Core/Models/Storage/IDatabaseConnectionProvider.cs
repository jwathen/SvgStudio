using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Mobile.Core.Models.Storage
{
    public interface IDatabaseConnectionProvider
    {
        SQLiteConnectionWithLock GetConnection();

        SQLiteAsyncConnection GetAsyncConnection();
    }
}
