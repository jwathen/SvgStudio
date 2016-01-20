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

namespace SvgStudio.Web.ViewModels.Licenses
{
    [Validator(typeof(LicenseViewModelValidator))]
    public class LicenseViewModel
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string LicenseName { get; set; }
        public string LicenseUrl { get; set; }
        public bool AttributionRequired { get; set; }

        public bool IsNew()
        {
            return string.IsNullOrWhiteSpace(Id);
        }

        public bool CanBeDeleted()
        {
            var db = SvgStudioDataContext.Current;
            return !db.ContentLicenses.Any(x => x.LicenseId == this.Id);
        }

        public static async Task<LicenseViewModel> BuildAsync(License license)
        {
            LicenseViewModel viewModel = new LicenseViewModel();
            if (license != null)
            {
                viewModel.Id = license.Id;
                viewModel.LicenseName = license.LicenseName;
                viewModel.LicenseUrl = license.LicenseUrl;
                viewModel.AttributionRequired = license.AttributionRequired;
            }
            return viewModel;
        }

        public async Task SaveAsync(bool executeSaveChanges = false)
        {
            var db = SvgStudioDataContext.Current;
            License license = null;
            if (this.IsNew())
            {
                license = new License();
                license.Id = UniqueId.Generate();
                db.Licenses.Add(license);
            }
            else
            {
                license = await db.Licenses.FirstOrDefaultAsync(x => x.Id == this.Id);
            }

            license.LicenseName = this.LicenseName;
            license.LicenseUrl = this.LicenseUrl;
            license.AttributionRequired = this.AttributionRequired;

            if (executeSaveChanges)
            {
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(bool executeSaveChanges = false)
        {
            var db = SvgStudioDataContext.Current;
            var license = await db.Licenses.FirstOrDefaultAsync(x => x.Id == this.Id);
            if (license != null)
            {
                db.Licenses.Remove(license);
            }

            if (executeSaveChanges)
            {
                await db.SaveChangesAsync();
            }
        }
    }

    public class LicenseViewModelValidator : AbstractValidator<LicenseViewModel>
    {
        public LicenseViewModelValidator()
        {
            When(x => x.Action == "Save", () =>
             {
                 RuleFor(x => x.LicenseName).NotEmpty().WithMessage("License name is required.");
                 RuleFor(x => x.LicenseUrl).NotEmpty().WithMessage("License URL is required.");
             });
            When(x => x.Action == "Delete", () =>
            {
                RuleFor(x => x).Must(x => x.CanBeDeleted()).WithMessage("This license so it cannot be deleted because some shapes are using it.");
            });
        }
    }
}