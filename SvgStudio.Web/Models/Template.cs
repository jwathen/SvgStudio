﻿using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class Template : ISyncableEntity<TemplateDto>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }

        public ICollection<DesignRegion> DesignRegions { get; set; }

        public SyncableEntityId SyncableEntityId
        {
            get
            {
                return new SyncableEntityId
                {
                    Id = this.Id,
                    RowVersion = HexHelper.ByteArrayToHexString(this.RowVersion)
                };
            }
        }

        public TemplateDto ToDto()
        {
            return new TemplateDto
            {
                Id = this.Id,
                RowVersion = HexHelper.ByteArrayToHexString(this.RowVersion),
                Name = this.Name
            };
        }
    }
}