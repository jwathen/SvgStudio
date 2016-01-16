using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class MarkupFragment : ISyncableEntity<MarkupFragmentDto>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string Content { get; set; }

        public MarkupFragmentDto ToDto()
        {
            return new MarkupFragmentDto
            {
                Id = this.Id,
                RowVersion = HexHelper.ByteArrayToHexString(this.RowVersion),
                Content = this.Content
            };
        }
    }
}