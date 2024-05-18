﻿using System.Text;
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
        public ClimateModel? Climate { get; set; }
        public ResourceModel Resource { get; set; }
        public HabitabilityModel Habitability { get; set; }
        public int AffinityScore { get; set; }


        public PlanetModel() { }

        public PlanetModel(string name, string description = "")
        {
            Name = name;
            Description = description;
            WorldType = new WorldTypeModel();

            if (AtmosphereModel.CanHaveAtmosphere(WorldType))
                Atmosphere = new AtmosphereModel(WorldType);

            HydrographicCoverage = new HydrographicCoverageModel(WorldType);

            if (Atmosphere != null)
                Climate = new ClimateModel(WorldType, Atmosphere.Mass, HydrographicCoverage.WaterCoveragePercent);

            Resource = new ResourceModel(WorldType.Type);
            Habitability = new HabitabilityModel(Atmosphere, HydrographicCoverage, Climate);
            AffinityScore = Resource.ResourceValueModifier + Habitability.Score;
        }

        public PlanetModel(string name, PlanetType type, PlanetSize size, string description = "")
        {
            Name = name;
            Description = description;
            WorldType = new WorldTypeModel(size, type);

            if (AtmosphereModel.CanHaveAtmosphere(WorldType))
                Atmosphere = new AtmosphereModel(WorldType);

            HydrographicCoverage = new HydrographicCoverageModel(WorldType);

            if (Atmosphere != null)
                Climate = new ClimateModel(WorldType, Atmosphere.Mass, HydrographicCoverage.WaterCoveragePercent);

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
            sb.AppendLine();

            //Step 5
            if (Climate != null)
            {
                sb.AppendLine("Climate Data:");
                sb.AppendLine($"Climate Type: {Climate.Type}");
                sb.AppendLine($"Average surface temperature: {Climate.CelsiusDegrees}ºC / {Climate.FarenheitDegrees}ºF / {Climate.KelvinDegrees}ºK");
                sb.AppendLine($"Blackbody Temperature: {Climate.BlackbodyTemperature}ºK");
                sb.AppendLine();
            }

            //Step 6


            //Step 7
            if (Resource != null)
            {
                sb.AppendLine($"Resource Value: {Resource.Description} ({string.Concat("+", Resource.ResourceValueModifier.ToString())}");
            }
            if (Habitability != null)
            {
                sb.AppendLine($"Habitability Score: {Habitability.Score}");
            }
            sb.AppendLine($"Affinity Score: {AffinityScore}");

            return sb.ToString();
        }
    }
}
