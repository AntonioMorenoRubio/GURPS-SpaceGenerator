using GeneratorLibrary.PlanetGeneration;
using GeneratorLibrary.PlanetGeneration.Enums;
using GeneratorLibrary.Tests.TestData;

namespace GeneratorLibrary.Tests
{
    public class ClimateModelTests
    {
        [Theory]
        [InlineData(0, -459.67f)]
        [InlineData(100, -279.67f)]
        [InlineData(300, 80.33f)]
        [InlineData(500	, 440.33f)]
        [InlineData(1000, 1340.33f)]
        public void ConvertKelvinToFarenheitDegrees(int kelvin, float expected)
        {
            float actual = ClimateModel.ConvertToFarenheit(kelvin);
            float tolerance = 0.0001f;
            Assert.InRange(actual, expected - tolerance, expected + tolerance);
        }

        [Theory]
        [InlineData(0,	-273.15f)]
        [InlineData(100, -173.15f)]
        [InlineData(300, 26.85f)]
        [InlineData(500	, 226.85f)]
        [InlineData(1000, 726.85f)]
        public void ConvertKelvinToCelsiusDegrees(int kelvin, float expected)
        {
            float actual = ClimateModel.ConvertToCelsius(kelvin);
            float tolerance = 0.0001f;
            Assert.InRange(actual, expected - tolerance, expected + tolerance);
        }

        [Theory]
        [ClassData(typeof(Step6AverageSurfaceTemperatureInKelvinsPerWorldType))]
        public void AssignValidKelvinTemperatureBasedOnWorldTypeINFullRandomMethod(WorldTypeModel worldType, int minimum, int maximum)
        {
            int actual = ClimateModel.GenerateAverageSurfaceTemperature(worldType, GenerationMethod.FullRandom);
            Assert.InRange(actual, minimum, maximum);
        }

        [Theory]
        [ClassData(typeof(Step6KelvinToClimateTestData))]
        public void AssignCorrectClimateTypeBasedOnKelvinTemperature(int kelvinDegrees, ClimateType expected)
        {
            ClimateType actual = ClimateModel.DetermineClimateType(kelvinDegrees);
            Assert.True(actual.Equals(expected));
        }
    }
}
