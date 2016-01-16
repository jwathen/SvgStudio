using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SvgStudio.Mobile.Core.Models
{
    public class Design
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string RowVersion { get; set; }
        public string ShapeId { get; set; }
        public string PaletteId { get; set; }
    }
}