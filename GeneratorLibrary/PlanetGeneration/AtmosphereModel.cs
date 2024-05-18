using GeneratorLibrary.PlanetGeneration.Enums;

namespace GeneratorLibrary.PlanetGeneration
{
    public class AtmosphereModel
    {
        public float Pressure { get; set; }
        public AtmosphericPressureCategory PressureCategory { get; set; }
        public AtmosphericPressureCategory PressureClassFeltByHumans { get; set; }
        public float Mass { get; set; }
        public MarginalAtmosphere? MarginalAtmosphere { get; set; } = null;
        public List<string> Composition { get; set; } = new List<string>();
        public List<AtmosphereCharacteristic> Characteristics { get; set; } = new List<AtmosphereCharacteristic>();
        public bool IsToxic { get; set; } = false;

        public AtmosphereModel() { }

        /// <summary>
        /// Generates the basic values of an Atmosphere based on the steps indicated in Step 3 (pp.78-81).
        /// </summary>
        /// <param name="worldType">The World Type and Size created in Step 2.</param>
        public AtmosphereModel(WorldTypeModel worldType)
        {
            Mass = GenerateAtmosphericMass(worldType);

            if (worldType.Type == PlanetType.Garden)
            {
                if (DiceRoller.BasicRoll() >= 12)
                    MarginalAtmosphere = GenerateMarginalAtmosphere();
            }

            Composition = AssignBaseCompositionBasedOnWorldType(worldType);
            Characteristics = AssignBaseCharacteristicsBasedOnWorldType(worldType);

            if (MarginalAtmosphere != null)
                AddMarginalEffects();

            if (Characteristics.Contains(AtmosphereCharacteristic.MildlyToxic) || Characteristics.Contains(AtmosphereCharacteristic.HighlyToxic) || Characteristics.Contains(AtmosphereCharacteristic.LethallyToxic))
                IsToxic = true;
        }

        private void AddMarginalEffects()
        {
            (List<string> Composition, List<AtmosphereCharacteristic> Characteristics) marginal = GetMarginalCompositionAndCharacteristics();

            foreach (string compound in marginal.Composition)
                Composition.Add(compound);

            foreach (AtmosphereCharacteristic characteristic in marginal.Characteristics)
                Characteristics.Add(characteristic);
        }

        public static float GenerateAtmosphericMass(WorldTypeModel worldType)
        {
            if (worldType.Type == PlanetType.AsteroidBelt ||
                worldType.Size == PlanetSize.Tiny ||
                worldType.Size == PlanetSize.Small &&
                                (worldType.Type == PlanetType.Hadean || worldType.Type == PlanetType.Rock) ||
                worldType.Size == PlanetSize.Standard &&
                                (worldType.Type == PlanetType.Hadean || worldType.Type == PlanetType.Chthonian) ||
                worldType.Size == PlanetSize.Large && worldType.Type == PlanetType.Chthonian)
            {
                return 0f;
            }

            float baseMass = DiceRoller.BasicRoll() % 10f;
            float variation = Random.Shared.NextSingle(-0.05f, 0.05f);

            return Math.Clamp(MathF.Round(baseMass + variation, 2), 0f, float.PositiveInfinity);
        }

        private MarginalAtmosphere GenerateMarginalAtmosphere() => DiceRoller.BasicRoll() switch
        {
            3 or 4 => Enums.MarginalAtmosphere.ChlorineOrFluorine,
            5 or 6 => Enums.MarginalAtmosphere.SulphurCompounds,
            7 => Enums.MarginalAtmosphere.NitrogenCompounds,
            8 or 9 => Enums.MarginalAtmosphere.OrganicToxins,
            10 or 11 => Enums.MarginalAtmosphere.LowOxygen,
            12 or 13 => Enums.MarginalAtmosphere.Pollutants,
            14 => Enums.MarginalAtmosphere.HighCarbonDioxide,
            15 or 16 => Enums.MarginalAtmosphere.HighOxygen,
            17 or 18 => Enums.MarginalAtmosphere.InertGases,
            _ => throw new Exception("Couldn't generate marginal atmosphere.")
        };

        private List<string> AssignBaseCompositionBasedOnWorldType(WorldTypeModel worldType) => (worldType.Size, worldType.Type) switch
        {
            (PlanetSize.Small, PlanetType.Ice) => new List<string> { "Nitrogen", "Methane" },
            (PlanetSize.Standard, PlanetType.Ammonia) => new List<string> { "Nitrogen", "Ammonia", "Methane" },
            (PlanetSize.Large, PlanetType.Ammonia) => new List<string> { "Helium", "Ammonia", "Methane" },
            (PlanetSize.Standard, PlanetType.Ice) or (PlanetSize.Standard, PlanetType.Ocean) => new List<string> { "Nitrogen", "Carbon Dioxide" },
            (PlanetSize.Large, PlanetType.Ice) or (PlanetSize.Large, PlanetType.Ocean) => new List<string> { "Helium", "Nitrogen" },
            (PlanetSize.Standard, PlanetType.Garden) => new List<string> { "Nitrogen", "Oxygen" },
            (PlanetSize.Large, PlanetType.Garden) => new List<string> { "Nitrogen", "Noble gases", "Oxygen" },
            (PlanetSize.Standard, PlanetType.Greenhouse) or (PlanetSize.Large, PlanetType.Greenhouse) => new List<string> { "Carbon Dioxide", "Nitrogen" },
            (PlanetSize.Special, PlanetType.GasGiant) => new List<string> { "Hydrogen", "Helium" },
            _ => new List<string>()
        };

        private List<AtmosphereCharacteristic> AssignBaseCharacteristicsBasedOnWorldType(WorldTypeModel worldType) => (worldType.Size, worldType.Type) switch
        {
            (PlanetSize.Small, PlanetType.Ice) => DiceRoller.BasicRoll() <= 15 ?
            new List<AtmosphereCharacteristic> {
                AtmosphereCharacteristic.Suffocating,
                AtmosphereCharacteristic.MildlyToxic
            }
            : new List<AtmosphereCharacteristic> {
                AtmosphereCharacteristic.Suffocating,
                AtmosphereCharacteristic.HighlyToxic
            },
            (PlanetSize.Standard or PlanetSize.Large, PlanetType.Ammonia) =>
            new List<AtmosphereCharacteristic> {
                AtmosphereCharacteristic.Suffocating,
                AtmosphereCharacteristic.LethallyToxic,
                AtmosphereCharacteristic.Corrosive
            },
            (PlanetSize.Standard, PlanetType.Ice) or (PlanetSize.Standard, PlanetType.Ocean) => DiceRoller.BasicRoll() <= 12 ?
            new List<AtmosphereCharacteristic> {
                AtmosphereCharacteristic.Suffocating
            }
            : new List<AtmosphereCharacteristic> {
                AtmosphereCharacteristic.Suffocating,
                AtmosphereCharacteristic.MildlyToxic
            },
            (PlanetSize.Large, PlanetType.Ice) or (PlanetSize.Large, PlanetType.Ocean) =>
            new List<AtmosphereCharacteristic> {
                AtmosphereCharacteristic.Suffocating,
                AtmosphereCharacteristic.HighlyToxic
            },
            (PlanetSize.Standard or PlanetSize.Large, PlanetType.Garden) => new List<AtmosphereCharacteristic>(),
            (PlanetSize.Standard, PlanetType.Greenhouse) or (PlanetSize.Large, PlanetType.Greenhouse) =>
            new List<AtmosphereCharacteristic> {
                AtmosphereCharacteristic.Suffocating,
                AtmosphereCharacteristic.LethallyToxic,
                AtmosphereCharacteristic.Corrosive
            },
            (PlanetSize.Special, PlanetType.GasGiant) => new List<AtmosphereCharacteristic>
            {
                AtmosphereCharacteristic.Suffocating,
                AtmosphereCharacteristic.LethallyToxic
            },
            _ => new List<AtmosphereCharacteristic>()
        };

        private (List<string> Composition, List<AtmosphereCharacteristic> Characteristics) GetMarginalCompositionAndCharacteristics()
        {
            (List<string> Composition, List<AtmosphereCharacteristic> Characteristics) output = new()
            {
                Composition = new(),
                Characteristics = new()
            };

            switch (MarginalAtmosphere)
            {
                case Enums.MarginalAtmosphere.ChlorineOrFluorine:
                    if (Random.Shared.Next(2) / 100f <= 90f)
                    {
                        output.Composition.Add("Chlorine");
                        output.Characteristics.Add(AtmosphereCharacteristic.HighlyToxic);
                    }
                    else
                    {
                        output.Composition.Add("Fluorine");
                        output.Characteristics.Add(AtmosphereCharacteristic.HighlyToxic);
                    }
                    break;
                case Enums.MarginalAtmosphere.HighCarbonDioxide:
                    output.Composition.Add("Carbon Dioxide");
                    output.Characteristics.Add(AtmosphereCharacteristic.MildlyToxic);
                    break;
                case Enums.MarginalAtmosphere.NitrogenCompounds:
                    output.Composition.Add("Nitrogen Oxide");
                    output.Characteristics.Add(AtmosphereCharacteristic.MildlyToxic);
                    break;
                case Enums.MarginalAtmosphere.SulphurCompounds:
                    output.Composition.Add("Hydrogen Sulfide");
                    output.Composition.Add("Sulfur Dioxide");
                    output.Composition.Add("Sulfur Trioxide");
                    output.Characteristics.Add(AtmosphereCharacteristic.MildlyToxic);
                    break;
                case Enums.MarginalAtmosphere.OrganicToxins:
                    output.Composition.Add("Spores");
                    output.Characteristics.Add(AtmosphereCharacteristic.MildlyToxic);
                    break;
                case Enums.MarginalAtmosphere.Pollutants:
                    output.Characteristics.Add(AtmosphereCharacteristic.MildlyToxic);
                    break;
            }

            return output;
        }

        public void DetermineAtmosphericPressure(WorldTypeModel worldType, float surfaceGravity)
        {
            if (worldType.Type == PlanetType.AsteroidBelt ||
                worldType.Size == PlanetSize.Tiny ||
                worldType.Type == PlanetType.Hadean)
            {
                Pressure = 0f;
                PressureCategory = AtmosphericPressureCategory.None;
                PressureClassFeltByHumans = AtmosphericPressureCategory.None;
            }
            else if (worldType.Type == PlanetType.Chthonian ||
                (worldType.Size == PlanetSize.Small && worldType.Type == PlanetType.Rock))
            {
                Pressure = 0.001f;
                PressureCategory = AtmosphericPressureCategory.Trace;
                PressureClassFeltByHumans = AtmosphericPressureCategory.Trace;
            }
            else
            {
                int pressureFactor = GetPressureFactor(worldType);
                Pressure = Mass * pressureFactor * surfaceGravity;
                PressureCategory = DeterminePressureCategory();
                PressureClassFeltByHumans = DeterminePressureForHumans();
            }
        }

        private AtmosphericPressureCategory DeterminePressureForHumans()
        {
            if (MarginalAtmosphere is null)
                return PressureCategory;
            else if (MarginalAtmosphere is Enums.MarginalAtmosphere.HighCarbonDioxide)
                return AtmosphericPressureCategory.VeryDense;
            else if (MarginalAtmosphere is Enums.MarginalAtmosphere.HighOxygen)
            {
                switch (PressureCategory)
                {
                    case AtmosphericPressureCategory.Trace:
                        return AtmosphericPressureCategory.VeryThin;
                    case AtmosphericPressureCategory.VeryThin:
                        return AtmosphericPressureCategory.Thin;
                    case AtmosphericPressureCategory.Thin:
                        return AtmosphericPressureCategory.Standard;
                    case AtmosphericPressureCategory.Standard:
                        return AtmosphericPressureCategory.Dense;
                    case AtmosphericPressureCategory.Dense:
                        return AtmosphericPressureCategory.VeryDense;
                    default:
                        return AtmosphericPressureCategory.SuperDense;
                }
            }
            else if (MarginalAtmosphere is Enums.MarginalAtmosphere.LowOxygen)
            {
                switch (PressureCategory)
                {
                    case AtmosphericPressureCategory.VeryThin:
                        return AtmosphericPressureCategory.Trace;
                    case AtmosphericPressureCategory.Thin:
                        return AtmosphericPressureCategory.VeryThin;
                    case AtmosphericPressureCategory.Standard:
                        return AtmosphericPressureCategory.Thin;
                    case AtmosphericPressureCategory.Dense:
                        return AtmosphericPressureCategory.Standard;
                    case AtmosphericPressureCategory.VeryDense:
                        return AtmosphericPressureCategory.Dense;
                    case AtmosphericPressureCategory.SuperDense:
                        return AtmosphericPressureCategory.VeryDense;
                    default:
                        return AtmosphericPressureCategory.None;
                }
            }
            else
                return PressureCategory;
        }

        private AtmosphericPressureCategory DeterminePressureCategory() => Pressure switch
        {
            < 0.01f => AtmosphericPressureCategory.Trace,
            >= 0.01f and <= 0.5f => AtmosphericPressureCategory.VeryThin,
            > 0.5f and <= 0.8f => AtmosphericPressureCategory.Thin,
            > 0.8f and <= 1.2f => AtmosphericPressureCategory.Standard,
            > 1.2f and <= 1.5f => AtmosphericPressureCategory.Dense,
            > 1.5f and <= 10f => AtmosphericPressureCategory.VeryDense,
            > 10f => AtmosphericPressureCategory.SuperDense,
            _ => AtmosphericPressureCategory.None
        };

        private int GetPressureFactor(WorldTypeModel worldType)
        {
            if (worldType.Size == PlanetSize.Small && worldType.Type == PlanetType.Ice)
                return 10;
            if (worldType.Size == PlanetSize.Standard && worldType.Type == PlanetType.Greenhouse)
                return 100;
            if (worldType.Size == PlanetSize.Standard)
                return 1;
            if (worldType.Size == PlanetSize.Large && worldType.Type == PlanetType.Greenhouse)
                return 500;
            if (worldType.Size == PlanetSize.Large)
                return 5;
            else
                throw new Exception();
        }
    }
}
