public abstract class GameEntity
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int AttackPower { get; set; }

    public abstract void LaserAttack(GameEntity target);

    public abstract void GotDamage(int amount);
}
    
public class SpaceShip : GameEntity
{
    public int Fuel { get; set; }
    public int ShieldStrength { get; set; }
    public int CargoCapacity { get; set; }
    public int CurrentX { get; set; }
    public int CurrentY { get; set; }

    public SpaceShip()
    {
        Name = "Player's Ship";
        Health = 100;
        AttackPower = 20;
        Fuel = 100;
        ShieldStrength = 50;
        CargoCapacity = 10;

        CurrentX = 1;
        CurrentY = 1;
    }

    public override void GotDamage(int amount) // 100
    {
        if (ShieldStrength > 0) // 50
        {
            int damage = Math.Min(ShieldStrength, amount); // 50 vs 100
            ShieldStrength -= damage; // 50 - 50
            amount -= damage; // 100 - 50
            Console.WriteLine($"The spaceship recived {amount} damage. Remaining Shield Strength: {ShieldStrength}");
        }
        // 50 - > Health
        if (amount > 0)
        {
            Health -= amount;
            Console.WriteLine($"The spaceship recived {amount} damage. Remaining health: {Health}");
        }

        if (Health < 0)
        {
            Console.WriteLine("GAME OVER! The ship has been destroyed.");
        }
    }

    public override void LaserAttack(GameEntity target)
    {
        //target.LaserAttack(AttackPower);
    }
}

public class Alien : GameEntity
{
    public Alien(string name, int health, int attackPower)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
    }

    public override void GotDamage(int amount)
    {
        Health -= amount;
        Console.WriteLine($"{Name} took {amount} ponmts of damage. Remaining health: {Health}");

        if (Health < 0)
        {
            Console.WriteLine($"{Name} enemy has been beaten.");
        }
    }

    public override void LaserAttack(GameEntity target)
    {
        //target.LaserAttack(AttackPower);
    }
}

public class Martian : Alien
{
    public Martian() : base("Martian", 100, 10)
    {
        //
    }
}

public class Zogarian : Alien
{
    public Zogarian() : base("Zogarian", 100, 10)
    {
        //
    }
}

public class SpacePirate : Alien
{
    public SpacePirate() : base("Space Pirate", 100, 10)
    {
        //
    }
}

public abstract class Resource
{
    public string ResourceName { get; set; }
    public string Description { get; set; }
    public int ResourceNumber { get; set; }

    protected Resource(string resourceName, string description, int resourceNumber)
    {
        ResourceName = resourceName;
        Description = description;
        ResourceNumber = resourceNumber;
    }

    public abstract void Collect();
}

public class FuelCell : Resource
{
    public FuelCell(int resourceNumber) : base("Fuel Cell", "", resourceNumber) { }

    public override void Collect()
    {
        Console.WriteLine($"You have collectes a {ResourceName} Your ship's fuel level has increased.");
    }
}

public class SpaceMineral : Resource
{
    public SpaceMineral(int resourceNumber) : base("Space Mineral", "", resourceNumber) { }

    public override void Collect()
    {
        Console.WriteLine($"You have collected a {ResourceName}. This space mineral can be valuable for trade or upgrades.");
    }
}

public class AncientArtifact : Resource
{
    public AncientArtifact(int resourceNumber) : base("Ancient Artifact", "", resourceNumber) { }

    public override void Collect()
    {
        Console.WriteLine($"You have found a {ResourceName}. This is an ancient artifact, guard it with your life");
    }
}

public class Galaxey
{
    public List<Alien> Aliens { get; set; }
    public List<Resource> Resources { get; set; }
    public bool IsExplored { get; set; }

    public Galaxey()
    {
        Aliens = new List<Alien>();
        Resources = new List<Resource>();
        IsExplored = false;
    }

    public void AddAlien(Alien alien)
    {
        Aliens.Add( alien );
    }

    public void AddResource(Resource resource)
    {
        Resources.Add( resource );
    }

    public void Explore()
    {
        IsExplored = true;
    }

    public string GoToOtheerZone()
    {
        if (!IsExplored)
        {
            return "You've entered an unexplored area.";
        }

        var description = "You have arrived in a new sector of the galaxy.";
        if (Resources.Count > 0)
        {
            description += $"Resources found in this sector: {string.Join(", ", Resources.Select(r => r.ResourceName))}.";
        }
        if (Aliens.Count > 0)
        {
            description += $"Enemies in sight in this sector: {string.Join(", ", Aliens.Select(a => a.Name))}.";
        }
        if (Aliens.Count == 0 && Resources.Count == 0)
        {
            description += $"The sector seems empty.";
        }
        return description;
    }
}

public class Game
{
    public SpaceShip PlayerSpaceship { get; set; }
    public Galaxey[,] GalaxyMap { get; set; }
    public bool IsGameOver { get; private set; }

    public Game(int galaxySize)
    {
        PlayerSpaceship = new SpaceShip();
        GalaxyMap = new Galaxey[galaxySize, galaxySize];
        InitializeGalaxy();
        IsGameOver = false;
    }

    private void InitializeGalaxy()
    {
        for (int i = 0; i < GalaxyMap.GetLength(0); i++)
        {
            for (int j = 0; j < GalaxyMap.GetLength(1); j++)
            {
                GalaxyMap[i, j] = new Galaxey();
            }
        }
    }

    public void StartGame()
    {
        GameLoop();
    }

    public void GameLoop()
    {
        while (!IsGameOver)
        {
            DisplayGameStatus();
            string command = GetPlayerCommand();
            ProcessCommand(command);
            UpdateGameStatus();
        }
    }

    private void DisplayGameStatus()
    {
        Console.WriteLine("Ship Status:");
        Console.WriteLine($"Ship Name: {PlayerSpaceship.Name}");
        Console.WriteLine($"Ship Health: {PlayerSpaceship.Health}");
        Console.WriteLine($"Ship Attack Power: {PlayerSpaceship.AttackPower}");
        Console.WriteLine($"Ship Fuel: {PlayerSpaceship.Fuel}");
        Console.WriteLine($"Ship Shield Level: {PlayerSpaceship.ShieldStrength}");
        Console.WriteLine($"Ship Cargo Capacitty: {PlayerSpaceship.CargoCapacity}");
        Console.WriteLine();

        Galaxey currentLocation = GetCurrentLocation();

        Console.WriteLine($"Current Location: [{PlayerSpaceship.CurrentX},{PlayerSpaceship.CurrentY}]");
        //Console.WriteLine();
        //Console.WriteLine(currentLocation.GoToOtheerZone());
    }

    private Galaxey GetCurrentLocation()
    {
        int x = PlayerSpaceship.CurrentX;
        int y = PlayerSpaceship.CurrentY;
        return GalaxyMap[x, y];
    }

    private string GetPlayerCommand()
    {
        Console.ReadLine();
        return Console.ReadLine();
    }

    private void ProcessCommand(string command)
    {

    }

    private void UpdateGameStatus()
    {

    }

    private void EndGame()
    {
        IsGameOver = true;
        Console.WriteLine("GAME OVER!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("  __ _____  ___   ____  _____    _____ _  _  _____  __     _____  _____  _____  _____ ");
        Console.WriteLine(" ((  ||_// ||=|| ((     ||==     ||==  \\\\//  ||_// ||     ((   )) ||_//  ||==   ||_// ");
        Console.WriteLine(" \\_)) ||   || ||  \\\\__  ||___    ||___ //\\\\  ||    ||__|   \\\\_//  || \\\\  ||___  || \\\\ ");
        Console.WriteLine("");
        Console.WriteLine("                               Press any button to continue");
        Console.ReadKey();
        Console.Clear();

        Game game = new Game(5);
        game.StartGame();
    }
}