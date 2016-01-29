using FluentValidation;
using SvgStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using SvgStudio.Shared.StorageModel;

namespace SvgStudio.Web.ViewModels.Shared
{
    public class TemplateViewModel
    {
        public TemplateViewModel()
        {
            DesignRegions = new List<DesignRegionViewModel>();
        }

        public string Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsMaster { get; set; }
        public string Name { get; set; }

        public bool IsNew()
        {
            return string.IsNullOrWhiteSpace(Id);
        }

        public List<DesignRegionViewModel> DesignRegions { get; set; }

        public static async Task<TemplateViewModel> BuildAsync(SvgStudio.Shared.StorageModel.Template template, int numberOfDesignRegionRows)
        {
            var viewModel = new TemplateViewModel();
            if (template != null)
            {
                viewModel.Id = template.Id;
                viewModel.IsActive = template.IsActive;
                viewModel.IsMaster = template.IsMaster;
                viewModel.Name = template.Name;

                foreach (var designRegion in template.DesignRegions.OrderBy(x => x.SortOrder))
                {
                    viewModel.DesignRegions.Add(await DesignRegionViewModel.BuildAsync(designRegion));
                }
            }

            while(viewModel.DesignRegions.Count < numberOfDesignRegionRows)
            {
                viewModel.DesignRegions.Add(await DesignRegionViewModel.BuildAsync(null));
            }

            return viewModel;
        }

        public async Task SaveAsync(bool executeSaveChanges = false)
        {
            var db = SvgStudioDataContext.Current; 

            SvgStudio.Shared.StorageModel.Template template = null;
            if (IsNew())
            {
                template = new SvgStudio.Shared.StorageModel.Template();
                template.Id = UniqueId.Generate();
                this.Id = template.Id;
                db.Templates.Add(template);
            }
            else
            {
                template = await db.Templates.FirstAsync(x => x.Id == Id);
            }

            template.IsActive = this.IsActive;
            template.IsMaster = this.IsMaster;
            template.Name = this.Name;

            if (executeSaveChanges)
            {
                await db.SaveChangesAsync();
            }

            foreach(var designRegion in DesignRegions)
            {
                if (designRegion.IsEmpty())
                {
                    await designRegion.DeleteAsync(executeSaveChanges);
                }
                else
                {
                    await designRegion.SaveAsync(template, executeSaveChanges);
                }
            }
        }
    }

    public class TemplateViewModelValidator : AbstractValidator<TemplateViewModel>
    {
        public TemplateViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Template name cannot be empty.");
        }
    }
}