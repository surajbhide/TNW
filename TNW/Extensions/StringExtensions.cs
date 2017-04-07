using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNW.Extensions
{
    public static class StringExtensions
    {
        public static string UpcaseFirstLetter(this string input)
        {
            char[] temp = input.ToCharArray();
            temp[0] = Char.ToUpper(temp[0]);
            return new string(temp);
        }
    }
}
