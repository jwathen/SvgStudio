using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.ViewModels.Licenses
{
    [Validator(typeof(LicenseViewModelValidator))]
    public class LicenseViewModel
    {
        public string Id { get; set; }
        public string LicenseName { get; set; }
        public string LicenseUrl { get; set; }
        public bool AttributionRequired { get; set; }
    }

    public class LicenseViewModelValidator : AbstractValidator<LicenseViewModel>
    {
        public LicenseViewModelValidator()
        {
            RuleFor(x => x.LicenseName).NotEmpty().WithMessage("License name is required.");
            RuleFor(x => x.LicenseUrl).NotEmpty().WithMessage("License URL is required.");
        }
    }
}