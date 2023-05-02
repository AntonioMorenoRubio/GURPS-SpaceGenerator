using System.Drawing;

namespace GeneratorLibrary
{
    public class WorldTypeModel
    {
        public PlanetSize Size { get; set; }
        public PlanetType Type { get; set; }

        public WorldTypeModel()
        {
            (Size, Type) = GenerateNewWorld(GetOverallType());
        }

        public WorldTypeModel(PlanetType type, PlanetSize size)
        {
            if (CheckValidCombination(type, size))
            {
                Size = size;
                Type = type;
            }
            else
                throw new ArgumentException("World Type and Size are not compatible. Check combinations from p.75-p.77.");
        }

        public static bool CheckValidCombination(PlanetType type, PlanetSize size) => (size, type) switch
        {
            (PlanetSize.Tiny, PlanetType.Rock) => true,
            (PlanetSize.Tiny, PlanetType.Ice) => true,
            (PlanetSize.Tiny, PlanetType.Sulfur) => true,
            (PlanetSize.Small, PlanetType.Hadean) => true,
            (PlanetSize.Small, PlanetType.Ice) => true,
            (PlanetSize.Small, PlanetType.Rock) => true,
            (PlanetSize.Standard, PlanetType.Hadean) => true,
            (PlanetSize.Standard, PlanetType.Ammonia) => true,
            (PlanetSize.Standard, PlanetType.Ice) => true,
            (PlanetSize.Standard, PlanetType.Ocean) => true,
            (PlanetSize.Standard, PlanetType.Garden) => true,
            (PlanetSize.Standard, PlanetType.Greenhouse) => true,
            (PlanetSize.Standard, PlanetType.Chthonian) => true,
            (PlanetSize.Large, PlanetType.Ammonia) => true,
            (PlanetSize.Large, PlanetType.Ice) => true,
            (PlanetSize.Large, PlanetType.Ocean) => true,
            (PlanetSize.Large, PlanetType.Garden) => true,
            (PlanetSize.Large, PlanetType.Greenhouse) => true,
            (PlanetSize.Large, PlanetType.Chthonian) => true,
            (PlanetSize.Special, PlanetType.AsteroidBelt) => true,
            (PlanetSize.Special, PlanetType.GasGiant) => true,
            (_, _) => false
        };

        private (PlanetSize Size, PlanetType Type) GenerateNewWorld(string overallType) => overallType switch
        {
            "Hostile" => GenerateHostileWorld(),
            "Barren" => GenerateBarrenWorld(),
            "Garden" => GenerateGardenWorld(),
            _ => throw new Exception($"OverallType for Planet not expected: {overallType}")
        };

        public string GetOverallType() => DiceRoller.BasicRoll() switch
        {
            <= 7 => "Hostile",
            >= 8 and <= 13 => "Barren",
            >= 14 and <= 18 => "Garden",
            _ => throw new Exception("Couldn't define an Overall Type based on roll")
        };

        public (PlanetSize Size, PlanetType Type) GenerateHostileWorld() => DiceRoller.BasicRoll() switch
        {
            3 or 4 => (PlanetSize.Standard, PlanetType.Chthonian),
            5 or 6 => (PlanetSize.Standard, PlanetType.Greenhouse),
            >= 7 and <= 9 => (PlanetSize.Tiny, PlanetType.Sulfur),
            >= 10 and <= 12 => (PlanetSize.Standard, PlanetType.Ammonia),
            13 or 14 => (PlanetSize.Large, PlanetType.Ammonia),
            15 or 16 => (PlanetSize.Large, PlanetType.Greenhouse),
            17 or 18 => (PlanetSize.Large, PlanetType.Chthonian),
            _ => throw new Exception("Couldn't generate hostile world.")
        };

        public (PlanetSize Size, PlanetType Type) GenerateBarrenWorld() => DiceRoller.BasicRoll() switch
        {
            3 => (PlanetSize.Small, PlanetType.Hadean),
            4 => (PlanetSize.Small, PlanetType.Ice),
            5 or 6 => (PlanetSize.Small, PlanetType.Rock),
            7 or 8 => (PlanetSize.Tiny, PlanetType.Rock),
            9 or 10 => (PlanetSize.Tiny, PlanetType.Ice),
            11 or 12 => (PlanetSize.Special, PlanetType.AsteroidBelt),
            13 or 14 => (PlanetSize.Standard, PlanetType.Ocean),
            15 => (PlanetSize.Standard, PlanetType.Ice),
            16 => (PlanetSize.Standard, PlanetType.Hadean),
            17 => (PlanetSize.Large, PlanetType.Ocean),
            18 => (PlanetSize.Large, PlanetType.Ice),
            _ => throw new Exception("Couldn't generate barren world.")
        };

        public (PlanetSize Size, PlanetType Type) GenerateGardenWorld() => DiceRoller.BasicRoll() switch
        {
            >= 3 and <= 16 => (PlanetSize.Standard, PlanetType.Garden),
            >= 17 and <= 18 => (PlanetSize.Large, PlanetType.Garden),
            _ => throw new Exception("Couldn't generate garden world.")
        };
    }
}