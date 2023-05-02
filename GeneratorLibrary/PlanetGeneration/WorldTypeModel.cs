namespace GeneratorLibrary
{
    public class WorldTypeModel
    {
        public PlanetSize Size { get; set; }
        public PlanetType Type { get; set; }

        public WorldTypeModel() { }

        public WorldTypeModel(PlanetType? type, PlanetSize? size)
        {
            Type = type?? GenerateOverallType();
            Size = size?? GenerateValidSizeForType();
        }

        public PlanetType GenerateOverallType()
        {
            throw new NotImplementedException();
        }

        private PlanetSize GenerateValidSizeForType()
        {
            throw new NotImplementedException();
        }
    }
}