using GeneratorLibrary.PlanetGeneration.Enums;

namespace GeneratorLibrary.PlanetGeneration
{
    public class ClimateModel
    {
        public int KelvinDegrees { get; set; } //Average surface temperature, used mainly in computations
        public float CelsiusDegrees { get; set; } //Average surface temperature
        public float FarenheitDegrees { get; set; } //Average surface temperature
        public ClimateType Type { get; set; }
        public float BlackbodyTemperature { get; set; }

        public ClimateModel(WorldTypeModel worldType, float atmosphereMass, float hydrographicCoverage, GenerationMethod genMethod = GenerationMethod.FullRandom)
        {
            KelvinDegrees = GenerateAverageSurfaceTemperature(worldType, genMethod);
            CelsiusDegrees = ConvertToCelsius(KelvinDegrees);
            FarenheitDegrees = ConvertToFarenheit(KelvinDegrees);
            Type = DetermineClimateType(KelvinDegrees);
            BlackbodyTemperature = GenerateBlackbodyTemperature(worldType, atmosphereMass, hydrographicCoverage);
        }

        public static int GenerateAverageSurfaceTemperature(WorldTypeModel worldType, GenerationMethod genMethod = GenerationMethod.FullRandom)
        {
            int minimum, maximum, stepValue;

            minimum = GetMinimumBasedOnWorldType(worldType);
            maximum = GetMaximumTemperatureBasedOnWorldType(worldType);

            if (genMethod == GenerationMethod.Dice)
            {
                stepValue = GetStepValueBasedOnWorldType(worldType);
                int generatedValue = DiceRoller.RollD6(3, -3) * stepValue;
                return Math.Clamp(generatedValue + minimum, minimum, maximum);
            }
            else
            {
                return Random.Shared.Next(minimum, maximum + 1);
            }
        }

        private static int GetMinimumBasedOnWorldType(WorldTypeModel worldType)
        {
            if (worldType.Type == PlanetType.AsteroidBelt)
                return 140;
            if (worldType.Type == PlanetType.Ice || worldType.Type == PlanetType.Sulfur)
                return 80;
            if (worldType.Type == PlanetType.Rock)
                return 140;
            if (worldType.Type == PlanetType.Hadean)
                return 50;
            if (worldType.Type == PlanetType.Ice)
                return 80;
            if (worldType.Type == PlanetType.Ammonia)
                return 140;
            if (worldType.Type == PlanetType.Ice)
                return 80;
            if (worldType.Type == PlanetType.Garden || worldType.Type == PlanetType.Ocean)
                return 250;
            if (worldType.Type == PlanetType.Greenhouse || worldType.Type == PlanetType.Chthonian)
                return 500;
            throw new Exception("Cannot get minimum temperature surface. World type doesn't match any combination.");
        }

        private static int GetStepValueBasedOnWorldType(WorldTypeModel worldType)
        {
            if (worldType.Type == PlanetType.AsteroidBelt)
                return 24;
            if (worldType.Size == PlanetSize.Tiny &&
                (worldType.Type == PlanetType.Ice || worldType.Type == PlanetType.Sulfur))
                return 4;
            if (worldType.Type == PlanetType.Rock)
                return 24;
            if (worldType.Type == PlanetType.Hadean)
                return 2;
            if (worldType.Size == PlanetSize.Small && worldType.Type == PlanetType.Ice)
                return 4;
            if (worldType.Type == PlanetType.Ammonia)
                return 5;
            if ((worldType.Size == PlanetSize.Standard || worldType.Size == PlanetSize.Large)
                && worldType.Type == PlanetType.Ice)
                return 10;
            if (worldType.Type == PlanetType.Garden || worldType.Type == PlanetType.Ocean)
                return 6;
            if (worldType.Type == PlanetType.Greenhouse || worldType.Type == PlanetType.Chthonian)
                return 30;
            throw new Exception("Cannot get minimum temperature surface. World type doesn't match any combination.");
        }

        private static int GetMaximumTemperatureBasedOnWorldType(WorldTypeModel worldType)
        {
            if (worldType.Type == PlanetType.AsteroidBelt)
                return 500;
            if (worldType.Size == PlanetSize.Tiny &&
                (worldType.Type == PlanetType.Ice || worldType.Type == PlanetType.Sulfur))
                return 140;
            if (worldType.Type == PlanetType.Rock)
                return 500;
            if (worldType.Type == PlanetType.Hadean)
                return 80;
            if (worldType.Size == PlanetSize.Small && worldType.Type == PlanetType.Ice)
                return 140;
            if (worldType.Type == PlanetType.Ammonia)
                return 215;
            if ((worldType.Size == PlanetSize.Standard || worldType.Size == PlanetSize.Large)
                && worldType.Type == PlanetType.Ice)
                return 230;
            if (worldType.Type == PlanetType.Garden || worldType.Type == PlanetType.Ocean)
                return 340;
            if (worldType.Type == PlanetType.Greenhouse || worldType.Type == PlanetType.Chthonian)
                return 950;
            throw new Exception("Cannot get minimum temperature surface. World type doesn't match any combination.");
        }

        public static float ConvertToCelsius(int kelvinDegrees)
        {
            float output = kelvinDegrees - 273.15f;
            return MathF.Round(output, 2);
        }

        public static float ConvertToFarenheit(int kelvinDegrees)
        {
            float output = 1.8f * (kelvinDegrees - 273.15f) + 32;
            return MathF.Round(output, 2);
        }

        public static ClimateType DetermineClimateType(int KelvinDegrees) => KelvinDegrees switch
        {
            < 244 => ClimateType.Frozen,
            >= 244 and < 255 => ClimateType.VeryCold,
            >= 255 and < 266 => ClimateType.Cold,
            >= 266 and < 278 => ClimateType.Chilly,
            >= 278 and < 289 => ClimateType.Cool,
            >= 289 and < 300 => ClimateType.Normal,
            >= 300 and < 311 => ClimateType.Warm,
            >= 311 and < 322 => ClimateType.Tropical,
            >= 322 and < 333 => ClimateType.Hot,
            >= 333 and <= 344 => ClimateType.VeryHot,
            > 344 => ClimateType.Infernal
        };

        public float GenerateBlackbodyTemperature(WorldTypeModel worldType, float atmosphereMass, float hydrographicCoverage)
        {
            float blackbodyCorrection, absortionFactor, greenhouseFactor;

            absortionFactor = GetAbsortionFactorBasedOnWorldType(worldType, hydrographicCoverage);
            greenhouseFactor = GetGreenhouseFactorBasedOnWorldType(worldType);

            blackbodyCorrection = absortionFactor * (1 + (atmosphereMass * greenhouseFactor));

            return KelvinDegrees / blackbodyCorrection;
        }

        public float GetAbsortionFactorBasedOnWorldType(WorldTypeModel worldType, float hydrographicCoverage)
        {
            if (worldType.Type == PlanetType.AsteroidBelt)
                return 0.97f;

            if (worldType.Size == PlanetSize.Tiny)
                return GetAbsortionFactorForTinyWorld(worldType.Type);

            if (worldType.Size == PlanetSize.Small)
                return GetAbsortionFactorForSmallWorld(worldType.Type);

            if (worldType.Type == PlanetType.Ocean || worldType.Type == PlanetType.Garden)
                return GetAbsortionFactorForOceanAndGardenWorld(hydrographicCoverage);

            if (worldType.Size == PlanetSize.Standard || worldType.Size == PlanetSize.Large)
                return GetAbsortionFactorForStandardOrLargeWorld(worldType.Type);


            throw new Exception("Cannot get absortion factor based on world type.");
        }
        
        public float GetAbsortionFactorForTinyWorld(PlanetType type) => type switch
        {
            PlanetType.Ice => 0.86f,
            PlanetType.Rock => 0.97f,
            PlanetType.Sulfur => 0.77f,
            _ => throw new ArgumentException()
        };
        
        public float GetAbsortionFactorForSmallWorld(PlanetType type) => type switch
        {
            PlanetType.Hadean => 0.67f,
            PlanetType.Ice => 0.93f,
            PlanetType.Rock => 0.96f,
            _ => throw new ArgumentException()
        };
        
        public float GetAbsortionFactorForOceanAndGardenWorld(float hydrographicCoverage) => hydrographicCoverage switch
        {
            < 0.21f => 0.95f,
            > 0.21f and < 0.51f => 0.92f,
            > 0.51f and < 0.91f => 0.88f,
            >= 0.91f => 0.84f,
            _ => throw new ArgumentException()
        };
        
        public float GetAbsortionFactorForStandardOrLargeWorld(PlanetType type) => type switch
        {
            PlanetType.Hadean => 0.67f,
            PlanetType.Ammonia => 0.84f,
            PlanetType.Ice => 0.86f,
            PlanetType.Greenhouse => 0.77f,
            PlanetType.Chthonian => 0.97f,
            _ => throw new ArgumentException()
        };
        
        public float GetGreenhouseFactorBasedOnWorldType(WorldTypeModel worldType) => (worldType.Size, worldType.Type) switch
        {
            (PlanetSize.Small, PlanetType.Ice) => 0.1f,
            (PlanetSize.Standard, PlanetType.Ammonia) or (PlanetSize.Large, PlanetType.Ammonia) or
            (PlanetSize.Standard, PlanetType.Ice) or (PlanetSize.Large, PlanetType.Ice) => 0.20f,
            (PlanetSize.Standard, PlanetType.Ocean) or (PlanetSize.Large, PlanetType.Ocean) or
            (PlanetSize.Standard, PlanetType.Garden) or (PlanetSize.Large, PlanetType.Garden) => 0.16f,
            (PlanetSize.Standard, PlanetType.Greenhouse) or (PlanetSize.Large, PlanetType.Greenhouse) => 2f,
            _ => 0f
        };
    }
}
