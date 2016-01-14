﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public abstract class Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int NumberOfFillsSupported { get; set; } = 1;

        public int NumberOfStrokesSupported { get; set; } = 1;

        public abstract RenderDesignResult Render(Palette palette);
    }
}