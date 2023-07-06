using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T11
{
    public static class StrExt
    {
        // Count how many cell in string
        public static int Count(this string str)
        {
            return str.ToArrayString().Length;
        }

        // Gen a array of cell
        public static string[] ToArrayString(this string str)
        {
            return str.Split(',');
        }
    }
}
