﻿using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class Palette : ISyncableRecord
    {
        [PrimaryKey]
        public string Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public short SortOrder { get; set; }

        [Ignore, JsonIgnore]
        public ICollection<Design> Designs { get; set; }
        [Ignore, JsonIgnore]
        public ICollection<Fill> Fills { get; set; }
        [Ignore, JsonIgnore]
        public ICollection<Stroke> Strokes { get; set; }
    }
}
