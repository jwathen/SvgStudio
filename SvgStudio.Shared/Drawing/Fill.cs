﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public abstract class Fill : IDefProvider
    {
        public abstract void ApplyTo(XElement target);
        public abstract DefinitionCollection GetDefs();
    }
}
