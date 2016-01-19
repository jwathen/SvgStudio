﻿using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class Shape_CompatibilityTag : ISyncableRecord
    {

        [PrimaryKey]
        public string Id
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(CompatibilityTagId) && !string.IsNullOrWhiteSpace(ShapeId))
                {
                    return string.Format("{0}-{1}", CompatibilityTagId, ShapeId);
                }
                else
                {
                    return null;
                }
            }
            set
            {
               // Do nothing;
            }
        }

        [Indexed(Name = "IX_Shape_CompatibilityTag_X", Order = 1)]
        public string CompatibilityTagId { get; set; }
        [Indexed(Name = "IX_Shape_CompatibilityTag_X", Order = 2)]
        public string ShapeId { get; set; }

        public byte[] RowVersion
        {
            get
            {
                string id = this.Id;
                if (id == null)
                {
                    return new byte[0];
                }
                else
                {
                    return Encoding.UTF8.GetBytes(id);
                }
            }
        }
    }
}
