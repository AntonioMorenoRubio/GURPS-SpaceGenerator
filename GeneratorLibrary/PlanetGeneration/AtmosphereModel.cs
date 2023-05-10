using GeneratorLibrary.PlanetGeneration.Enums;

namespace GeneratorLibrary.PlanetGeneration
{
    public class AtmosphereModel
    {
        public float Pressure { get; set; }
        public AtmosphericPressureCategory PressureClassification { get; set; }
        public AtmosphericPressureCategory EffectivePressureForLife { get; set; }
        public float Mass { get; set; }
        public MarginalAtmosphere? MarginalAtmosphere { get; set; } = null;
        public List<string> Composition { get; set; } = new List<string>();
        public List<AtmosphereCharacteristic> Characteristics { get; set; } = new List<AtmosphereCharacteristic>();

        public AtmosphereModel() { }


        /// <summary>
        /// Generates the basic values of an Atmosphere based on the World Type created in Step 2. (pp.78-81)
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
        }

        private void AddMarginalEffects()
        {
            (List<string> Composition, List<AtmosphereCharacteristic> Characteristics) marginal = GetMarginalCompositionAndCharacteristics();

            foreach (string compound in marginal.Composition)
                Composition.Add(compound);

            foreach (AtmosphereCharacteristic characteristic in marginal.Characteristics)
                Characteristics.Add(characteristic);
        }

        private float GenerateAtmosphericMass(WorldTypeModel worldType)
        {
            if (worldType.Type == PlanetType.AsteroidBelt ||
                worldType.Size == PlanetSize.Tiny ||
                worldType.Size == PlanetSize.Small && (worldType.Type == PlanetType.Hadean || worldType.Type == PlanetType.Rock) ||
                worldType.Size == PlanetSize.Standard && (worldType.Type == PlanetType.Hadean || worldType.Type == PlanetType.Chthonian) ||
                worldType.Size == PlanetSize.Large && worldType.Type == PlanetType.Chthonian)
            {
                return 0f;
            }

            float baseMass = DiceRoller.BasicRoll() % 10f;
            float variation = Random.Shared.NextSingle(-0.05f, 0.05f);

            return baseMass + variation;
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
            (PlanetSize.Large, PlanetType.Ammonia) => new List<string> { "Helium gas", "Ammonia", "Methane" },
            (PlanetSize.Standard, PlanetType.Ice) or (PlanetSize.Standard, PlanetType.Ocean) => new List<string> { "Nitrogen", "Carbon Dioxide" },
            (PlanetSize.Large, PlanetType.Ice) or (PlanetSize.Large, PlanetType.Ocean) => new List<string> { "Helium gas", "Nitrogen gas" },
            (PlanetSize.Standard, PlanetType.Garden) => new List<string> { "Nitrogen", "Oxygen" },
            (PlanetSize.Large, PlanetType.Garden) => new List<string> { "Nitrogen", "Noble gases", "Oxygen" },
            (PlanetSize.Standard, PlanetType.Greenhouse) or (PlanetSize.Large, PlanetType.Greenhouse) => new List<string> { "Carbon Dioxide", "Nitrogen" },
            _ => throw new ArgumentException($"Combination of planet size {worldType.Size} and type {worldType.Type} not found. Could not determine atmospheric composition.")
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
            _ => throw new ArgumentException($"Combination of planet size {worldType.Size} and type {worldType.Type} not found. Could not determine atmosphere characteristics.")
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
                case Enums.MarginalAtmosphere.HighOxygen:
                    if (PressureClassification != AtmosphericPressureCategory.SuperDense)
                        EffectivePressureForLife = PressureClassification + 1;
                    break;
                case Enums.MarginalAtmosphere.LowOxygen:
                    if (PressureClassification != AtmosphericPressureCategory.Trace)
                        EffectivePressureForLife = PressureClassification - 1;
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


    }
}
