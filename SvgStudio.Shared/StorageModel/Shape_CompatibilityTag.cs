﻿using Newtonsoft.Json;
using SQLite.Net.Attributes;
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
        public virtual string Id { get; set; }

        [Indexed(Name = "IX_Shape_CompatibilityTag_X", Order = 1)]
        public virtual string CompatibilityTagId { get; set; }
        [Indexed(Name = "IX_Shape_CompatibilityTag_X", Order = 2)]
        public virtual string ShapeId { get; set; }

        public virtual byte[] RowVersion
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

        [Ignore, JsonIgnore]
        public virtual CompatibilityTag CompatibilityTag { get; set; }
        [Ignore, JsonIgnore]
        public virtual Shape Shape { get; set; }

        public void ComputeId()
        {
            string compatibilityTagId = CompatibilityTagId;
            if (compatibilityTagId == null && CompatibilityTag != null)
            {
                compatibilityTagId = CompatibilityTag.Id;
            }

            string shapeId = ShapeId;
            if (shapeId == null && Shape != null)
            {
                shapeId = Shape.Id;
            }

            string id = null;
            if (!string.IsNullOrWhiteSpace(compatibilityTagId) && !string.IsNullOrWhiteSpace(shapeId))
            {
                id = string.Format("{0}|{1}", compatibilityTagId, shapeId);
            }

            Id = id;
        }
    }
}
