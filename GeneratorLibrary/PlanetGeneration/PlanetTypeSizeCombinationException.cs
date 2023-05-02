using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLibrary
{
    [Serializable]
    public class PlanetTypeSizeCombinationException : Exception
    {
        public PlanetTypeSizeCombinationException() : base() { }
        public PlanetTypeSizeCombinationException(string message) : base(message) { }
        public PlanetTypeSizeCombinationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
