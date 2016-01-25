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
using SvgStudio.Web.Models;
using System.Configuration;

namespace SvgStudio.Test.Mobile.Core.Models.Synchronization
{
    public class ModelSynchronizerTests
    {
        private readonly IDatabaseConnectionProvider _databaseConnectionProvider = new TestDatabaseConnectionProvider(automaticallySyncWithServer: false);
        private readonly SvgStudioDataContext _db = new SvgStudioDataContext(ConfigurationManager.ConnectionStrings["SvgStudio"].ConnectionString);

        public ModelSynchronizerTests()
        {
            ServerDatabase.Reset();
        }

        public async Task AddsNewRecords()
        {
            var syncResult = await SynchronizesMobileDatabasesWithServer();

            AssertSync<CompatibilityTag>(syncResult, added: _db.CompatibilityTags.Count());
            AssertSync<ContentLicense>(syncResult, added: _db.ContentLicenses.Count());
            AssertSync<Design>(syncResult, added: _db.Designs.Count());
            AssertSync<DesignRegion>(syncResult, added: _db.DesignRegions.Count());
            AssertSync<DesignRegion_CompatibilityTag>(syncResult, added: _db.DesignRegion_CompatibilityTags.Count());
            AssertSync<Fill>(syncResult, added: _db.Fills.Count());
            AssertSync<License>(syncResult, added: _db.Licenses.Count());
            AssertSync<MarkupFragment>(syncResult, added: _db.MarkupFragments.Count());
            AssertSync<Palette>(syncResult, added: _db.Palettes.Count());
            AssertSync<Shape>(syncResult, added: _db.Shapes.Count());
            AssertSync<Shape_CompatibilityTag>(syncResult, added: _db.Shape_CompatibilityTags.Count());
            AssertSync<Stroke>(syncResult, added: _db.Strokes.Count());
            AssertSync<Template>(syncResult, added: _db.Templates.Count());
        }

        public async Task UpdatesExistingRecords()
        {
            var syncResult = await SynchronizesMobileDatabasesWithServer();

            // Update the first row in each table with random data.
            // The many-to-many tables can only be inserted or deleted
            // so they are not included in this test.
            _db.CompatibilityTags.First().Tag = UniqueId.Generate();
            _db.ContentLicenses.First().AttributionName = UniqueId.Generate();
            _db.DesignRegions.First().Name = UniqueId.Generate();
            _db.Fills.First().SolidColorFill_Color = UniqueId.Generate();
            _db.Licenses.First().LicenseUrl = UniqueId.Generate();
            _db.MarkupFragments.First().Content = UniqueId.Generate();
            _db.Palettes.First().Name = UniqueId.Generate();
            _db.Shapes.First().Name = UniqueId.Generate();
            _db.Strokes.First().Color = UniqueId.Generate();
            _db.Templates.First().Name = UniqueId.Generate();
            _db.SaveChanges();

            syncResult = await SynchronizesMobileDatabasesWithServer();

            AssertSync<CompatibilityTag>(syncResult, updated: 1);
            AssertSync<ContentLicense>(syncResult, updated: 1);
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

            int compatibilityTagsCount = _db.CompatibilityTags.Count();
            int contentLicensesCount = _db.ContentLicenses.Count();
            int designsCount = _db.Designs.Count();
            int designRegionsCount = _db.DesignRegions.Count();
            int designRegion_CompatibilityTagsCount = _db.DesignRegion_CompatibilityTags.Count();
            int fillsCount = _db.Fills.Count();
            int licensesCount = _db.Licenses.Count();
            int markupFragmentsCount = _db.MarkupFragments.Count();
            int palettesCount = _db.Palettes.Count();
            int shapesCount = _db.Shapes.Count();
            int shape_CompatibilityTagsCount = _db.Shape_CompatibilityTags.Count();
            int strokesCount = _db.Strokes.Count();
            int templatesCount = _db.Templates.Count();

            // Remove all records.
            ServerDatabase.Empty();

            syncResult = await SynchronizesMobileDatabasesWithServer();

            AssertSync<CompatibilityTag>(syncResult, deleted: compatibilityTagsCount);
            AssertSync<ContentLicense>(syncResult, deleted: contentLicensesCount);
            AssertSync<Design>(syncResult, deleted: designsCount);
            AssertSync<DesignRegion>(syncResult, deleted: designRegionsCount);
            AssertSync<DesignRegion_CompatibilityTag>(syncResult, deleted: designRegion_CompatibilityTagsCount);
            AssertSync<Fill>(syncResult, deleted: fillsCount);
            AssertSync<License>(syncResult, deleted: licensesCount);
            AssertSync<MarkupFragment>(syncResult, deleted: markupFragmentsCount);
            AssertSync<Palette>(syncResult, deleted: palettesCount);
            AssertSync<Shape>(syncResult, deleted: shapesCount);
            AssertSync<Shape_CompatibilityTag>(syncResult, deleted: shape_CompatibilityTagsCount);
            AssertSync<Stroke>(syncResult, deleted: strokesCount);
            AssertSync<Template>(syncResult, deleted: templatesCount);
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
    }
}
