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
        Aliens.Add(alien);
    }

    public void AddResource(Resource resource)
    {
        Resources.Add(resource);
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
        Console.Clear();
        Console.WriteLine("Ship Status:");
        Console.WriteLine($"Ship Name: {PlayerSpaceship.Name}");
        Console.WriteLine($"Ship Health: {PlayerSpaceship.Health}");
        Console.WriteLine($"Ship Attack Power: {PlayerSpaceship.AttackPower}");
        Console.WriteLine($"Ship Fuel: {PlayerSpaceship.Fuel}");
        Console.WriteLine($"Ship Shield Level: {PlayerSpaceship.ShieldStrength}");
        Console.WriteLine($"Ship Cargo Capacitty: {PlayerSpaceship.CargoCapacity}");
        Console.WriteLine();

        Console.WriteLine($"Current Location: [{PlayerSpaceship.CurrentX},{PlayerSpaceship.CurrentY}]");
        Console.WriteLine("Your target is [3,3]");
    }

    private Galaxey GetCurrentLocation()
    {
        int x = PlayerSpaceship.CurrentX;
        int y = PlayerSpaceship.CurrentY;
        return GalaxyMap[x, y];
    }

    private string GetPlayerCommand()
    {
        Console.WriteLine("");
        Console.WriteLine("Please select the direction of movement: ");
        Console.WriteLine("(Please type 'up'; 'down'; 'left'; 'right'");
        return Console.ReadLine();
    }

    private void ProcessCommand(string command)
    {
        switch(command.ToLower())
        {
            case "up":
                MovePlayer(0, -1);
                break;
            case "down":
                MovePlayer(0, 1);
                break;
            case "left":
                MovePlayer(-1, 0);
                break;
            case "right":
                MovePlayer(1, 0);
                break;
            default:
                Console.WriteLine("");
                Console.WriteLine("Invalid Input");
                Console.WriteLine("Press any button to continue");
                Console.ReadKey();
                break;
        }
    }

    private void MovePlayer(int MoveX, int MoveY)
    {
        int newX = PlayerSpaceship.CurrentX + MoveX;
        int newY = PlayerSpaceship.CurrentY + MoveY;

        if (newX >= 1 && newX < GalaxyMap.GetLength(1) && newY >= 1 && newY < GalaxyMap.GetLength(1))
        {   
            PlayerSpaceship.CurrentX = newX;
            PlayerSpaceship.CurrentY = newY;
            Console.WriteLine("");
            Console.WriteLine($"The ship has moved to the [{PlayerSpaceship.CurrentX},{PlayerSpaceship.CurrentY}]");
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("");
            Console.WriteLine("Unable to move to that location beyond the map!");
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
        }
        //Galaxey currentLocation = GetCurrentLocation();
        //Console.WriteLine(currentLocation.GoToOtheerZone());
    }

    private void UpdateGameStatus()
    {
        if (PlayerSpaceship.Fuel <= 0)
        {
            Console.WriteLine("Fuel depeletion!");
            EndGame();
            return;
        }

        if (PlayerSpaceship.Health <= 0)
        {
            Console.WriteLine("The ship has been destroyed.");
            EndGame();
            return;
        }

        Galaxey currentLocation = GetCurrentLocation();
        if (currentLocation.Aliens.Count > 0)
        {
            Console.WriteLine("You met aliens!");
            foreach (var alien in currentLocation.Aliens)
            {
                PlayerSpaceship.LaserAttack(alien);
                if (alien.Health > 0)
                {
                    alien.LaserAttack(PlayerSpaceship);
                }
            }

            currentLocation.Aliens.RemoveAll(alien => alien.Health <= 0);
        }

        if (currentLocation.Resources.Count > 0)
        {
            Console.WriteLine("You found the resources!");
            foreach (var resource in currentLocation.Resources)
            {
                resource.Collect();
                if (resource is FuelCell fuelCell)
                {
                    PlayerSpaceship.Fuel += fuelCell.ResourceNumber;
                }
                if (resource is SpaceMineral spaceMineral)
                {
                    PlayerSpaceship.ShieldStrength += spaceMineral.ResourceNumber;
                }
                if (resource is AncientArtifact ancientArtifact)
                {
                    PlayerSpaceship.CargoCapacity -= ancientArtifact.ResourceNumber;
                }

                currentLocation.Resources.Clear();
            }
        }
    }

    private void EndGame()
    {
        IsGameOver = true;
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

        //Game game = new Game(4); // size - 1
        //game.StartGame();
        TempGame game = new TempGame();
        game.StartGame();
    }
}

public class TempGame
{
    private bool IsGameOver = false;
    private string[,] map = new string[3, 3];
    private Player PlayerSpaceship = new Player();

    public TempGame()
    {
        map[0, 0] = "Start";
        map[0, 1] = "Alien";
        map[0, 2] = "Resource";
        map[1, 0] = "Resource";
        map[1, 1] = "Alien";
        map[1, 2] = "Resource";
        map[2, 0] = "Alien";
        map[2, 1] = "Resource";
        map[2, 2] = "Goal";
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
            CheckPointTemp(PlayerSpaceship.CurrentX, PlayerSpaceship.CurrentY);
            //UpdateGameStatus();
        }
    }

    private void CheckPointTemp(int x, int y)
    {
        string encounter = map[x, y];
        switch (encounter)
        {
            case "Alien":
                Console.WriteLine("");
                Console.WriteLine("You encountered an alien!");
                Console.WriteLine("You're under attacked! (-30 Health)");
                PlayerSpaceship.CostDamage(30);
                Console.WriteLine("Press any button to continue");
                Console.ReadKey();
                break;
            case "Resource":
                Console.WriteLine("");
                Console.WriteLine("You found a resource! (+30 Health; -1 Cargo Room)");
                PlayerSpaceship.CollectResource(30);
                Console.WriteLine("Press any button to continue");
                Console.ReadKey();
                break;
            case "Goal":
                Console.WriteLine("");
                Console.WriteLine("You reached the goal!");
                Console.WriteLine("Press any button to continue");
                Console.ReadKey();
                IsGameOver = true;
                break;
            default:
                Console.WriteLine("");
                Console.WriteLine("Nothing here.");
                Console.WriteLine("Press any button to continue");
                Console.ReadKey();
                break;
        }
    }

    private void DisplayGameStatus()
    {
        Console.Clear();
        Console.WriteLine("Ship Status:");
        Console.WriteLine($"Ship Name: {PlayerSpaceship.Name}");
        Console.WriteLine($"Ship Health: {PlayerSpaceship.Health}");
        //Console.WriteLine($"Ship Attack Power: {PlayerSpaceship.AttackPower}");
        //Console.WriteLine($"Ship Fuel: {PlayerSpaceship.Fuel}");
        //Console.WriteLine($"Ship Shield Level: {PlayerSpaceship.ShieldStrength}");
        Console.WriteLine($"Ship Cargo Capacitty: {PlayerSpaceship.CargoCapacity}");
        Console.WriteLine();

        Console.WriteLine($"Current Location: [{PlayerSpaceship.CurrentX},{PlayerSpaceship.CurrentY}]");
        Console.WriteLine("Your target is [2,2]");
    }

    private string GetPlayerCommand()
    {
        Console.WriteLine("");
        Console.WriteLine("Please select the direction of movement: ");
        Console.WriteLine("(Please type 'up'; 'down'; 'left'; 'right'");
        return Console.ReadLine();
    }

    private void ProcessCommand(string command)
    {
        switch (command.ToLower())
        {
            case "up":
                PlayerSpaceship.MoveUp();
                break;
            case "down":
                PlayerSpaceship.MoveDown();
                break;
            case "left":
                PlayerSpaceship.MoveLeft();
                break;
            case "right":
                PlayerSpaceship.MoveRight();
                break;
            default:
                Console.WriteLine("Invalid command.");
                break;
        }
    }
}

public class Player
{
    public int CurrentX { get; set; } = 0;
    public int CurrentY { get; set; } = 0;
    public string Name { get; set; }
    public int Health { get; set; }
    public int AttackPower { get; set; }
    public int Fuel { get; set; }
    public int ShieldStrength { get; set; }
    public int CargoCapacity { get; set; }

    public Player()
    {
        Name = "Player's Ship";
        Health = 100;
        AttackPower = 20;
        Fuel = 100;
        ShieldStrength = 50;
        CargoCapacity = 10;
    }

    public void MoveUp()
    {
        if (CurrentY > 0) CurrentY--;
    }

    public void MoveDown()
    {
        if (CurrentY < 2) CurrentY++;
    }

    public void MoveLeft()
    {
        if (CurrentX > 0) CurrentX--;
    }

    public void MoveRight()
    {
        if (CurrentX < 2) CurrentX++;
    }

    public void CostDamage(int damage)
    {
        Health -= damage;
    }

    public void CollectResource(int resourceNumber)
    {
        Health += resourceNumber;
        CargoCapacity = CargoCapacity - 1;
    }
}