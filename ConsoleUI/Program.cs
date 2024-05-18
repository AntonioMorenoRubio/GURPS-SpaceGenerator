using GeneratorLibrary.PlanetGeneration;
using GeneratorLibrary.PlanetGeneration.Enums;

Console.WriteLine("GURPS Planet & Star System Generator");
Console.WriteLine("=============================================================");
Console.WriteLine();

GenerateTestWorlds();
//GeneratePossibleTeegardenBs();

Console.WriteLine();

Console.WriteLine("Execution complete. Press ENTER to exit program.");
Console.ReadLine();

static void GenerateTestWorlds()
{
    Console.WriteLine("Creating Test planets");

    List<PlanetModel> planets = new List<PlanetModel>();

    for (int i = 0; i < 10; i++)
    {
        planets.Add(new PlanetModel($"Planet {i + 1}", "Test Description"));
    }

    Console.WriteLine("Planets generated.");
    Console.WriteLine();

    foreach (PlanetModel planet in planets)
    {
        Console.WriteLine(planet.ToString());
        Console.WriteLine();
    }
}

static void GeneratePossibleTeegardenBs()
{
    Console.WriteLine("Creating Test planets");

    List<PlanetModel> planets = new List<PlanetModel>();

    for (int i = 0; i < 10; i++)
    {
        planets.Add(new PlanetModel("Teegarden B", PlanetType.Garden, PlanetSize.Standard, "A planet 12 ly away from Earth"));
    }

    Console.WriteLine("Planets generated.");
    Console.WriteLine();

    foreach (PlanetModel planet in planets)
    {
        Console.WriteLine(planet.ToString());
        Console.WriteLine();
    }
}