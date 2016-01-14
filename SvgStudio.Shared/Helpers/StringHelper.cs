using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SvgStudio.Shared.Helpers
{
    public static class StringHelper
    {
        public static string StripNonAlphaNumericChars(string input)
        {
            if (input == null)
            {
                return null;
            }

            StringBuilder result = new StringBuilder();
            foreach(char character in input)
            {
                if (char.IsLetterOrDigit(character))
                {
                    result.Append(character);
                }
                else
                {
                    result.Append('_');
                }
            }
            return result.ToString();
        }
    }
}
