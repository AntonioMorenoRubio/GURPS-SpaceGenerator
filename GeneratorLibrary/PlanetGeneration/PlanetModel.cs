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
        public WorldTypeModel WorldType { get; set; }

        public PlanetModel() { }

        public PlanetModel(string name, string description = "")
        {
            Name = name;
            Description = description;
            WorldType = new WorldTypeModel();
        }

        public PlanetModel(string name, PlanetType type, PlanetSize size, string description = "")
        {
            Name = name;
            Description = description;
            WorldType = new WorldTypeModel(type, size);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            //Step 1
            sb.AppendLine("Name:" + Name);
            sb.AppendLine("Description:" + Description);

            //Step 2
            sb.AppendLine($"World Type: {WorldType.Type}");
            sb.AppendLine($"World Abstract Size: {WorldType.Size}");

            return sb.ToString();
        }
    }
}
