using GeneratorLibrary.PlanetGeneration;
using GeneratorLibrary.PlanetGeneration.Enums;

namespace GeneratorLibrary.Tests
{
    public class HydrographicCoverageTests
    {
        public static IEnumerable<object[]> DataForTests()
        {
            yield return new object[] { new WorldTypeModel(PlanetSize.Special, PlanetType.AsteroidBelt), 0f, 0f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Tiny, PlanetType.Rock), 0f, 0f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Small, PlanetType.Rock), 0f, 0f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Tiny, PlanetType.Ice), 0f, 0f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Small, PlanetType.Hadean), 0f, 0f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Hadean), 0f, 0f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Tiny, PlanetType.Sulfur), 0f, 0f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Small, PlanetType.Ice), 0.3f, 0.8f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Ammonia), 0.5f, 1f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Large, PlanetType.Ammonia), 0.5f, 1f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Ice), 0f, 0.2, };
            yield return new object[] { new WorldTypeModel(PlanetSize.Large, PlanetType.Ice), 0f, 0.2f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Ocean), 0.5f, 1f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Garden), 0.5f, 1f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Large, PlanetType.Ocean), 0.5f, 1f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Large, PlanetType.Garden), 0.5f, 1f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Greenhouse), 0f, 0.5f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Large, PlanetType.Greenhouse), 0f, 0.5f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Standard, PlanetType.Chthonian), 0f, 0f };
            yield return new object[] { new WorldTypeModel(PlanetSize.Large, PlanetType.Chthonian), 0f, 0f };
        }

        [Theory]
        [MemberData(nameof(DataForTests))]
        public void GeneratesValidHydrographicCoverageForWorldType(WorldTypeModel worldType, float minimumExpected, float maximumExpected)
        {
            HydrographicCoverageModel coverage = new HydrographicCoverageModel(worldType);

            Assert.InRange(coverage.WaterCoveragePercent, minimumExpected, maximumExpected);
        }
    }
}
