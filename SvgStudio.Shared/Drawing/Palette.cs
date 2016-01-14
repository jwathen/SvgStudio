using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SvgStudio.Shared.Drawing
{
    public class Palette
    {
        public List<Stroke> Strokes { get; private set; } = new List<Stroke>();
        public List<Fill> Fills { get; private set; } = new List<Fill>();

        public Stroke GetStroke(int index)
        {
            return GetItemAtIndex(Strokes, index, new Stroke());
        }

        public Fill GetFill(int index)
        {
            return GetItemAtIndex(Fills, index, SolidColorFill.Transparent);
        }

        private T GetItemAtIndex<T>(List<T> list, int index, T defaultT)
        {
            if (list.Count > (index + 1))
            {
                return list[index];
            }
            else if (list.Any())
            {
                return list.Last();
            }
            else
            {
                return defaultT;
            }
        }
    }
}
