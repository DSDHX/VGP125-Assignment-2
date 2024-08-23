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

    public override void GotDamage(int amount) // 100
    {
        if (ShieldStrength > 0) // 50
        {
            int damage = Math.Min(ShieldStrength, amount); // 50 vs 100
            ShieldStrength -= damage; // 50 - 50
            amount -= damage; // 100 - 50
            Console.WriteLine($"");
        }
        // 50 - > Health
        if (amount > 0)
        {
            Health -= amount;
            Console.WriteLine($"");
        }

        if (Health < 0)
        {
            Console.WriteLine("GAME OVER!");
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
        Console.WriteLine($"");

        if (Health < 0)
        {
            Console.WriteLine($"");
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

    protected Resource(string resourceName)
    {
        ResourceName = resourceName;
    }

    public abstract void Collect();
}

public class FuelCell : Resource
{
    public FuelCell() : base("Fuel Cell") { }

    public override void Collect()
    {
        Console.WriteLine($"");
    }
}

public class SpaceMineral : Resource
{
    public SpaceMineral() : base("Space Mineral") { }

    public override void Collect()
    {
        Console.WriteLine($"");
    }
}

public class AncientArtifact : Resource
{
    public AncientArtifact() : base("Ancient Artifact") { }

    public override void Collect()
    {
        Console.WriteLine($"");
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
            return "";
        }

        var description = "";
        if (Resources.Count > 0)
        {
            description += $"";
        }
        if (Aliens.Count > 0)
        {
            description += $"";
        }
        if (Aliens.Count == 0 && Resources.Count == 0)
        {
            description += $"";
        }
        return description;
    }
}

public class Game
{

}

class Program
{
    static void Main(string[] args)
    {

    }
}