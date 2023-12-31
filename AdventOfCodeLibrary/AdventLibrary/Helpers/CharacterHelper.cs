using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventLibrary.Helpers
{
    public static class CharacterHelper
    {
        public static bool InAsciiRange(this char chr, char start, char end)
        {
            return start <= chr && chr <= end;
        }
        public static bool InAsciiRange(this char chr, int start, int end)
        {
            return start <= chr && chr <= end;
        }
    }
}
