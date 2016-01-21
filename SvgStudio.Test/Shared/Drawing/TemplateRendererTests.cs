using SvgStudio.Shared.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SvgStudio.Test.Shared.Drawing
{
    public class TemplateRendererTests
    {
        //public void Render()
        //{
        //    Template coatOfArms = new Template();
        //    coatOfArms.Name = "Coat of Arms";

        //    var shield = new DesignRegion("Shield", 160, 350, 260, 340);
        //    var leftSupporter = new DesignRegion("LeftSupporter", 24, 327, 188, 365);
        //    coatOfArms.DesignRegions.Add(shield);
        //    coatOfArms.DesignRegions.Add(leftSupporter);

        //    string lionPath = File.ReadAllText(@"TestHelpers\Fixtures\LionPath.txt");
        //    Shape lionShape = new BasicShape(378, 334, 1, 1, lionPath);

        //    var shieldTemplate = new Template();
        //    shieldTemplate.Name = "ShieldWithThreeBars";
        //    shieldTemplate.DesignRegions.Add(new DesignRegion("TopBar", 0, 0, 825, 330));
        //    shieldTemplate.DesignRegions.Add(new DesignRegion("MiddleBar", 0, 330, 825, 330));
        //    shieldTemplate.DesignRegions.Add(new DesignRegion("BottomBar", 0, 660, 825, 330));

        //    var rectangle = new BasicShape(1, 1, 1, 0, File.ReadAllText(@"TestHelpers\Fixtures\RectanglePath.txt"));

        //    var redPalette = new Palette();
        //    redPalette.Fills.Add(new SolidColorFill(Color.FromName("red")));

        //    var greenPalette = new Palette();
        //    greenPalette.Fills.Add(new SolidColorFill(Color.FromName("green")));

        //    var polkaDotsPattern = new PatternFill();
        //    var polkaDotsShape = new BasicShape(15, 15, 0, 0, File.ReadAllText(@"TestHelpers\Fixtures\PolkaDotsPattern.txt"));
        //    polkaDotsPattern.Design = new Design
        //    {
        //        Shape = polkaDotsShape
        //    };
        //    polkaDotsPattern.Width = 150;
        //    polkaDotsPattern.Height = 150;
        //    polkaDotsPattern.PatternUnits = "userSpaceOnUse";
        //    polkaDotsPattern.Name = "BlackAndWhitePolkaDots";
        //    var polkaDotsPalette = new Palette();
        //    polkaDotsPalette.Strokes.Add(new Stroke { Color = Color.FromName("black"), Width = 3 });
        //    polkaDotsPalette.Fills.Add(polkaDotsPattern);

        //    string shieldPath = File.ReadAllText(@"TestHelpers\Fixtures\ShieldPath.txt");
        //    TemplateShape shieldShape = new TemplateShape(shieldTemplate);
        //    shieldShape.Width = 825;
        //    shieldShape.Height = 990;
        //    shieldShape.NumberOfFillsSupported = 0;
        //    shieldShape.NumberOfStrokesSupported = 0;
        //    shieldShape.ClipPathMarkup = File.ReadAllText(@"TestHelpers\Fixtures\ShieldClipPath.txt");
        //    shieldShape.AddDesign("TopBar", rectangle, redPalette);
        //    shieldShape.AddDesign("MiddleBar", rectangle, polkaDotsPalette);
        //    shieldShape.AddDesign("BottomBar", rectangle, greenPalette);

        //    var grayAndBlue = new Palette();
        //    grayAndBlue.Strokes.Add(new Stroke { Color = Color.FromName("Gray"), Width = 3 });
        //    grayAndBlue.Fills.Add(new SolidColorFill(Color.FromName("Blue")));

        //    var renderer = new TemplateRenderer(coatOfArms);
        //    renderer.AddDesign("LeftSupporter", lionShape, grayAndBlue);
        //    renderer.AddDesign("Shield", shieldShape, polkaDotsPalette);
        //    XElement svg = renderer.Render();
        //    string result = svg.ToString();
        //    result = result.Replace("<svg ", "<svg xmlns=\"http://www.w3.org/2000/svg\" ");
        //    File.WriteAllText("output.svg", result);
        //}
    }
}
