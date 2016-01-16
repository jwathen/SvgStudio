using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SvgStudio.Mobile.Core.Models.Storage;
using System.IO;
using SQLite.Net.Platform.XamarinAndroid;
using SQLite.Net;
using SQLite.Net.Async;
using Xamarin.Forms;

[assembly: Dependency(typeof(SvgStudio.Mobile.Droid.DroidDatabaseConnectionProvider))]

namespace SvgStudio.Mobile.Droid
{
    public class DroidDatabaseConnectionProvider : IDatabaseConnectionProvider
    {
        private static SQLiteConnectionWithLock _connection = null;

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            return new SQLiteAsyncConnection(GetConnection);
        }

        public SQLiteConnectionWithLock GetConnection()
        {
            if (_connection == null)
            {
                var sqliteFilename = "Data.db3";
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var path = Path.Combine(documentsPath, sqliteFilename);
                var platform = new SQLitePlatformAndroid();
                var connectionString = new SQLiteConnectionString(path, true);
                _connection = new SQLiteConnectionWithLock(platform, connectionString);
            }
            return _connection;
        }
    }
}