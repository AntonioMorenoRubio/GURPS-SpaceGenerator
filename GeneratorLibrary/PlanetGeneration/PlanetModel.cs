using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLibrary
{
    public class PlanetModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public PlanetModel() { }

        public PlanetModel(string name, string description = "")
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Name:" + Name);
            sb.AppendLine("Description:" + Description);

            return sb.ToString();
        }
    }
}
