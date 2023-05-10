using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLibrary
{
    public static class RandomHelper
    {
        public static float NextSingle(this Random random, float minimum, float maximum)
        {
            return new Random().NextSingle() * (maximum - minimum) + minimum;
        }
    }
}
