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
        private SvgStudio.Web.Models.SvgStudioDataContext _serverDatabase = new SvgStudio.Web.Models.SvgStudioDataContext("SvgStudio");
        private ServerData _serverFixtures = ServerData.CreateFixtures();

        public async Task SynchronizeCompatibilityTags()
        {
            _serverDatabase.CompatibilityTags.Add(_serverFixtures.CompatibilityTags["stamp"]);
            _serverDatabase.SaveChanges();
            var syncResult = await SynchronizesMobileDatabasesWithServer();
            AssertSync(syncResult, "CompatibilityTag", added: 1);

            _serverDatabase.CompatibilityTags.First().Tag = "tag1";
            _serverDatabase.SaveChanges();
            syncResult = await SynchronizesMobileDatabasesWithServer();
            AssertSync(syncResult, "CompatibilityTag", updated: 1);

            _serverDatabase.CompatibilityTags.Remove(_serverDatabase.CompatibilityTags.First());
            _serverDatabase.SaveChanges();
            syncResult = await SynchronizesMobileDatabasesWithServer();
            AssertSync(syncResult, "CompatibilityTag", deleted: 1);
        }

        public async Task SynchronizeContentLicenses()
        {
            _serverDatabase.ContentLicenses.Add(_serverFixtures.ContentLicenses["lion_license"]);
            _serverDatabase.SaveChanges();
            var syncResult = await SynchronizesMobileDatabasesWithServer();
            AssertSync(syncResult, "ContentLicense", added: 1);

            _serverDatabase.ContentLicenses.First().AttributionName = "Name";
            _serverDatabase.SaveChanges();
            syncResult = await SynchronizesMobileDatabasesWithServer();
            AssertSync(syncResult, "ContentLicense", updated: 1);

            _serverDatabase.ContentLicenses.Remove(_serverDatabase.ContentLicenses.First());
            _serverDatabase.SaveChanges();
            syncResult = await SynchronizesMobileDatabasesWithServer();
            AssertSync(syncResult, "ContentLicense", deleted: 1);
        }

        public async Task SynchronizeDesigns()
        {
            _serverDatabase.Designs.Add(_serverFixtures.Designs["gray_and_blue_lion"]);
            _serverDatabase.SaveChanges();
            var syncResult = await SynchronizesMobileDatabasesWithServer();
            AssertSync(syncResult, "Design", added: 1);

            _serverDatabase.Designs.First().Palette = _serverFixtures.Palettes["gray_and_yellow"];
            _serverDatabase.SaveChanges();
            syncResult = await SynchronizesMobileDatabasesWithServer();
            AssertSync(syncResult, "Design", updated: 1);

            _serverDatabase.Designs.Remove(_serverDatabase.Designs.First());
            _serverDatabase.SaveChanges();
            syncResult = await SynchronizesMobileDatabasesWithServer();
            AssertSync(syncResult, "Design", deleted: 1);
        }

        // TODO finish tests for database synchronization
        // DesignRegion, DesignRegion_CompatibilityTag, Fill, License, MarkupFragment, Palette, Shape, Shape_CompatibilityTag, Stroke, Template

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
            _serverDatabase.Database.Connection.Open();
            checkpoint.Reset(_serverDatabase.Database.Connection);
            _serverDatabase.Database.Connection.Close();
        }
    }
}
