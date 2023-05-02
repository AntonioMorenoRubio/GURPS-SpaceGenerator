using GeneratorLibrary;

Console.WriteLine("GURPS Planet & Star System Generator");
Console.WriteLine("=============================================================");
Console.WriteLine();


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

Console.WriteLine();

Console.WriteLine("Execution complete. Press ENTER to exit program.");
Console.ReadLine();