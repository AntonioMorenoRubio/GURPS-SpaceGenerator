using GeneratorLibrary.PlanetGeneration;
using GeneratorLibrary.PlanetGeneration.Enums;

namespace GeneratorLibrary.Tests.TestData
{
    public static class ConstantData
    {
        public static List<WorldTypeModel> GetAllWorldPossibleWorldCombinations()
        {
            List<WorldTypeModel> output = new List<WorldTypeModel>();

            for (int i = 0; i < Enum.GetValues<PlanetSize>().Length; i++)
            {
                for (int j = 0; j < Enum.GetValues<PlanetType>().Length; j++)
                {
                    output.Add(new WorldTypeModel
                    {
                        Size = (PlanetSize)i,
                        Type = (PlanetType)j
                    });
                }
            }
            return output;
        }

        public static List<WorldTypeModel> GetValidWorldTypes()
        {
            return new List<WorldTypeModel>
            {
                new WorldTypeModel(PlanetSize.Tiny, PlanetType.Rock),
                new WorldTypeModel(PlanetSize.Tiny, PlanetType.Ice),
                new WorldTypeModel(PlanetSize.Tiny, PlanetType.Sulfur),
                new WorldTypeModel(PlanetSize.Small, PlanetType.Hadean),
                new WorldTypeModel(PlanetSize.Small, PlanetType.Ice),
                new WorldTypeModel(PlanetSize.Small, PlanetType.Rock),
                new WorldTypeModel(PlanetSize.Standard, PlanetType.Hadean),
                new WorldTypeModel(PlanetSize.Standard, PlanetType.Ammonia),
                new WorldTypeModel(PlanetSize.Standard, PlanetType.Ice),
                new WorldTypeModel(PlanetSize.Standard, PlanetType.Ocean),
                new WorldTypeModel(PlanetSize.Standard, PlanetType.Garden),
                new WorldTypeModel(PlanetSize.Standard, PlanetType.Greenhouse),
                new WorldTypeModel(PlanetSize.Standard, PlanetType.Chthonian),
                new WorldTypeModel(PlanetSize.Large, PlanetType.Ammonia),
                new WorldTypeModel(PlanetSize.Large, PlanetType.Ice),
                new WorldTypeModel(PlanetSize.Large, PlanetType.Ocean),
                new WorldTypeModel(PlanetSize.Large, PlanetType.Garden),
                new WorldTypeModel(PlanetSize.Large, PlanetType.Greenhouse),
                new WorldTypeModel(PlanetSize.Large, PlanetType.Chthonian),
                new WorldTypeModel(PlanetSize.Special, PlanetType.AsteroidBelt),
                new WorldTypeModel(PlanetSize.Special, PlanetType.GasGiant),
            };
        }

        public static List<WorldTypeModel> GetInvalidWorldTypes()
        {
            return GetAllWorldPossibleWorldCombinations().Except(GetValidWorldTypes()).ToList();
        }
    }
}
