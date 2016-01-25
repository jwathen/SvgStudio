using SvgStudio.Mobile.Core.Models.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Async;
using System.IO;
using SQLite.Net;
using SQLite.Net.Platform.Win32;
using SvgStudio.Mobile.Core.Models.Synchronization;

namespace SvgStudio.Test.TestHelpers
{
    public class TestDatabaseConnectionProvider : IDatabaseConnectionProvider
    {
        private static bool _alreadySyncedWithServer = false;
        private SQLiteConnectionWithLock _connection = null;
        private SQLiteAsyncConnection _asyncConnection = null;
        private readonly bool _automaticallySyncWithServer = true;
        private readonly string _connectionString = null;

        static TestDatabaseConnectionProvider()
        {
            foreach (string oldDatabase in Directory.GetFiles(".", "*.db3"))
            {
                if (Path.GetFileNameWithoutExtension(oldDatabase) != "Test")
                {
                    File.Delete(oldDatabase);
                }
            }
        }

        public TestDatabaseConnectionProvider(bool automaticallySyncWithServer = true)
        {
            if (automaticallySyncWithServer)
            {
                _connectionString = "Test.db3";
            }
            else
            {
                _connectionString = string.Format("{0}.db3", Guid.NewGuid().ToString("n").Substring(0, 8).ToLower());
            }

            _automaticallySyncWithServer = automaticallySyncWithServer;
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            if (_asyncConnection == null)
            {
                _asyncConnection = new SQLiteAsyncConnection(GetConnection);
            }
            return _asyncConnection;
        }

        public SQLiteConnectionWithLock GetConnection()
        {
            if (_connection == null)
            {
                var win32Platform = new SQLitePlatformWin32();
                _connection = new SQLiteConnectionWithLock(win32Platform, new SQLiteConnectionString(_connectionString, true));
                DatabaseInitilizer.Init(_connection);
            }

            if (_automaticallySyncWithServer && !_alreadySyncedWithServer)
            {
                var mobileService = new TestMobileServiceGateway();
                var subject = new DatabaseSynchronizer(this, mobileService);
                _alreadySyncedWithServer = true;
                Task.WaitAll(subject.SynchronizeModelWithServer());
            }

            return _connection;
        }
    }
}
