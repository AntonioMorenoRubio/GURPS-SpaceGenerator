using GeneratorLibrary.PlanetGeneration;
using GeneratorLibrary.PlanetGeneration.Enums;

namespace GeneratorLibrary.Tests
{
    public class AtmosphereModelTests
    {
        public static IEnumerable<object[]> GetPossibleWorldTypes() => new List<object[]>
        {
                new object[] { new WorldTypeModel(PlanetSize.Tiny, PlanetType.Rock) },
                new object[] { new WorldTypeModel(PlanetSize.Tiny, PlanetType.Ice) },
                new object[] { new WorldTypeModel(PlanetSize.Tiny, PlanetType.Sulfur) },
                new object[] { new WorldTypeModel(PlanetSize.Small, PlanetType.Hadean) },
                new object[] { new WorldTypeModel(PlanetSize.Small, PlanetType.Ice) },
                new object[] { new WorldTypeModel(PlanetSize.Small, PlanetType.Rock) },
                new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Hadean) },
                new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Ammonia) },
                new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Ice) },
                new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Ocean) },
                new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Garden) },
                new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Greenhouse) },
                new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Chthonian) },
                new object[] { new WorldTypeModel(PlanetSize.Large, PlanetType.Ammonia) },
                new object[] { new WorldTypeModel(PlanetSize.Large, PlanetType.Ice) },
                new object[] { new WorldTypeModel(PlanetSize.Large, PlanetType.Ocean) },
                new object[] { new WorldTypeModel(PlanetSize.Large, PlanetType.Garden) },
                new object[] { new WorldTypeModel(PlanetSize.Large, PlanetType.Greenhouse) },
                new object[] { new WorldTypeModel(PlanetSize.Large, PlanetType.Chthonian) },
                new object[] { new WorldTypeModel(PlanetSize.Special, PlanetType.AsteroidBelt) },
                new object[] { new WorldTypeModel(PlanetSize.Special, PlanetType.GasGiant) }
        };

        List<WorldTypeModel> possibleWorldTypes = new List<WorldTypeModel>
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
            new WorldTypeModel(PlanetSize.Special, PlanetType.GasGiant)
        };

        List<WorldTypeModel> possibleWorldsWithAtmosphere = new List<WorldTypeModel>
        {
            new WorldTypeModel(PlanetSize.Small, PlanetType.Ice),
            new WorldTypeModel(PlanetSize.Standard, PlanetType.Ammonia),
            new WorldTypeModel(PlanetSize.Standard, PlanetType.Ice),
            new WorldTypeModel(PlanetSize.Standard, PlanetType.Ocean),
            new WorldTypeModel(PlanetSize.Standard, PlanetType.Garden),
            new WorldTypeModel(PlanetSize.Standard, PlanetType.Greenhouse),
            new WorldTypeModel(PlanetSize.Large, PlanetType.Ammonia),
            new WorldTypeModel(PlanetSize.Large, PlanetType.Ice),
            new WorldTypeModel(PlanetSize.Large, PlanetType.Ocean),
            new WorldTypeModel(PlanetSize.Large, PlanetType.Garden),
            new WorldTypeModel(PlanetSize.Large, PlanetType.Greenhouse),
            new WorldTypeModel(PlanetSize.Special, PlanetType.GasGiant)
        };

        Dictionary<(PlanetSize, PlanetType), List<string>> possibleAtmosphereCompositions = new Dictionary<(PlanetSize, PlanetType), List<string>>
        {
            { (PlanetSize.Small, PlanetType.Ice), new List<string> { "Nitrogen", "Methane" } },
            { (PlanetSize.Standard, PlanetType.Ammonia), new List<string> { "Nitrogen", "Ammonia","Methane" } },
            { (PlanetSize.Standard, PlanetType.Ice), new List<string> { "Carbon Dioxide", "Nitrogen" } },
            { (PlanetSize.Standard, PlanetType.Ocean), new List<string> { "Carbon Dioxide", "Nitrogen" } },
            { (PlanetSize.Standard, PlanetType.Garden), new List<string> { "Nitrogen", "Oxygen" } },
            { (PlanetSize.Standard, PlanetType.Greenhouse), new List<string> { "Carbon Dioxide", "Nitrogen" } },
            { (PlanetSize.Large, PlanetType.Ammonia), new List<string> { "Helium", "Ammonia","Methane" } },
            { (PlanetSize.Large, PlanetType.Ice), new List<string> { "Helium", "Nitrogen" } },
            { (PlanetSize.Large, PlanetType.Ocean), new List<string> { "Helium", "Nitrogen" } },
            { (PlanetSize.Large, PlanetType.Garden), new List<string> { "Nitrogen", "Oxygen", "Noble gases" } },
            { (PlanetSize.Large, PlanetType.Greenhouse), new List<string> { "Carbon Dioxide", "Nitrogen" } },
            { (PlanetSize.Special, PlanetType.GasGiant), new List<string> { "Hydrogen", "Helium" } },
        };


        [Fact]
        public void CanCreateAtmosphere()
        {
            AtmosphereModel model = new AtmosphereModel();
            Assert.NotNull(model);
        }

        [Fact]
        public void OnlyValidWorldsCanHaveAtmosphere()
        {
            for (int i = 0; i < possibleWorldTypes.Count; i++)
            {
                if (possibleWorldsWithAtmosphere.Exists(x =>
                    x.Type == possibleWorldTypes[i].Type &&
                    x.Size == possibleWorldTypes[i].Size))
                    Assert.True(AtmosphereModel.CanHaveAtmosphere(possibleWorldTypes[i]));
                else
                    Assert.False(AtmosphereModel.CanHaveAtmosphere(possibleWorldTypes[i]));
            }
        }

        [Theory]
        [MemberData(nameof(GetPossibleWorldTypes))]
        public void AtmosphericMassIsAlwaysZeroOrPositive(WorldTypeModel worldType)
        {
            Assert.InRange(AtmosphereModel.GenerateAtmosphericMass(worldType), 0f, float.PositiveInfinity);
        }

        [Fact]
        public void AssignCorrectAtmosphericCompositionBasedOnWorldType()
        {
            WorldTypeModel world;
            List<string> actualComposition, expectedComposition = new List<string>();

            for (int i = 0; i < possibleWorldsWithAtmosphere.Count; i++)
            {
                world = possibleWorldsWithAtmosphere[i];
                actualComposition = new AtmosphereModel(possibleWorldsWithAtmosphere[i]).Composition;
                if (possibleAtmosphereCompositions.TryGetValue((world.Size, world.Type), out expectedComposition))
                {
                    for (int j = 0; j < expectedComposition.Count; j++)
                    {
                        Assert.Contains<string>(expectedComposition[j], actualComposition);
                    }
                }
            }
        }



    }
}
