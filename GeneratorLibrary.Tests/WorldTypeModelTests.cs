using System.Drawing;

namespace GeneratorLibrary.Tests
{
    public class WorldTypeModelTests
    {
        [Fact]
        public void CanCreateWorldTypeModel()
        {
            WorldTypeModel model = new WorldTypeModel();
            Assert.NotNull(model);
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void UsingTypeAndSize_IceCreatesAllButSpecialSized(PlanetSize size)
        {
            PlanetType type = PlanetType.Ice;

            if (size == PlanetSize.Special)
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(type, size));
            else
            {
                WorldTypeModel model = new WorldTypeModel(type, size);
                Assert.True(model.Type == type && model.Size == size);
            }
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void CanCreateOnlyTinyAndSmallSizedRockWorlds(PlanetSize size)
        {
            PlanetType type = PlanetType.Rock;

            if (size == PlanetSize.Tiny || size == PlanetSize.Small)
            {
                WorldTypeModel model = new WorldTypeModel(type, size);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(type, size));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void CanCreateOnlyTinySizedSulfurWorlds(PlanetSize size)
        {
            PlanetType type = PlanetType.Sulfur;

            if (size == PlanetSize.Tiny)
            {
                WorldTypeModel model = new WorldTypeModel(type, size);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(type, size));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void CanCreateOnlySmallAndStandardSizedHadeanWorlds(PlanetSize size)
        {
            PlanetType type = PlanetType.Hadean;

            if (size == PlanetSize.Small || size == PlanetSize.Standard)
            {
                WorldTypeModel model = new WorldTypeModel(type, size);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(type, size));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void CanCreateOnlyStandardAndLargeSizedOceanWorlds(PlanetSize size)
        {
            PlanetType type = PlanetType.Ocean;

            if (size == PlanetSize.Standard || size == PlanetSize.Large)
            {
                WorldTypeModel model = new WorldTypeModel(type, size);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(type, size));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void CanCreateOnlyStandardAndLargeSizedGardenWorlds(PlanetSize size)
        {
            PlanetType type = PlanetType.Garden;

            if (size == PlanetSize.Standard || size == PlanetSize.Large)
            {
                WorldTypeModel model = new WorldTypeModel(type, size);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(type, size));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void CanCreateOnlyStandardAndLargeSizedGreenhouseWorlds(PlanetSize size)
        {
            PlanetType type = PlanetType.Greenhouse;

            if (size == PlanetSize.Standard || size == PlanetSize.Large)
            {
                WorldTypeModel model = new WorldTypeModel(type, size);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(type, size));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void CanCreateOnlyStandardAndLargeSizedAmmoniaWorlds(PlanetSize size)
        {
            PlanetType type = PlanetType.Ammonia;

            if (size == PlanetSize.Standard || size == PlanetSize.Large)
            {
                WorldTypeModel model = new WorldTypeModel(type, size);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(type, size));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void CanCreateOnlyStandardAndLargeSizedChthonianWorlds(PlanetSize size)
        {
            PlanetType type = PlanetType.Chthonian;

            if (size == PlanetSize.Standard || size == PlanetSize.Large)
            {
                WorldTypeModel model = new WorldTypeModel(type, size);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(type, size));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void CanCreateOnlySpecialAsteroidBeltWorlds(PlanetSize size)
        {
            PlanetType type = PlanetType.AsteroidBelt;

            if (size == PlanetSize.Special)
            {
                WorldTypeModel model = new WorldTypeModel(type, size);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(type, size));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void CanCreateOnlySpecialGasGiantWorlds(PlanetSize size)
        {
            PlanetType type = PlanetType.GasGiant;

            if (size == PlanetSize.Special)
            {
                WorldTypeModel model = new WorldTypeModel(type, size);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(type, size));
        }

    }
}
