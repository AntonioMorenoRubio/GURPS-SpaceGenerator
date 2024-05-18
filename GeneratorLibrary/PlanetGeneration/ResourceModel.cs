using GeneratorLibrary.PlanetGeneration.Enums;

namespace GeneratorLibrary.PlanetGeneration
{
    public class ResourceModel
    {
        public string Description { get; set; }
        public int ResourceValueModifier { get; set; }

        public ResourceModel(PlanetType planetType)
        {
            if (planetType == PlanetType.AsteroidBelt)
                ResourceValueModifier = RollForAsteroidBelts();
            else
                ResourceValueModifier = RollForOtherPlanets();

            Description = GetDescriptionBasedOnRVM();
        }

        private string GetDescriptionBasedOnRVM() => ResourceValueModifier switch
        {
            -5 => "Worthless",
            -4 => "Very Scant",
            -3 => "Scant",
            -2 => "Very Poor",
            -1 => "Poor",
            0 => "Average",
            1 => "Abundant",
            2 => "Very Abundant",
            3 => "Rich",
            4 => "Very Rich",
            5 => "Motherlode",
            _ => throw new ArgumentOutOfRangeException()
        };

        private int RollForAsteroidBelts() => DiceRoller.BasicRoll() switch
        {
            3 => -5,
            4 => -4,
            5 => -3,
            6 or 7 => -2,
            8 or 9 => -1,
            10 or 11 => 0,
            12 or 13 => 1,
            14 or 15 => 2,
            16 => 3,
            17 => 4,
            18 => 5,
            _ => throw new ArgumentOutOfRangeException()
        };

        private int RollForOtherPlanets() => DiceRoller.BasicRoll() switch
        {
            <3 => -3,
            3 or 4 => -2,
            5 or 6 or 7 => -1,
            >=8 and <=13 => 0,
            14 or 15 or 16 => 1,
            17 or 18 => 2,
            >= 19 => 3
        };
    }
}