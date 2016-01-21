using FluentValidation;
using SvgStudio.Shared.StorageModel;
using SvgStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Data.Entity;

namespace SvgStudio.Web.ViewModels.Shared
{
    public class MarkupFragmentViewModel
    {
        public string Id { get; set; }
        [AllowHtml]
        public string Content { get; set; }        

        public static async Task<MarkupFragmentViewModel> BuildAsync(string id)
        {
            var viewModel = new MarkupFragmentViewModel();

            var db = SvgStudioDataContext.Current;
            var markupFragment = await db.MarkupFragments.FirstOrDefaultAsync(x => x.Id == id);
            if (markupFragment != null)
            {
                viewModel.Id = markupFragment.Id;
                viewModel.Content = markupFragment.Content;
            }

            return viewModel;
        }

        public async Task SaveAsync(bool executeSaveChanges = false)
        {
            var db = SvgStudioDataContext.Current;

            MarkupFragment markupFragment = null;
            if (!string.IsNullOrWhiteSpace(this.Id))
            {
                markupFragment = await db.MarkupFragments.FirstOrDefaultAsync(x => x.Id == this.Id);
            }
            if (markupFragment == null)
            {
                markupFragment = new MarkupFragment();
                markupFragment.Id = UniqueId.Generate();
                this.Id = markupFragment.Id;
                db.MarkupFragments.Add(markupFragment);
            }

            markupFragment.Content = this.Content;

            if (executeSaveChanges)
            {
                await db.SaveChangesAsync();
            }
        }
    }

    public class MarkupFragmentViewModelValidator : AbstractValidator<MarkupFragmentViewModel>
    {
        public MarkupFragmentViewModelValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("XML content is required.");
            RuleFor(x => x.Content).Must(BeValidXml).WithMessage("XML content is invalid.");
        }

        public bool BeValidXml(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return true;
            }

            try
            {
                XElement.Parse(content);
                return true;
            }
            catch { }

            try
            {
                XElement.Parse("<g>" + content + "</g>");
                return true;
            }
            catch { }

            return false;
        }
    }
}