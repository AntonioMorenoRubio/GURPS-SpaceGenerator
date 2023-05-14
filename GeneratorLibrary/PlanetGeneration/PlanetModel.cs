using System.Text;
using GeneratorLibrary.PlanetGeneration.Enums;

namespace GeneratorLibrary.PlanetGeneration
{
    public class PlanetModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public WorldTypeModel WorldType { get; set; }
        public AtmosphereModel? Atmosphere { get; set; }
        public HydrographicCoverageModel HydrographicCoverage { get; set; }

        public PlanetModel() { }

        public PlanetModel(string name, string description = "")
        {
            Name = name;
            Description = description;
            WorldType = new WorldTypeModel();

            if (AtmosphereModel.CanHaveAtmosphere(WorldType))
                Atmosphere = new AtmosphereModel(WorldType);

            HydrographicCoverage = new HydrographicCoverageModel(WorldType);
        }

        public PlanetModel(string name, PlanetType type, PlanetSize size, string description = "")
        {
            Name = name;
            Description = description;
            WorldType = new WorldTypeModel(size, type);

            if (AtmosphereModel.CanHaveAtmosphere(WorldType))
                Atmosphere = new AtmosphereModel(WorldType);

            HydrographicCoverage = new HydrographicCoverageModel(WorldType);
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

            //Step 3
            if (Atmosphere != null)
            {
                sb.AppendLine($"Atmosphere:");
                sb.AppendLine($"Atmospheric Pressure: {Atmosphere.Pressure}");
                sb.AppendLine($"Pressure Category: {Atmosphere.PressureCategory}");
                sb.AppendLine($"Pressure felt by Life: {Atmosphere.PressureClassFeltByLife}");
                sb.AppendLine($"Mass: {Atmosphere.Mass}");

                if (Atmosphere.MarginalAtmosphere != null)
                    sb.AppendLine(Atmosphere.MarginalAtmosphere.ToString());

                sb.Append("Composition: ");
                foreach (string compound in Atmosphere.Composition)
                {
                    sb.Append($"{compound}, ");
                }
                sb.AppendLine();

                sb.Append("Special Charateristics for Humans: ");
                if (Atmosphere.Characteristics.Count > 0)
                {
                    foreach (AtmosphereCharacteristic characteristic in Atmosphere.Characteristics)
                    {
                        sb.Append($"{characteristic.ToString()}, ");
                    }
                } else
                {
                    sb.Append("None");
                }
                sb.AppendLine();
            }

            //Step 4
            sb.AppendLine($"Hydrographic Coverage: {HydrographicCoverage.WaterCoveragePercent*100f}%");
            sb.AppendLine($"Has liquid water on surface: {HydrographicCoverage.HasLiquidWater}");

            return sb.ToString();
        }
    }
}
