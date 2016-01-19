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
using SvgStudio.Shared.StorageModel;

namespace SvgStudio.Test.Mobile.Core.Models.Synchronization
{
    public class ModelSynchronizerTests : IDisposable
    {
        private readonly IDatabaseConnectionProvider _databaseConnectionProvider = new TestDatabaseConnectionProvider(automaticallySyncWithServer: false);
        private SvgStudio.Web.Models.SvgStudioDataContext _serverDatabase = new SvgStudio.Web.Models.SvgStudioDataContext("SvgStudio");
        private ServerData _serverFixtures = null;

        public ModelSynchronizerTests()
        {
            _serverFixtures = ServerData.CreateFixtures(_serverDatabase);
        }

        public async Task AddsNewRecords()
        {
            var syncResult = await SynchronizesMobileDatabasesWithServer();

            AssertSync<CompatibilityTag>(syncResult, added: _serverFixtures.CompatibilityTags.Count);
            AssertSync<ContentLicense>(syncResult, added: _serverFixtures.ContentLicenses.Count);
            AssertSync<Design>(syncResult, added: _serverFixtures.Designs.Count);
            AssertSync<DesignRegion>(syncResult, added: _serverFixtures.DesignRegions.Count);
            AssertSync<DesignRegion_CompatibilityTag>(syncResult, added: _serverFixtures.DesignRegion_CompatibilityTags.Count);
            AssertSync<Fill>(syncResult, added: _serverFixtures.Fills.Count);
            AssertSync<License>(syncResult, added: _serverFixtures.Licenses.Count);
            AssertSync<MarkupFragment>(syncResult, added: _serverFixtures.MarkupFragments.Count);
            AssertSync<Palette>(syncResult, added: _serverFixtures.Palettes.Count);
            AssertSync<Shape>(syncResult, added: _serverFixtures.Shapes.Count);
            AssertSync<Shape_CompatibilityTag>(syncResult, added: _serverFixtures.Shape_CompatibilityTags.Count);
            AssertSync<Stroke>(syncResult, added: _serverFixtures.Strokes.Count);
            AssertSync<Template>(syncResult, added: _serverFixtures.Templates.Count);
        }

        public async Task UpdatesExistingRecords()
        {
            var syncResult = await SynchronizesMobileDatabasesWithServer();

            // Update the first row in each table with random data.
            // The many-to-many tables can only be inserted or deleted
            // so they are not included in this test.
            _serverDatabase.CompatibilityTags.First().Tag = UniqueId.Generate();
            _serverDatabase.ContentLicenses.First().AttributionName = UniqueId.Generate();
            _serverDatabase.Designs.First().ShapeId = UniqueId.Generate();
            _serverDatabase.DesignRegions.First().Name = UniqueId.Generate();
            _serverDatabase.Fills.First().SolidColorFill_Color = UniqueId.Generate();
            _serverDatabase.Licenses.First().LicenseUrl = UniqueId.Generate();
            _serverDatabase.MarkupFragments.First().Content = UniqueId.Generate();
            _serverDatabase.Palettes.First().Name = UniqueId.Generate();
            _serverDatabase.Shapes.First().Name = UniqueId.Generate();
            _serverDatabase.Strokes.First().Color = UniqueId.Generate();
            _serverDatabase.Templates.First().Name = UniqueId.Generate();
            _serverDatabase.SaveChanges();

            syncResult = await SynchronizesMobileDatabasesWithServer();

            AssertSync<CompatibilityTag>(syncResult, updated: 1);
            AssertSync<ContentLicense>(syncResult, updated: 1);
            AssertSync<Design>(syncResult, updated: 1);
            AssertSync<DesignRegion>(syncResult, updated: 1);
            AssertSync<Fill>(syncResult, updated: 1);
            AssertSync<License>(syncResult, updated: 1);
            AssertSync<MarkupFragment>(syncResult, updated: 1);
            AssertSync<Palette>(syncResult, updated: 1);
            AssertSync<Shape>(syncResult, updated: 1);
            AssertSync<Stroke>(syncResult, updated: 1);
            AssertSync<Template>(syncResult, updated: 1);
        }

        public async Task RemovesDeletedRecords()
        {
            var syncResult = await SynchronizesMobileDatabasesWithServer();

            // Remove all records.
            _serverDatabase.CompatibilityTags.RemoveRange(_serverDatabase.CompatibilityTags);
            _serverDatabase.ContentLicenses.RemoveRange(_serverDatabase.ContentLicenses);
            _serverDatabase.Designs.RemoveRange(_serverDatabase.Designs);
            _serverDatabase.DesignRegions.RemoveRange(_serverDatabase.DesignRegions);
            _serverDatabase.DesignRegion_CompatibilityTags.RemoveRange(_serverDatabase.DesignRegion_CompatibilityTags);
            _serverDatabase.Fills.RemoveRange(_serverDatabase.Fills);
            _serverDatabase.Licenses.RemoveRange(_serverDatabase.Licenses);
            _serverDatabase.MarkupFragments.RemoveRange(_serverDatabase.MarkupFragments);
            _serverDatabase.Palettes.RemoveRange(_serverDatabase.Palettes);
            _serverDatabase.Shapes.RemoveRange(_serverDatabase.Shapes);
            _serverDatabase.Shape_CompatibilityTags.RemoveRange(_serverDatabase.Shape_CompatibilityTags);
            _serverDatabase.Strokes.RemoveRange(_serverDatabase.Strokes);
            _serverDatabase.Templates.RemoveRange(_serverDatabase.Templates);
            _serverDatabase.SaveChanges();

            syncResult = await SynchronizesMobileDatabasesWithServer();

            AssertSync<CompatibilityTag>(syncResult, deleted: _serverFixtures.CompatibilityTags.Count);
            AssertSync<ContentLicense>(syncResult, deleted: _serverFixtures.ContentLicenses.Count);
            AssertSync<Design>(syncResult, deleted: _serverFixtures.Designs.Count);
            AssertSync<DesignRegion>(syncResult, deleted: _serverFixtures.DesignRegions.Count);
            AssertSync<DesignRegion_CompatibilityTag>(syncResult, deleted: _serverFixtures.DesignRegion_CompatibilityTags.Count);
            AssertSync<Fill>(syncResult, deleted: _serverFixtures.Fills.Count);
            AssertSync<License>(syncResult, deleted: _serverFixtures.Licenses.Count);
            AssertSync<MarkupFragment>(syncResult, deleted: _serverFixtures.MarkupFragments.Count);
            AssertSync<Palette>(syncResult, deleted: _serverFixtures.Palettes.Count);
            AssertSync<Shape>(syncResult, deleted: _serverFixtures.Shapes.Count);
            AssertSync<Shape_CompatibilityTag>(syncResult, deleted: _serverFixtures.Shape_CompatibilityTags.Count);
            AssertSync<Stroke>(syncResult, deleted: _serverFixtures.Strokes.Count);
            AssertSync<Template>(syncResult, deleted: _serverFixtures.Templates.Count);
        }

        private async Task<IEnumerable<TableSynchronizationSummary>> SynchronizesMobileDatabasesWithServer()
        {
            var mobileService = new TestMobileServiceGateway();
            var subject = new DatabaseSynchronizer(_databaseConnectionProvider, mobileService);
            return await subject.SynchronizeModelWithServer();
        }

        private void AssertSync<T>(IEnumerable<TableSynchronizationSummary> syncResult, int added = 0, int updated = 0, int deleted = 0)
        {
            string tableName = typeof(T).Name;
            var tableResult = syncResult.First(x => x.TableName == tableName);
            tableResult.RowsAdded.ShouldEqual(added, string.Format("Expected {0} added to {1} but found {2}.", added, tableName, tableResult.RowsAdded));
            tableResult.RowsUpdated.ShouldEqual(updated, string.Format("Expected {0} updated in {1} but found {2}.", added, tableName, tableResult.RowsUpdated));
            tableResult.RowsDeleted.ShouldEqual(deleted, string.Format("Expected {0} deleted from {1} but found {2}.", added, tableName, tableResult.RowsDeleted));
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
