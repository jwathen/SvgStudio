using FluentValidation;
using SvgStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace SvgStudio.Web.ViewModels.Shared
{
    public class DesignRegionViewModel
    {
        public string Id { get; set; }
        public string TemplateId { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string SortOrder { get; set; }

        public bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(Name)
                && string.IsNullOrWhiteSpace(X)
                && string.IsNullOrWhiteSpace(Y)
                && string.IsNullOrWhiteSpace(Width)
                && string.IsNullOrWhiteSpace(Height)
                && string.IsNullOrWhiteSpace(SortOrder);
        }

        public bool IsNew()
        {
            return string.IsNullOrWhiteSpace(Id);
        }

        public static async Task<DesignRegionViewModel> BuildAsync(SvgStudio.Shared.StorageModel.DesignRegion designRegion)
        {
            var viewModel = new DesignRegionViewModel();
            if (designRegion != null)
            {
                viewModel.Id = designRegion.Id;
                viewModel.TemplateId = designRegion.TemplateId;
                viewModel.IsActive = designRegion.IsActive;
                viewModel.Name = designRegion.Name;
                viewModel.X = designRegion.X.ToString();
                viewModel.Y = designRegion.Y.ToString();
                viewModel.Width = designRegion.Width.ToString();
                viewModel.Height = designRegion.Height.ToString();
                viewModel.SortOrder = designRegion.SortOrder.ToString();
            }

            return viewModel;
        }

        public async Task SaveAsync(SvgStudio.Shared.StorageModel.Template template, bool executeSaveChanges = false)
        {
            if (IsEmpty())
            {
                return;
            }

            var db = SvgStudioDataContext.Current;

            SvgStudio.Shared.StorageModel.DesignRegion designRegion = null;
            if (IsNew())
            {
                designRegion = new SvgStudio.Shared.StorageModel.DesignRegion();
                db.DesignRegions.Add(designRegion);
            }
            else
            {
                designRegion = await db.DesignRegions.FirstAsync(x => x.Id == Id);
            }

            designRegion.Template = template;
            designRegion.Height = int.Parse(this.Height);
            designRegion.IsActive = this.IsActive;
            designRegion.Name = this.Name;
            designRegion.SortOrder = short.Parse(this.SortOrder);
            designRegion.Width = int.Parse(this.Width);
            designRegion.X = int.Parse(this.X);
            designRegion.Y = int.Parse(this.Y);

            if (executeSaveChanges)
            {
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(bool executeSaveChanges = false)
        {
            if (!IsNew())
            {
                var db = SvgStudioDataContext.Current;
                var designRegion = await db.DesignRegions.FirstAsync(x => x.Id == Id);
                db.DesignRegions.Remove(designRegion);

                if (executeSaveChanges)
                {
                    await db.SaveChangesAsync();
                }
            }
        }
    }

    public class DesignRegionViewModelValidator : AbstractValidator<DesignRegionViewModel>
    {
        public DesignRegionViewModelValidator()
        {
            When(x => !x.IsEmpty(), () =>
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Design region name is required.");
                RuleFor(x => x.X).Must(BeANumber).WithMessage("Design region Translate X is invalid.");
                RuleFor(x => x.Y).Must(BeANumber).WithMessage("Design region Translate Y is invalid.");
                RuleFor(x => x.Width).Must(BeAPositiveNumber).WithMessage("Design region width is invalid.");
                RuleFor(x => x.Height).Must(BeAPositiveNumber).WithMessage("Design region height is invalid.");
                RuleFor(x => x.SortOrder).Must(BeAPositiveNumber).WithMessage("Design region sort order is invalid.");
            });
        }

        public bool BeANumber(string input)
        {
            int number = 0;
            return int.TryParse(input, out number);
        }

        public bool BeAPositiveNumber(string input)
        {
            int number = 0;
            if (int.TryParse(input, out number))
            {
                return number >= 0;
            }

            return false;
        }
    }
}