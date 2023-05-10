using GeneratorLibrary.PlanetGeneration;

namespace GeneratorLibrary.Tests
{
    public class AtmosphereModelTests
    {
        [Fact]
        public void CanCreateAtmosphere()
        {
            AtmosphereModel model = new AtmosphereModel();
            Assert.NotNull(model);
        }


    }
}
