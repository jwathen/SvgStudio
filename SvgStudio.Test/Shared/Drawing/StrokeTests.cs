using SvgStudio.Shared.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Should;

namespace SvgStudio.Test.Shared.Drawing
{
    public class StrokeTests
    {
        public void StrokeWidthIsOnlyAppliedIfItDoesntAlreadyExist()
        {
            Stroke stroke = new Stroke();
            stroke.Width = 3;

            var rect = XElement.Parse("<rect />");
            stroke.ApplyTo(rect);
            rect.Attribute("stroke-width").Value.ShouldEqual("3");

            rect = XElement.Parse("<rect stroke-width=\"10\" />");
            stroke.ApplyTo(rect);
            rect.Attribute("stroke-width").Value.ShouldEqual("10");
        }
    }
}
