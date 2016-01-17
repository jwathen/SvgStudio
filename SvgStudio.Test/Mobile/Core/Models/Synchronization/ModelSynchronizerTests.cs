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
using Should;
using SvgStudio.Test.TestHelpers.Fixtures;

namespace SvgStudio.Test.Mobile.Core.Models.Synchronization
{
    public class ModelSynchronizerTests : IDisposable
    {
        private readonly IDatabaseConnectionProvider _databaseConnectionProvider = new TestDatabaseConnectionProvider(automaticallySyncWithServer: false);
        private SvgStudio.Web.Models.SvgStudioDataContext serverDatabase = new SvgStudio.Web.Models.SvgStudioDataContext("SvgStudio");

        public async Task SynchronizeCompatibilityTags()
        {
            serverDatabase.CompatibilityTags.Add(new SvgStudio.Web.Models.CompatibilityTag { Tag = "tag" });
            serverDatabase.SaveChanges();
            var syncResult = await SynchronizesMobileDatabasesWithServer();
            AssertSync(syncResult, "CompatibilityTag", added: 1);

            serverDatabase.CompatibilityTags.First().Tag = "tag1";
            serverDatabase.SaveChanges();
            syncResult = await SynchronizesMobileDatabasesWithServer();
            AssertSync(syncResult, "CompatibilityTag", updated: 1);

            serverDatabase.CompatibilityTags.Remove(serverDatabase.CompatibilityTags.First());
            serverDatabase.SaveChanges();
            syncResult = await SynchronizesMobileDatabasesWithServer();
            AssertSync(syncResult, "CompatibilityTag", deleted: 1);
        }
    
        // TODO finish tests for database synchronization.

        private async Task<IEnumerable<TableSynchronizationSummary>> SynchronizesMobileDatabasesWithServer()
        {
            var mobileService = new TestMobileServiceGateway();
            var subject = new DatabaseSynchronizer(_databaseConnectionProvider, mobileService);
            return await subject.SynchronizeModelWithServer();
        }

        private void AssertSync(IEnumerable<TableSynchronizationSummary> syncResult, string tableName, int added = 0, int updated = 0, int deleted = 0)
        {
            var tableResult = syncResult.First(x => x.TableName == tableName);
            tableResult.RowsAdded.ShouldEqual(added);
            tableResult.RowsUpdated.ShouldEqual(updated);
            tableResult.RowsDeleted.ShouldEqual(deleted);
        }

        public void Dispose()
        {
            var checkpoint = new Respawn.Checkpoint();
            serverDatabase.Database.Connection.Open();
            checkpoint.Reset(serverDatabase.Database.Connection);
            serverDatabase.Database.Connection.Close();
        }
    }
}
