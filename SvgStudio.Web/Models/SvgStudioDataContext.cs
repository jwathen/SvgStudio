using SvgStudio.Shared.StorageModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class SvgStudioDataContext : DbContext
    {
        public static SvgStudioDataContext Current
        {
            get
            {
                return (SvgStudioDataContext)HttpContext.Current.Items["SvgStudioDataContext.Current"];
            }
        }

        public SvgStudioDataContext(string connectionString)
            : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompatibilityTag>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<ContentLicense>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<Design>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<DesignRegion>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<DesignRegion_CompatibilityTag>().Ignore(x => x.RowVersion);
            modelBuilder.Entity<DesignRegion_CompatibilityTag>().HasKey(x => x.Id);
            modelBuilder.Entity<Fill>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<License>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<MarkupFragment>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<Palette>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<Shape>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<Shape_CompatibilityTag>().Ignore(x => x.RowVersion);
            modelBuilder.Entity<Shape_CompatibilityTag>().HasKey(x => x.Id);
            modelBuilder.Entity<Stroke>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<Template>().Property(x => x.RowVersion).IsRowVersion();
        }

        public DbSet<CompatibilityTag> CompatibilityTags { get; set; }
        public DbSet<ContentLicense> ContentLicenses { get; set; }
        public DbSet<Design> Designs { get; set; }
        public DbSet<DesignRegion> DesignRegions { get; set; }
        public DbSet<DesignRegion_CompatibilityTag> DesignRegion_CompatibilityTags { get; set; }
        public DbSet<Fill> Fills { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<MarkupFragment> MarkupFragments { get; set; }
        public DbSet<Palette> Palettes { get; set; }
        public DbSet<Shape> Shapes { get; set; }
        public DbSet<Shape_CompatibilityTag> Shape_CompatibilityTags { get; set; }
        public DbSet<Stroke> Strokes { get; set; }
        public DbSet<Template> Templates { get; set; }
    }
}