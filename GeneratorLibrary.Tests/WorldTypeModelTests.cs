using GeneratorLibrary.PlanetGeneration;
using GeneratorLibrary.PlanetGeneration.Enums;

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
        public void Ice_CanBe_AllButSpecialSize(PlanetSize size)
        {
            PlanetType type = PlanetType.Ice;

            if (size == PlanetSize.Special)
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(size, type));
            else
            {
                WorldTypeModel model = new WorldTypeModel(size, type);
                Assert.True(model.Type == type && model.Size == size);
            }
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void Rock_CanOnlyBe_TinyAndSmallSize(PlanetSize size)
        {
            PlanetType type = PlanetType.Rock;

            if (size == PlanetSize.Tiny || size == PlanetSize.Small)
            {
                WorldTypeModel model = new WorldTypeModel(size, type);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(size, type));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void Sulfur_CanOnlyBe_TinySize(PlanetSize size)
        {
            PlanetType type = PlanetType.Sulfur;

            if (size == PlanetSize.Tiny)
            {
                WorldTypeModel model = new WorldTypeModel(size, type);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(size, type));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void Hadean_CanOnlyBe_SmallAndStandardSize(PlanetSize size)
        {
            PlanetType type = PlanetType.Hadean;

            if (size == PlanetSize.Small || size == PlanetSize.Standard)
            {
                WorldTypeModel model = new WorldTypeModel(size, type);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(size, type));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void Ocean_CanOnlyBe_StandardAndLargeSize(PlanetSize size)
        {
            PlanetType type = PlanetType.Ocean;

            if (size == PlanetSize.Standard || size == PlanetSize.Large)
            {
                WorldTypeModel model = new WorldTypeModel(size, type);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(size, type));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void Garden_CanOnlyBe_StandardAndLargeSize(PlanetSize size)
        {
            PlanetType type = PlanetType.Garden;

            if (size == PlanetSize.Standard || size == PlanetSize.Large)
            {
                WorldTypeModel model = new WorldTypeModel(size, type);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(size, type));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void Greenhouse_CanOnlyBe_StandardAndLargeSize(PlanetSize size)
        {
            PlanetType type = PlanetType.Greenhouse;

            if (size == PlanetSize.Standard || size == PlanetSize.Large)
            {
                WorldTypeModel model = new WorldTypeModel(size, type);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(size, type));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void Ammonia_CanOnlyBe_StandardAndLargeSize(PlanetSize size)
        {
            PlanetType type = PlanetType.Ammonia;

            if (size == PlanetSize.Standard || size == PlanetSize.Large)
            {
                WorldTypeModel model = new WorldTypeModel(size, type);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(size, type));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void Chthonian_CanOnlyBe_StandardAndLargeSize(PlanetSize size)
        {
            PlanetType type = PlanetType.Chthonian;

            if (size == PlanetSize.Standard || size == PlanetSize.Large)
            {
                WorldTypeModel model = new WorldTypeModel(size, type);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(size, type));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void AsteroidBelt_CanOnlyBe_SpecialSize(PlanetSize size)
        {
            PlanetType type = PlanetType.AsteroidBelt;

            if (size == PlanetSize.Special)
            {
                WorldTypeModel model = new WorldTypeModel(size, type);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(size, type));
        }

        [Theory]
        [InlineData(PlanetSize.Tiny)]
        [InlineData(PlanetSize.Small)]
        [InlineData(PlanetSize.Standard)]
        [InlineData(PlanetSize.Large)]
        [InlineData(PlanetSize.Special)]
        public void GasGiant_CanOnlyBe_SpecialSize(PlanetSize size)
        {
            PlanetType type = PlanetType.GasGiant;

            if (size == PlanetSize.Special)
            {
                WorldTypeModel model = new WorldTypeModel(size, type);
                Assert.True(model.Type == type && model.Size == size);
            }
            else
                Assert.Throws<ArgumentException>(() => new WorldTypeModel(size, type));
        }

    }
}