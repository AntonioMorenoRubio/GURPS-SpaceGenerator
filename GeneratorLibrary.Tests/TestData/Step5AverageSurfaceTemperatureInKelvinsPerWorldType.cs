using GeneratorLibrary.PlanetGeneration;
using GeneratorLibrary.PlanetGeneration.Enums;

namespace GeneratorLibrary.Tests.TestData
{
    public class Step5AverageSurfaceTemperatureInKelvinsPerWorldType : TheoryData<WorldTypeModel, int, int>
    {
        public Step5AverageSurfaceTemperatureInKelvinsPerWorldType()
        {
            Add(new WorldTypeModel(PlanetSize.Special, PlanetType.AsteroidBelt), 140, 500);
            Add(new WorldTypeModel(PlanetSize.Tiny, PlanetType.Ice), 80, 140);
            Add(new WorldTypeModel(PlanetSize.Tiny, PlanetType.Sulfur), 80, 140);
            Add(new WorldTypeModel(PlanetSize.Tiny, PlanetType.Rock), 140, 500);
            Add(new WorldTypeModel(PlanetSize.Small, PlanetType.Hadean), 50, 80);
            Add(new WorldTypeModel(PlanetSize.Small, PlanetType.Ice), 80, 140);
            Add(new WorldTypeModel(PlanetSize.Small, PlanetType.Rock), 140, 500);
            Add(new WorldTypeModel(PlanetSize.Standard, PlanetType.Hadean), 50, 80);
            Add(new WorldTypeModel(PlanetSize.Standard, PlanetType.Ammonia), 140, 215);
            Add(new WorldTypeModel(PlanetSize.Standard, PlanetType.Ice), 80, 230);
            Add(new WorldTypeModel(PlanetSize.Standard, PlanetType.Ocean), 250, 340);
            Add(new WorldTypeModel(PlanetSize.Standard, PlanetType.Garden), 250, 340);
            Add(new WorldTypeModel(PlanetSize.Standard, PlanetType.Greenhouse), 500, 950);
            Add(new WorldTypeModel(PlanetSize.Standard, PlanetType.Chthonian), 500, 950);
            Add(new WorldTypeModel(PlanetSize.Large, PlanetType.Ammonia), 140, 215);
            Add(new WorldTypeModel(PlanetSize.Large, PlanetType.Ice), 80, 230);
            Add(new WorldTypeModel(PlanetSize.Large, PlanetType.Ocean), 250, 340);
            Add(new WorldTypeModel(PlanetSize.Large, PlanetType.Garden), 250, 340);
            Add(new WorldTypeModel(PlanetSize.Large, PlanetType.Greenhouse), 500, 950);
            Add(new WorldTypeModel(PlanetSize.Large, PlanetType.Chthonian), 500, 950);
        }
    }
}
