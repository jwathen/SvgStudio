using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class MarkupFragment
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string Content { get; set; }
    }
}