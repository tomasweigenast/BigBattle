using BigBattle.Character;
using BigBattle.Equipment;

namespace BigBattle.Match
{
    /// <summary>
    /// Initial entry point of the app. Game is a singleton that contains the game state.
    /// </summary>
    public class Game
    {
        private static Game? m_GameSingleton;

        private readonly List<IEquipment> m_AvailableEquipments;
        private readonly List<IEquipment> m_DroppedEquipments;
        private readonly IEquipment m_DefaultMonsterEquipment;
        private readonly Team[] m_Teams;
        private readonly DateTime m_StartedAt = DateTime.Now;
        private readonly Random m_Random = new();

        private int m_CurrentTurn = 1;
        private int m_Attacker = 0; // the team index if the m_Teams array
        private int m_Rival = 1; // the team index if the m_Teams array

        public Game(List<IEquipment> availableEquipments, Team[] teams)
        {
            if (m_GameSingleton != null) throw new Exception("There is already a Game.");

            if (!(availableEquipments.Any(x => x is Spell) && availableEquipments.Any(x => x is Weapon))) throw new ArgumentException("Expected at least one Spell and one Weapon", nameof(availableEquipments));
            if (teams.Length != 2) throw new ArgumentException("Expected exactly two teams", nameof(teams));

            m_AvailableEquipments = availableEquipments;
            m_Teams = teams;
            m_DefaultMonsterEquipment = new Weapon("Monster Equipment", 1);
            m_GameSingleton = this;
            m_DroppedEquipments = new();
        }

        /// <summary>
        /// Returns a list of all the available equipments in the game
        /// </summary>
        public static IReadOnlyCollection<IEquipment> AvailableEquipments => m_GameSingleton!.m_AvailableEquipments.AsReadOnly();

        /// <summary>
        /// The default equipment of a monster when it does not have any other
        /// </summary>
        public static IEquipment DefaultMonsterEquipment => m_GameSingleton!.m_DefaultMonsterEquipment;

        /// <summary>
        /// The list of teams that are currently playing
        /// </summary>
        public static IEnumerable<Team> Teams => m_GameSingleton!.m_Teams;

        /// <summary>
        /// A flag that indicates if we are playing
        /// </summary>
        public bool IsPlaying => m_Teams.All(x => x.IsAlive);

        /// <summary>
        /// Called on every game tick
        /// </summary>
        public void OnTick()
        {
            Team attackerTeam = m_Teams[m_Attacker]!;
            Team rivalTeam = m_Teams[m_Rival]!;

            Console.WriteLine($"Turno {m_CurrentTurn}");
            Console.WriteLine($"Ataca [{attackerTeam}] Recibe [{rivalTeam}]");

            if (m_DroppedEquipments.Count > 0)
            {
                var nextDrops = new List<IEquipment>();
                foreach (var hero in m_Teams.SelectMany(x => x.Members.Where(b => b.IsAlive && b is Hero)))
                {
                    var randomDrop = m_DroppedEquipments[m_Random.Next(m_DroppedEquipments.Count)];

                    // drop its equipment first but for the next tick
                    if (hero.Equipment != null)
                        nextDrops.Add(hero.Equipment);

                    hero.Equip(randomDrop);
                }

                m_DroppedEquipments.AddRange(nextDrops);
            }

            foreach (var attacker in attackerTeam.Members.Where(x => x.IsAlive))
            {
                var aliveRivals = rivalTeam.Members.Where(x => x.IsAlive).ToList();

                // get random rival to attack
                var rival = aliveRivals[m_Random.Next(aliveRivals.Count)]!;
                int rivalHealth = rival.Health;
                attacker.Attack(rival);

                int damageGiven = rivalHealth - rival.Health;

                Console.WriteLine($"({attacker}) attacks ({rival}) for {damageGiven} damage.");

                // if the rival died, drop its equipment so in the next tick a Hero can pick it up
                if (!rival.IsAlive && rival.Equipment != null && rival.Equipment != m_DefaultMonsterEquipment)
                {
                    rival.Equip(null); // just to be "safe"
                    m_DroppedEquipments.Add(rival.Equipment);
                }
            }


            // swap teams
            (m_Rival, m_Attacker) = (m_Attacker, m_Rival);
            m_CurrentTurn++;
            Console.WriteLine("=============================================");
        }

        /// <summary>
        /// Called when the game starts
        /// </summary>
        public void OnStart()
        {
            // this actually fakes content to match TP's requirements
            var allPlayers = m_Teams.SelectMany(x => x.Members).ToList();
            var hero = allPlayers.First(x => x is Hero);
            var monsters = allPlayers.Where(x => x is Monster).ToList();

            hero.Equip(m_AvailableEquipments.First(x => x.Damage == 5));
            monsters[0].Equip(m_AvailableEquipments.First(x => x.Damage == 3));
            monsters[1].Equip(m_AvailableEquipments.First(x => x.Damage == 10));
            monsters[2].Equip(m_AvailableEquipments.First(x => x.Damage == 5));
        }

        /// <summary>
        /// Called when the game ends
        /// </summary>
        public void OnEnd()
        {
            var winner = m_Teams.First(x => x.IsAlive);
            var now = DateTime.Now;

            Console.WriteLine($"El juego terminó (Duración de la partida: {now - m_StartedAt})");
            Console.WriteLine($"Ganador: {winner.Name}");
            Console.WriteLine($"Miembros vivos: {winner.Members.Where(x => x.IsAlive).Count()}");
        }

    }
}