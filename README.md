# Introduction
This program is a straightforward text-based game that illustrates fundamental programming principles in C#. Players navigate a spaceship through a galaxy, interact with different alien species, gather resources, and aim to reach a far-off planet.

# Code Introduction
In the first part of the code, we create the GameEntity class, where we represent the health and attack specifications of the spaceship and the aliens. The SpaceShip class adds specific attributes such as Fuel, ShieldStrength, and CargoCapacity, and includes the GotDamage method to handle damage received, applying it first to the shield.

On the other hand, the Alien class inherits from GameEntity and has specific attributes.

For the game's resources, we have subclasses FuelCell, SpaceMineral, and AncientArtifact.

For combat, we created the methods LaserAttack, GotDamage, and CollectResource, where all interactions are text-based. Each enemy has a name, health, and the damage it deals.



# Credit
Haoxi Dong (2343873) 

Narda Limon Lagunas (2342884)
