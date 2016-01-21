using FluentValidation;
using FluentValidation.Attributes;
using SvgStudio.Shared.StorageModel;
using SvgStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Threading;
using System.Xml.Linq;
using System.Web.Mvc;
using SvgStudio.Web.ViewModels.Shared;
using SvgStudio.Shared.Materializer;

namespace SvgStudio.Web.ViewModels.Shapes
{
    [Validator(typeof(ShapeViewModelValidator))]
    public class ShapeViewModel
    {
        private string _compatibilityTagOptions = null;

        public string Action { get; set; }
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public ShapeType ShapeType { get; set; }
        public string Name { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string LicenseId { get; set; }
        public string ContentUrl { get; set; }
        public string AttributionUrl { get; set; }
        public string AttributionName { get; set; }
        public string CompatibilityTags { get; set; }

        public MarkupFragmentViewModel BasicShape_MarkupFragment { get; set; }

        public bool IsNew()
        {
            return string.IsNullOrWhiteSpace(Id);
        }

        public IEnumerable<SelectListItem> LicenseOptions
        {
            get
            {
                var db = SvgStudioDataContext.Current;
                return (from x in db.Licenses
                        select new SelectListItem
                        {
                            Text = x.LicenseName,
                            Value = x.Id
                        }).ToList();
            }
        }

        public string CompatibilityTagOptions
        {
            get
            {
                if (_compatibilityTagOptions == null)
                {
                    var db = SvgStudioDataContext.Current;
                    _compatibilityTagOptions = string.Join(", ", db.CompatibilityTags.Select(x => x.Tag).ToList());
                }
                return _compatibilityTagOptions;
            }
        }

        public string[] GetInvalidCompatiblityTags()
        {
            string[] tags = ExtractCompatiblityTags();
            string[] validTags = CompatibilityTagOptions.Split(',').Select(x => x.Trim()).ToArray();

            return tags.Where(x => !validTags.Contains(x, StringComparer.InvariantCultureIgnoreCase)).ToArray();
        }

        public string[] ExtractCompatiblityTags()
        {
            string[] tags = (CompatibilityTags ?? string.Empty)
                .Split(',')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
            return tags;
        }

        public IEnumerable<SelectListItem> NumberOfStrokeAndFillOptions
        {
            get
            {
                return (from x in Enumerable.Range(0, 11)
                        select new SelectListItem
                        {
                            Text = x.ToString(),
                            Value = x.ToString()
                        }).ToList();
            }
        }

        public static async Task<ShapeViewModel> BuildAsync(Shape shape)
        {
            var db = SvgStudioDataContext.Current;
            var viewModel = new ShapeViewModel();
            viewModel.ShapeType = ShapeType.Basic;

            if (shape != null)
            {
                viewModel.Id = shape.Id;
                viewModel.IsActive = shape.IsActive;
                viewModel.ShapeType = shape.ShapeType;
                viewModel.Name = shape.Name;
                viewModel.Width = shape.Width.ToString();
                viewModel.Height = shape.Height.ToString();
            }

            viewModel.BasicShape_MarkupFragment = await MarkupFragmentViewModel.BuildAsync(shape?.BasicShape_MarkupFragmentId);

            if (!viewModel.IsNew())
            {
                var contentLicense = await db.ContentLicenses.FirstOrDefaultAsync(x => x.ShapeId == shape.Id);
                if (contentLicense != null)
                {
                    viewModel.LicenseId = contentLicense.LicenseId;
                    viewModel.ContentUrl = contentLicense.ContentUrl;
                    viewModel.AttributionUrl = contentLicense.AttributionUrl;
                    viewModel.AttributionName = contentLicense.AttributionName;
                }

                var compatibilityTagIds = await db.Shape_CompatibilityTags
                    .Where(x => x.ShapeId == shape.Id)
                    .Select(x => x.CompatibilityTagId)
                    .ToListAsync();
                var compatibilityTags = await db.CompatibilityTags
                    .Where(x => compatibilityTagIds.Contains(x.Id))
                    .Select(x => x.Tag)
                    .ToListAsync();

                viewModel.CompatibilityTags = string.Join(", ", compatibilityTags);
            }

            return viewModel;
        }

        public async Task SaveAsync(bool executeSaveChanges = false)
        {
            var db = SvgStudioDataContext.Current;

            Shape shape = null;
            if (this.IsNew())
            {
                shape = new Shape();
                shape.Id = UniqueId.Generate();
                db.Shapes.Add(shape);
            }
            else
            {
                shape = await db.Shapes.FirstAsync(x => x.Id == this.Id);
            }
            
            shape.IsActive = this.IsActive;
            shape.ShapeType = this.ShapeType;
            shape.Name = this.Name;
            shape.Width = double.Parse(this.Width);
            shape.Height = double.Parse(this.Height);

            ContentLicense contentLicense = await db.ContentLicenses.FirstOrDefaultAsync(x => x.ShapeId == this.Id);
            if (contentLicense == null)
            {
                contentLicense = new ContentLicense();
                contentLicense.Id = UniqueId.Generate();
                contentLicense.ShapeId = shape.Id;
                db.ContentLicenses.Add(contentLicense);
            }
            contentLicense.LicenseId = this.LicenseId;
            contentLicense.ContentUrl = this.ContentUrl;
            contentLicense.AttributionUrl = this.AttributionUrl;
            contentLicense.AttributionName = this.AttributionName;


            var newTags = ExtractCompatiblityTags();
            var existingTags = await (from sct in db.Shape_CompatibilityTags
                                      join ct in db.CompatibilityTags on sct.CompatibilityTagId equals ct.Id
                                      where sct.ShapeId == this.Id
                                      select ct.Tag).ToArrayAsync();

            var tagsToAdd = newTags.Except(existingTags);
            var tagsToRemove = existingTags.Except(newTags);
            foreach (string tag in tagsToAdd)
            {
                var compatibilityTag = await db.CompatibilityTags.FirstOrDefaultAsync(x => x.Tag == tag);

                var shapeCompatibilityTag = new Shape_CompatibilityTag();
                shapeCompatibilityTag.ShapeId = shape.Id;
                shapeCompatibilityTag.CompatibilityTagId = compatibilityTag.Id;
                shapeCompatibilityTag.ComputeId();
                db.Shape_CompatibilityTags.Add(shapeCompatibilityTag);
            }
            foreach(string tag in tagsToRemove)
            {
                var compatibilityTag = await db.CompatibilityTags.FirstOrDefaultAsync(x => x.Tag == tag);
                var shapeCompatibilityTag = await db.Shape_CompatibilityTags.FirstOrDefaultAsync(x => x.ShapeId == this.Id && x.CompatibilityTagId == compatibilityTag.Id);
                db.Shape_CompatibilityTags.Remove(shapeCompatibilityTag);
            }

            if (ShapeType == ShapeType.Basic)
            {
                await this.BasicShape_MarkupFragment.SaveAsync();
                shape.BasicShape_MarkupFragmentId = this.BasicShape_MarkupFragment.Id;
            }

            if (executeSaveChanges)
            {
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(bool executeSaveChanges = false)
        {
            var db = SvgStudioDataContext.Current;
            db.ContentLicenses.RemoveRange(db.ContentLicenses.Where(x => x.ShapeId == this.Id));
            db.Shape_CompatibilityTags.RemoveRange(db.Shape_CompatibilityTags.Where(x => x.ShapeId == this.Id));
            db.MarkupFragments.RemoveRange(db.MarkupFragments.Where(x => x.Id == this.BasicShape_MarkupFragment.Id));
            db.Shapes.RemoveRange(db.Shapes.Where(x => x.Id == this.Id));
            
            if (executeSaveChanges)
            {
                await db.SaveChangesAsync();
            }
        }
    }

    public class ShapeViewModelValidator : AbstractValidator<ShapeViewModel>
    {
        public ShapeViewModelValidator()
        {
            When(x => x.Action == "Save", () =>
            {
                // Licensing Information
                RuleFor(x => x.LicenseId).NotEmpty().WithMessage("Governing license is required.");
                RuleFor(x => x.ContentUrl).NotEmpty().WithMessage("Artwork URL is required.");
                RuleFor(x => x.AttributionName).NotEmpty().When(LicenseRequiresAttribution).WithMessage("Attribute name is required because the content licence requires attribution.");
                RuleFor(x => x.AttributionUrl).NotEmpty().When(LicenseRequiresAttribution).WithMessage("Attribute URL is required because the content licence requires attribution.");

                // Rendering Data
                RuleFor(x => x.Name).NotEmpty().WithMessage("Shape name is required.");
                RuleFor(x => x.Name).Must(BeUnique).When(IsNew).WithMessage("Shape names must be unique.  There is already a shape named \"{0}\".", x => x.Name);
                RuleFor(x => x.Width).Must(BeAPositiveNumber).WithMessage("Width must be positive.");
                RuleFor(x => x.Height).Must(BeAPositiveNumber).WithMessage("Width must be positive.");
                RuleFor(x => x.CompatibilityTags).Must(AllBeValidTags).WithMessage("Invalid compatibility tag(s): {0}", x => string.Join(", ", x.GetInvalidCompatiblityTags()));

                RuleFor(x => x.BasicShape_MarkupFragment).SetValidator(new MarkupFragmentViewModelValidator()).When(IsBasicShape);
            });
        }

        public bool BeUnique(string name)
        {
            var db = SvgStudioDataContext.Current;
            bool alreadyExists = db.Shapes.Any(x => x.Name == name);
            return !alreadyExists;
        }

        public bool BeAPositiveNumber(string input)
        {
            double number = 0;
            if (double.TryParse(input, out number))
            {
                return number >= 0;
            }

            return false;
        }

        public bool AllBeValidTags(ShapeViewModel model, string compatibilityTags)
        {
            return !model.GetInvalidCompatiblityTags().Any();
        }

        public bool IsNew(ShapeViewModel model)
        {
            return model.IsNew();
        }

        public bool LicenseRequiresAttribution(ShapeViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.LicenseId))
            {
                var db = SvgStudioDataContext.Current;
                var license = db.Licenses.FirstOrDefault(x => x.Id == model.LicenseId);
                if (license != null)
                {
                    return license.AttributionRequired;
                }
            }

            return false;
        }

        public bool IsBasicShape(ShapeViewModel model)
        {
            return model.ShapeType == ShapeType.Basic;
        }
    }
}