
using GeneratorLibrary.PlanetGeneration.Enums;

namespace GeneratorLibrary.PlanetGeneration
{
    public class HabitabilityModel
    {
        public int Score;
        private List<int> modifiers;

        public HabitabilityModel(AtmosphereModel? atmosphere, HydrographicCoverageModel hydrographic, ClimateModel? climate)
        {
            modifiers = new List<int>();

            if (atmosphere == null || atmosphere.PressureCategory == Enums.AtmosphericPressureCategory.Trace)
                modifiers.Add(0);
            else if (atmosphere.Composition.Contains("Oxygen"))
            {
                modifiers.Add(BreathableAtmosphereByPressure(atmosphere.PressureCategory));
                if (atmosphere.MarginalAtmosphere == null)
                    modifiers.Add(1);

                modifiers.Add(BreathableAtmosphereByClimate(climate.Type));
            } else
            {
                modifiers.Add(NonBreathableAtmosphere(atmosphere.Characteristics));
            }

            if (hydrographic.HasLiquidWater == false || hydrographic.WaterCoveragePercent == 0f)
            {
                modifiers.Add(0);
            } else
                modifiers.Add(GetHydrographicCoverageModifier(hydrographic.WaterCoveragePercent));
        }

        private int NonBreathableAtmosphere(List<AtmosphereCharacteristic> characteristics)
        {
            if (characteristics.Contains(AtmosphereCharacteristic.Suffocating) && (
            characteristics.Contains(AtmosphereCharacteristic.MildlyToxic) ||
            characteristics.Contains(AtmosphereCharacteristic.HighlyToxic) ||
            characteristics.Contains(AtmosphereCharacteristic.LethallyToxic)) &&
            characteristics.Contains(AtmosphereCharacteristic.Corrosive))
                return -2;
            else if (characteristics.Contains(AtmosphereCharacteristic.Suffocating) && (
            characteristics.Contains(AtmosphereCharacteristic.MildlyToxic) ||
            characteristics.Contains(AtmosphereCharacteristic.HighlyToxic) ||
            characteristics.Contains(AtmosphereCharacteristic.LethallyToxic)))
                return -1;
            else
                return 0;
        }

        private int BreathableAtmosphereByPressure(AtmosphericPressureCategory pressureCategory) => pressureCategory switch
        {
            AtmosphericPressureCategory.VeryThin => 1,
            AtmosphericPressureCategory.Thin => 2,
            AtmosphericPressureCategory.Standard or AtmosphericPressureCategory.Dense => 3,
            AtmosphericPressureCategory.VeryDense or AtmosphericPressureCategory.SuperDense => 1,
            _ => 0
        };

        private int BreathableAtmosphereByClimate(ClimateType climateType) => climateType switch
        {
            ClimateType.Frozen or ClimateType.VeryCold or ClimateType.VeryHot or ClimateType.Infernal => 0,
            ClimateType.Cold or ClimateType.Hot => 1,
            ClimateType.Chilly or ClimateType.Cool or ClimateType.Normal or ClimateType.Warm or ClimateType.Tropical => 2,
            _ => 0
        };


        private int GetHydrographicCoverageModifier(float waterCoveragePercent) => waterCoveragePercent switch
        {
            >= 0.01f and < 0.6f => 1,
            >= 0.6f and < 0.91f => 2,
            >= 0.91f and < 1f => 1,
            1f => 0,
            _ => 0
        };
    }
}