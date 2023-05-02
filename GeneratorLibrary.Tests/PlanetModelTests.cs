namespace GeneratorLibrary.Tests
{
    public class PlanetModelTests
    {
        [Fact]
        public void CanCreatePlanet()
        {
            PlanetModel planet = new PlanetModel();
            Assert.NotNull(planet);
        }

        [Theory]
        [InlineData("Terra")]
        [InlineData("Martis")]
        [InlineData("Venus")]
        public void CreatePlanetWithName(string name)
        {
            PlanetModel planet = new PlanetModel(name);
            Assert.Equal(name, planet.Name);
        }

        [Theory]
        [InlineData("Terra", "The third planet of the Solar System.")]
        [InlineData("Martis", "The fourth planet of the Solar System.")]
        [InlineData("Venus", "The second planet of the Solar System.")]
        public void CreatePlanetWithNameAndDescription(string name, string description)
        {
            var planet = new PlanetModel(name, description);
            Assert.Equal(name, planet.Name);
            Assert.Equal(description, planet.Description);
        }
    }
}