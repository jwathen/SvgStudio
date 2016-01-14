using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class SvgStudioDataContext : DbContext
    {
        public SvgStudioDataContext(string connectionString) : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Designs
            modelBuilder.Entity<Design>().Property(x => x.RowVersion).IsRowVersion();

            // DesignRegions
            modelBuilder.Entity<DesignRegion>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<DesignRegion>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<DesignRegion>().HasMany(x => x.CompatibleShapes)
                .WithMany(x => x.CompatibleDesignRegions)
                .Map(x =>
                {
                    x.MapLeftKey("ShapeId");
                    x.MapRightKey("DesignRegionId");
                    x.ToTable("ShapeDesignRegionCompatibility");
                });

            // Fills
            modelBuilder.Entity<Fill>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<Fill>()
                .Map<SolidColorFill>(x =>
                    {
                        x.Requires("FillType").HasValue("SolidColor");
                        x.Property(solid => solid.Color).HasColumnName("SolidColorFill_Color");
                    })
                .Map<PatternFill>(x =>
                    {
                        x.Requires("FillType").HasValue("Pattern");
                        x.Property(pattern => pattern.Name).HasColumnName("PatternFill_Name");
                        x.Property(pattern => pattern.X).HasColumnName("PatternFill_X");
                        x.Property(pattern => pattern.Y).HasColumnName("PatternFill_Y");
                        x.Property(pattern => pattern.Width).HasColumnName("PatternFill_Width");
                        x.Property(pattern => pattern.Height).HasColumnName("PatternFill_Height");
                        x.Property(pattern => pattern.PatternUnits).HasColumnName("PatternFill_PatternUnits");
                        x.Property(pattern => pattern.PatternContentUnits).HasColumnName("PatternFill_PatternContentUnits");
                        x.Property(pattern => pattern.DesignId).HasColumnName("PatternFill_DesignId");
                    });

            // Palettes
            modelBuilder.Entity<Palette>().Property(x => x.RowVersion).IsRowVersion();

            // Shapes
            modelBuilder.Entity<Shape>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<Shape>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Shape>().Property(x => x.Markup).IsRequired();
            modelBuilder.Entity<Shape>()
                .Map<BasicShape>(x =>
                    {
                        x.Requires("ShapeType").HasValue("Basic");
                    });

            // Strokes
            modelBuilder.Entity<Stroke>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<Stroke>().Property(x => x.Color).IsRequired();
            modelBuilder.Entity<Stroke>().Property(x => x.Width).IsRequired();

            // Templates
            modelBuilder.Entity<Template>().Property(x => x.RowVersion).IsRowVersion();
        }

        public DbSet<Shape> Shapes { get; set; }
        public DbSet<Design> Designs { get; set; }
        public DbSet<DesignRegion> DesignRegions { get; set; }
        public DbSet<Fill> Fills { get; set; }
        public DbSet<Palette> Palettes { get; set; }
        public DbSet<Stroke> Strokes { get; set; }
        public DbSet<Template> Templates { get; set; }
    }
}