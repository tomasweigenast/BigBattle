using BigBattle.Character;
using BigBattle.Match;
using BigBattle.Equipment;

namespace BigBattle
{
    public class Program
    {
        public static void Main()
        {
            // modify this with caution because it is adjusted to match Game.OnStart method
            var equipments = new List<IEquipment>{
            new Spell("Magia Negra"),
            new Spell("Avada Kedavra"),
            new Spell("Wingardium Leviosa"),
            new Weapon("Cuchillo", 3),
            new Weapon("Espada", 5),
            new Weapon("Mosquete", 10),
        };
            var teams = new Team[]{
            new("Team A", new List<BaseCharacter>{
                new Hero("Player", new Warrior())
            }),
            new("Team B", new List<BaseCharacter>{
                new Monster("Zombie A", 35),
                new Monster("Zombie B", 35),
                new Monster("Zombie C", 35),
            }),
        };
            var game = new Game(equipments, teams);
            game.OnStart();

            while (game.IsPlaying)
            {
                game.OnTick();
                Thread.Sleep(2000);
            }

            game.OnEnd();
        }
    }
}