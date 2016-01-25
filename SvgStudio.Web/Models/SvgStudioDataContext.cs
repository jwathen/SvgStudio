using SvgStudio.Shared.Materializer;
using SvgStudio.Shared.StorageModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class SvgStudioDataContext : DbContext, IStorageRepository
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
            modelBuilder.Entity<Fill>().HasOptional(x => x.PatternFill_Design)
                .WithMany(x => x.Fills)
                .HasForeignKey(x => x.PatternFill_DesignId);

            modelBuilder.Entity<License>().Property(x => x.RowVersion).IsRowVersion();

            modelBuilder.Entity<MarkupFragment>().Property(x => x.RowVersion).IsRowVersion();

            modelBuilder.Entity<Palette>().Property(x => x.RowVersion).IsRowVersion();

            modelBuilder.Entity<Shape>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<Shape>().HasOptional(x => x.BasicShape_MarkupFragment)
                .WithMany(x => x.BasicShapes)
                .HasForeignKey(x => x.BasicShape_MarkupFragmentId);
            modelBuilder.Entity<Shape>().HasOptional(x => x.TemplateShape_ClipPathMarkupFragment)
                .WithMany(x => x.TemplateShapeClipPaths)
                .HasForeignKey(x => x.TemplateShape_ClipPathMarkupFragmentId);
            modelBuilder.Entity<Shape>().HasOptional(x => x.TemplateShape_Template)
                .WithMany(x => x.TemplateShapes)
                .HasForeignKey(x => x.TemplateShape_TemplateId);

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

        public List<Fill> LoadFillsByPaletteId(string paletteId)
        {
            return Fills.Where(x => x.PaletteId == paletteId).ToList();
        }

        public List<Stroke> LoadStrokesByPaletteId(string paletteId)
        {
            return Strokes.Where(x => x.PaletteId == paletteId).ToList();
        }

        public Design LoadDesign(string id)
        {
            return Designs.Find(id);
        }

        public Shape LoadShape(string id)
        {
            return Shapes.Find(id);
        }

        public string LoadMarkupFragmentContent(string id)
        {
            return MarkupFragments.Where(x => x.Id == id).Select(x => x.Content).FirstOrDefault();
        }

        public Template LoadTemplate(string id)
        {
            return Templates.Find(id);
        }

        public List<DesignRegion> LoadDesignRegionsByTemplateId(string templateId)
        {
            return DesignRegions.Where(x => x.TemplateId == templateId).ToList();
        }

        public List<Shape> LoadShapesByCompatibilityTagIds(List<string> compatibilityTagIds)
        {
            throw new NotImplementedException();
        }

        public DesignRegion LoadDesignRegion(string id)
        {
            throw new NotImplementedException();
        }

        public List<CompatibilityTag> LoadCompatibilityTagsByDesignRegionId(string designRegionId)
        {
            throw new NotImplementedException();
        }
    }
}