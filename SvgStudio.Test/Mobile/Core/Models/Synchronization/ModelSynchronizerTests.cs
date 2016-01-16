using SQLite.Net;
using SQLite.Net.Async;
using SvgStudio.Mobile.Core.Models.Storage;
using SvgStudio.Mobile.Core.Models.Synchronization;
using SvgStudio.Test.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Test.Mobile.Core.Models.Synchronization
{
    public class ModelSynchronizerTests
    {
        private readonly IDatabaseConnectionProvider _databaseConnectionProvider = new TestDatabaseConnectionProvider(automaticallySyncWithServer: false);

        public async Task SynchronizesMobileDatabasesWithServer()
        {
            var mobileService = new TestMobileServiceGateway();
            var subject = new ModelSynchronizer(_databaseConnectionProvider, mobileService);

            var response = await subject.SynchronizeModelWithServer();
        }
    }
}
