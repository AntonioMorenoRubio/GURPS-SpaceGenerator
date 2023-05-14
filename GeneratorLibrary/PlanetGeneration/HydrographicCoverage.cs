using GeneratorLibrary.PlanetGeneration.Enums;

namespace GeneratorLibrary.PlanetGeneration
{
    public class HydrographicCoverageModel
    {
        public bool HasLiquidWater { get; set; } = false;
        public float WaterCoveragePercent { get; set; }
        public HydrographicCoverageModel(WorldTypeModel worldType)
        {
            (bool HasLiquidWater, float WaterCoverage) generated = GenerateHydrographicCoverage(worldType);
            (float Minimum, float Maximum) minimumMaximum = GetMinMaxCoveragePerWorldType(worldType);

            HasLiquidWater = generated.Item1;

            if (HasLiquidWater)
            {
                float variation = Random.Shared.NextSingle(-0.05f, 0.05f);
                WaterCoveragePercent = Math.Clamp(generated.WaterCoverage + variation, minimumMaximum.Minimum, minimumMaximum.Maximum);
            }
            else
                WaterCoveragePercent = Math.Clamp(generated.WaterCoverage, minimumMaximum.Minimum, minimumMaximum.Maximum);
        }

        private (bool HasLiquidWater, float WaterCoverage) GenerateHydrographicCoverage(WorldTypeModel worldType) => (worldType.Size, worldType.Type) switch
        {
            (PlanetSize.Small, PlanetType.Ice)
            => (false, DiceRoller.RollD6(1, 2) * 0.1f),

            (PlanetSize.Standard, PlanetType.Ammonia) or (PlanetSize.Large, PlanetType.Ammonia)
            => (false, Math.Clamp(DiceRoller.RollD6(2) * 0.1f, 0f, 1f)),

            (PlanetSize.Standard, PlanetType.Ice) or (PlanetSize.Large, PlanetType.Ice)
            => (true, Math.Clamp(DiceRoller.RollD6(2, -10) * 0.1f, 0f, 0.2f)),

            (PlanetSize.Standard, PlanetType.Garden) or (PlanetSize.Standard, PlanetType.Ocean)
            => (true, Math.Clamp(DiceRoller.RollD6(1, 4) * 0.1f, 0f, 1f)),

            (PlanetSize.Large, PlanetType.Garden) or (PlanetSize.Large, PlanetType.Ocean)
            => (true, Math.Clamp(DiceRoller.RollD6(1, 6) * 0.1f, 0f, 1f)),

            (PlanetSize.Standard, PlanetType.Greenhouse) or (PlanetSize.Standard, PlanetType.Greenhouse)
            => (true, Math.Clamp(DiceRoller.RollD6(2, -7) * 0.1f, 0f, 0.5f)),

            _ => (false, 0f)
        };

        private (float Minimum, float Maximum) GetMinMaxCoveragePerWorldType(WorldTypeModel worldType) => (worldType.Size, worldType.Type) switch
        {
            (PlanetSize.Small, PlanetType.Ice) => (0.3f, 0.8f),
            (PlanetSize.Standard, PlanetType.Ammonia) or (PlanetSize.Large, PlanetType.Ammonia) => (0.5f, 1f),
            (PlanetSize.Standard, PlanetType.Ice) or (PlanetSize.Large, PlanetType.Ice) => (0f, 0.2f),
            (PlanetSize.Standard, PlanetType.Garden) or (PlanetSize.Standard, PlanetType.Ocean) 
            or (PlanetSize.Large, PlanetType.Garden) or (PlanetSize.Large, PlanetType.Ocean) => (0.5f, 1f),
            (PlanetSize.Standard, PlanetType.Greenhouse) or (PlanetSize.Standard, PlanetType.Greenhouse) => (0f, 0.5f),
            _ => (0f, 0f)
        };
    }
}
