﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class BasicShape : Shape
    {
        public int MarkupFragmentId { get; set; }

        public MarkupFragment MarkupFragment { get; set; }
    }
}