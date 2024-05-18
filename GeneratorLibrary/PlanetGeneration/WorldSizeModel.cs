

using GeneratorLibrary.PlanetGeneration.Enums;

namespace GeneratorLibrary.PlanetGeneration
{
    public class WorldSizeModel
    {
        public float Density { get; set; }
        public float Diameter { get; set; }
        public float SurfaceGravity { get; set; }
        public float Mass { get; set; }

        public WorldSizeModel(WorldTypeModel world, float blackbodyTemperature)
        {
            if (world.Size is not PlanetSize.Special)
            {
                Density = GenerateRandomDensity(world);
                Diameter = GenerateRandomDiameter(world.Size, blackbodyTemperature);
                SurfaceGravity = Density * Diameter;
                Mass = Density * MathF.Pow(Diameter, 3);
            }
        }

        private float GenerateRandomDiameter(PlanetSize size, float blackbodyTemperature)
        {
            (float minimumSize, float maximumSize) constraints = GetSizeContraints(size);

            float minimumDiameter = MathF.Sqrt(blackbodyTemperature / Density) * constraints.minimumSize;
            float maximumDiameter = MathF.Sqrt(blackbodyTemperature / Density) * constraints.maximumSize;


            float baseDiameter = (DiceRoller.RollD6(2,-2) * (1/10 * (maximumDiameter - minimumDiameter))) + minimumDiameter;
            float variation = Random.Shared.NextSingle(0.95f, 1.05f);
            return Math.Clamp(MathF.Round(baseDiameter * variation, 2), 0f, float.PositiveInfinity);
        }

        private (float minimumSize, float maximumSize) GetSizeContraints(PlanetSize size) => size switch
        {
            PlanetSize.Large => (0.065f, 0.091f),
            PlanetSize.Standard => (0.03f, 0.065f),
            PlanetSize.Small => (0.024f, 0.03f),
            PlanetSize.Tiny => (0.004f, 0.024f),
            _ => throw new ArgumentOutOfRangeException()
        };

        private float GenerateRandomDensity(WorldTypeModel world)
        {
            float randomDensity;


            if ((world.Type == Enums.PlanetType.Ice && (world.Size == Enums.PlanetSize.Tiny || world.Size == Enums.PlanetSize.Small)) ||
            world.Type == Enums.PlanetType.Sulfur || world.Type == Enums.PlanetType.Hadean ||world.Type == Enums.PlanetType.Ammonia)
                randomDensity = RollIcyCore();
            else if (world.Type == Enums.PlanetType.Rock)
                randomDensity = SmallIronCore();
            else
                randomDensity = LargeIronCore();

            float variation = Random.Shared.NextSingle(0.95f, 1.05f);
            return Math.Clamp(MathF.Round(randomDensity * variation, 2), 0f, float.PositiveInfinity);
        }

        private float RollIcyCore() => DiceRoller.BasicRoll() switch
        {
            >= 3 and <= 6 => 0.3f,
            >= 7 and <= 10 => 0.4f,
            >= 11 and <= 14 => 0.5f,
            >= 15 and <= 17 => 0.6f,
            18 => 0.7f,
            _ => throw new NotImplementedException()
        };

        private float SmallIronCore() => DiceRoller.BasicRoll() switch
        {
            >= 3 and <= 6 => 0.6f,
            >= 7 and <= 10 => 0.7f,
            >= 11 and <= 14 => 0.8f,
            >= 15 and <= 17 => 0.9f,
            18 => 1.0f,
            _ => throw new NotImplementedException()
        };

        private float LargeIronCore() => DiceRoller.BasicRoll() switch
        {
            >= 3 and <= 6 => 0.8f,
            >= 7 and <= 10 => 0.9f,
            >= 11 and <= 14 => 1f,
            >= 15 and <= 17 => 1.1f,
            18 => 1.2f,
            _ => throw new NotImplementedException()
        };

        public float DensityToGCC() => Density * 5.52f;

        public float DiameterToMiles() => Diameter * 7930;
    }
}