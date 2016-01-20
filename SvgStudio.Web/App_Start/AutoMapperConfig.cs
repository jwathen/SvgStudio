using SvgStudio.Shared.StorageModel;
using SvgStudio.Web.ViewModels.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            AutoMapper.Mapper.CreateMap<License, LicenseViewModel>()
                .ForSourceMember(x => x.RowVersion, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.RowVersion, opt => opt.Ignore());

#if DEBUG
            AutoMapper.Mapper.AssertConfigurationIsValid();
#endif
        }
    }
}