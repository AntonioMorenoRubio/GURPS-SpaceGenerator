using System.Text;
using GeneratorLibrary.PlanetGeneration.Enums;

namespace GeneratorLibrary.PlanetGeneration
{
    public class PlanetModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public WorldTypeModel WorldType { get; set; }
        public AtmosphereModel Atmosphere { get; set; }
        public HydrographicCoverageModel HydrographicCoverage { get; set; }
        public ClimateModel Climate { get; set; }
        public WorldSizeModel WorldSize { get; set; }
        public ResourceModel Resource { get; set; }
        public HabitabilityModel Habitability { get; set; }
        public int AffinityScore { get; set; }


        public PlanetModel(string name, string description = "")
        {
            Name = name;
            Description = description;
            WorldType = new WorldTypeModel();

            Atmosphere = new AtmosphereModel(WorldType);

            HydrographicCoverage = new HydrographicCoverageModel(WorldType);

            Climate = new ClimateModel(WorldType, Atmosphere.Mass, HydrographicCoverage.WaterCoveragePercent);

            if (WorldType.Type is not PlanetType.AsteroidBelt)
            {
                WorldSize = new WorldSizeModel(WorldType, Climate.BlackbodyTemperature);
                Atmosphere.DetermineAtmosphericPressure(WorldType, WorldSize.SurfaceGravity);
            }

            Resource = new ResourceModel(WorldType.Type);
            Habitability = new HabitabilityModel(Atmosphere, HydrographicCoverage, Climate);
            AffinityScore = Resource.ResourceValueModifier + Habitability.Score;
        }

        public PlanetModel(string name, PlanetType type, PlanetSize size, string description = "")
        {
            Name = name;
            Description = description;
            WorldType = new WorldTypeModel(size, type);

            Atmosphere = new AtmosphereModel(WorldType);

            HydrographicCoverage = new HydrographicCoverageModel(WorldType);

            Climate = new ClimateModel(WorldType, Atmosphere.Mass, HydrographicCoverage.WaterCoveragePercent);

            if (WorldType.Type is not PlanetType.AsteroidBelt)
                WorldSize = new WorldSizeModel(WorldType, Climate.BlackbodyTemperature);

            Resource = new ResourceModel(WorldType.Type);
            Habitability = new HabitabilityModel(Atmosphere, HydrographicCoverage, Climate);
            AffinityScore = Resource.ResourceValueModifier + Habitability.Score;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            //Step 1
            sb.AppendLine("Name:" + Name);
            sb.AppendLine("Description:" + Description);
            sb.AppendLine();

            //Step 2
            sb.AppendLine($"World Type: {WorldType.Type}");
            sb.AppendLine($"World Abstract Size: {WorldType.Size}");
            sb.AppendLine();

            //Step 3
            sb.AppendLine($"Atmosphere:");
            sb.AppendLine($"Atmospheric Pressure: {Atmosphere.Pressure} atm.");
            sb.AppendLine($"Pressure Category: {Atmosphere.PressureCategory}");
            sb.AppendLine($"Pressure felt by Humans: {Atmosphere.PressureClassFeltByHumans}");
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
            }
            else
            {
                sb.Append("None");
            }
            sb.AppendLine();

            //Step 4
            sb.AppendLine($"Hydrographic Coverage: {HydrographicCoverage.WaterCoveragePercent * 100f}%");
            sb.AppendLine($"Has liquid water on surface: {HydrographicCoverage.HasLiquidWater}");
            sb.AppendLine();

            //Step 5
            sb.AppendLine("Climate Data:");
            sb.AppendLine($"Climate Type: {Climate.Type}");
            sb.AppendLine($"Average surface temperature: {Climate.CelsiusDegrees}ºC / {Climate.FarenheitDegrees}ºF / {Climate.KelvinDegrees}ºK");
            sb.AppendLine($"Blackbody Temperature: {Climate.BlackbodyTemperature}ºK");
            sb.AppendLine();

            //Step 6
            if (WorldType.Type is not PlanetType.AsteroidBelt)
            {
                sb.AppendLine("World Size Data:");
                sb.AppendLine($"Density: {WorldSize.Density} Earth Densities / {WorldSize.DensityToGCC()} g/cc.");
                sb.AppendLine($"Diameter: {WorldSize.Diameter} Earth Diameters / {WorldSize.DiameterToMiles()} miles.");
                sb.AppendLine($"Surface Gravity: {WorldSize.SurfaceGravity} Gs");
                sb.AppendLine($"Mass: {WorldSize.Mass} Earth Masses.");
                sb.AppendLine();
            }

            //Step 7
            sb.AppendLine($"Resource Value: {Resource.Description} ({string.Concat("+", Resource.ResourceValueModifier.ToString())})");
            sb.AppendLine($"Habitability Score: {Habitability.Score}");
            sb.AppendLine($"Affinity Score: {AffinityScore}");
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
