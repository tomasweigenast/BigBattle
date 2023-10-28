using System;
using System.Collections.Generic;

class Character
{
    public string Name { get; set; }
    public int Health { get; set; }
    public Weapon Weapon
    {
        get; set;
    }

    public Character(string name, int health, Weapon weapon)
    {
        Name = name;
        Health = health;
        Weapon = weapon;
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    public void Attack(Character target)
    {
        if (IsAlive() && target.IsAlive())
        {
            int damage = 0;
            if (this is Hero hero)
            {
                if (hero.Weapon != null)
                {
                    if (hero is Warrior)
                    {
                        damage = hero.Weapon.Damage * 3;
                    }
                    else if (hero is Wizard)
                    {
                        damage = 20;
                    }
                }
            }
            else if (this is Monster monster)
            {
                if (monster.Weapon != null)
                {
                    damage = monster.Weapon.Damage;
                }
                else
                {
                    damage = 1;
                }
            }

            target.TakeDamage(damage);
            Console.WriteLine($"({Name} {GetType().Name}: {Health} HP) attacks ({target.Name}: {target.Health} HP) for {damage} damage");
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }
}

class Hero : Character
{
    public Hero(string name, int health, Weapon weapon) : base(name, health, weapon)
    {
    }

    public void ChangeType<T>() where T : Hero
    {
        if (this is T) return;

        var newHero = (Hero)Activator.CreateInstance(typeof(T), Name, Health, Weapon)!;
        Name = newHero.Name;
        Health = newHero.Health;
        Weapon = newHero.Weapon;
    }
}

class Warrior : Hero
{
    public Warrior(string name, int health, Weapon weapon) : base(name, health, weapon)
    {
    }
}

class Wizard : Hero
{
    public Wizard(string name, int health, Weapon weapon) : base(name, health, weapon)
    {
    }
}

class Monster : Character
{
    public Monster(string name, int health, Weapon weapon) : base(name, health, weapon)
    {
    }
}

class Weapon
{
    public int Damage { get; set; }

    public Weapon(int damage)
    {
        Damage = damage;
    }
}

class Program
{
    static void Battle(List<Hero> heroes, List<Monster> monsters)
    {
        int turn = 1;
        var random = new Random();

        while (true)
        {
            Console.WriteLine($"Turn {turn}");

            foreach (var hero in heroes)
            {
                if (hero.IsAlive())
                {
                    var aliveMonsters = monsters.FindAll(monster => monster.IsAlive());
                    if (aliveMonsters.Count > 0)
                    {
                        int randomIndex = random.Next(0, aliveMonsters.Count);
                        var randomMonster = aliveMonsters[randomIndex];
                        hero.Attack(randomMonster);
                    }
                }
            }

            foreach (var monster in monsters)
            {
                if (monster.IsAlive())
                {
                    var aliveHeroes = heroes.FindAll(hero => hero.IsAlive());
                    if (aliveHeroes.Count > 0)
                    {
                        int randomIndex = random.Next(0, aliveHeroes.Count);
                        var randomHero = aliveHeroes[randomIndex];
                        monster.Attack(randomHero);
                    }
                }
            }

            bool heroesAlive = heroes.Exists(hero => hero.IsAlive());
            bool monstersAlive = monsters.Exists(monster => monster.IsAlive());

            if (!heroesAlive || !monstersAlive)
            {
                break;
            }

            turn++;
            Thread.Sleep(2000);
        }
    }

    static void Main(string[] args)
    {
        var hero = new Warrior("Player Warrior", 100, new Weapon(5));
        var monsters = new List<Monster>
        {
            new("Zombie 1", 35, new Weapon(3)),
            new("Zombie 2", 35, new Weapon(10)),
            new("Zombie 3", 35, new Weapon(5))
        };

        var heroes = new List<Hero> { hero };

        Battle(heroes, monsters);
    }
}
