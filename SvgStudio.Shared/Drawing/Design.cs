﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SvgStudio.Shared.Drawing
{
    public class Design
    {
        public string StorageId { get; set; }
        public Shape Shape { get; set; }
        public Palette Palette { get; set; }

        public RenderDesignResult Render(string namingContext)
        {
            return Shape.Render(Palette, namingContext);
        }
    }
}
