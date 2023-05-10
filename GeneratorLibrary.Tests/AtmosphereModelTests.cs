using GeneratorLibrary.PlanetGeneration;

namespace GeneratorLibrary.Tests
{
    public class AtmosphereModelTests
    {
        List<WorldTypeModel> worldTypes;

        public AtmosphereModelTests()
        {
            worldTypes = new List<WorldTypeModel>()
            {

            };
        }

        [Fact]
        public void CanCreateAtmosphere()
        {
            AtmosphereModel model = new AtmosphereModel();
            Assert.NotNull(model);
        }



        [Theory]
        [InlineData(10)]
        public void AssignCorrectPressureFactorBasedOnWorldType(int expectedPressure)
        {

        }

    }
}
