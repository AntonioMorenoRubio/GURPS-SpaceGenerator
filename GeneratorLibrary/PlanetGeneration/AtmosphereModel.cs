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


    }
}
