using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Mobile.Core.Models.Synchronization
{
    public class EntityId
    {
        public EntitySource Source { get; set; }

        public string SourceId { get; set; }

        public static EntityId FromServerId(int? serverId)
        {
            return new EntityId
            {
                Source = EntitySource.Server,
                SourceId = serverId?.ToString()
            };
        }

        public static EntityId NewLocalId()
        {
            string random = Guid.NewGuid().ToString("n").Substring(0, 8).ToLower();
            return new EntityId
            {
                Source = EntitySource.Local,
                SourceId = random
            };
        }

        public static EntityId Parse(string id)
        {            
            int source = int.Parse(id.Substring(0, 1));
            string sourceId = id.Substring(2, id.Length - 2);
            return new EntityId
            {
                Source = (EntitySource)source,
                SourceId = sourceId
            };
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(SourceId))
            {
                return null;
            }
            return string.Format("{0}-{1}", (int)Source, SourceId);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntityId))
            {
                return false;
            }
            else
            {
                return obj.GetHashCode() == this.GetHashCode();
            }
        }
    }
}
