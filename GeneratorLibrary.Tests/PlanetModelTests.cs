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

        [Theory]
        [InlineData(PlanetType.Ice)]
        [InlineData(PlanetType.Hadean)]
        [InlineData(PlanetType.Rock)]
        [InlineData(PlanetType.Ammonia)]
        [InlineData(PlanetType.Greenhouse)]
        [InlineData(PlanetType.Sulfur)]
        [InlineData(PlanetType.Ocean)]
        [InlineData(PlanetType.Garden)]
        [InlineData(PlanetType.AsteroidBelt)]
        [InlineData(PlanetType.GasGiant)]
        [InlineData(PlanetType.Chthonian)]
        public void CanCreateWorldBasedOnType(PlanetType type)
        {
            var planet = new PlanetModel("Test", type, PlanetSize.Standard);
            Assert.True(planet.WorldType.Type.Equals(type));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void CanCreateWorldBasedOnSize(PlanetSize size)
        {
            var planet = new PlanetModel("Test", PlanetType.Ice, size);
            Assert.True(planet.WorldType.Size.Equals(size));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void CanCreateOnlyStandardAndLargeSizedGardenWorlds(PlanetSize size)
        {
            PlanetModel planet = new PlanetModel("Test", PlanetType.Garden, size);

            if (size == PlanetSize.Standard || size == PlanetSize.Large)
                Assert.True(planet.WorldType.Type == PlanetType.Garden && planet.WorldType.Size == size);
            else
                Assert.Fail("Not Implemented Yet.");
        }
    }
}