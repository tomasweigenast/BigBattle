using BigBattle.Equipment;

namespace BigBattle.Game
{
    /// <summary>
    /// Initial entry point of the app. Game is a singleton that contains the game state.
    /// </summary>
    public class Game
    {
        public static readonly Game GameInstance = new();

        private readonly List<IEquipment> m_AvailableEquipments;
        private readonly Team[] m_Teams;
        private readonly DateTime m_StartedAt = DateTime.Now;
        private readonly Random m_Random = new();

        private int m_CurrentTurn = 1;
        private int m_Attacker = 0; // the team index if the m_Teams array
        private int m_Rival = 1; // the team index if the m_Teams array


        private Game()
        {
            m_AvailableEquipments = new List<IEquipment>{
                new Spell("Magia Negra"),
                new Spell("Avada Kedavra"),
                new Spell("Wingardium Leviosa"),
                new Weapon("Cuchillo", 5),
                new Weapon("Espada", 12),
                new Weapon("Mosquete", 20),
                new Weapon("Arco", 15),
            };
            m_Teams = new Team[2];
        }

        /// <summary>
        /// Returns a list of all the available equipments in the game
        /// </summary>
        public IReadOnlyCollection<IEquipment> AvailableEquipments => m_AvailableEquipments.AsReadOnly();

        /// <summary>
        /// The list of teams that are currently playing
        /// </summary>
        public IEnumerable<Team> Teams => m_Teams;

        /// <summary>
        /// The current game turn
        /// </summary>
        public int CurrentTurn => m_CurrentTurn;

        /// <summary>
        /// A flag that indicates if we are playing
        /// </summary>
        public bool IsPlaying => m_Teams.All(x => x.IsAlive);

        /// <summary>
        /// Called on every game tick
        /// </summary>
        public void Tick()
        {
            // if we finished playing, do nothing
            if (!IsPlaying)
            {
                var winner = m_Teams.First(x => x.IsAlive);
                var now = DateTime.Now;

                Console.WriteLine($"El juego terminó (Duración de la partida: {now - m_StartedAt})");
                Console.WriteLine($"Ganador: {winner.Name}");
                Console.WriteLine($"Miembros vivos: {winner.Members.Where(x => x.IsAlive).Count()}");
            }

            Team attackerTeam = m_Teams[m_Attacker]!;
            Team rivalTeam = m_Teams[m_Rival]!;

            Console.WriteLine($"Turno {m_CurrentTurn}");
            Console.WriteLine($"Ataca [{attackerTeam}] Recibe [{rivalTeam}]");

            foreach (var attacker in attackerTeam.Members.Where(x => x.IsAlive))
            {
                var aliveRivals = rivalTeam.Members.Where(x => x.IsAlive).ToList();

                // get random rival to attack
                var rival = aliveRivals[m_Random.Next(aliveRivals.Count)]!;
                int rivalHealth = rival.Health;
                attacker.Attack(rival);

                int damageGiven = rivalHealth - rival.Health;

                Console.WriteLine($"{attacker} attacks {rival} for {damageGiven} damage.");
            }


            // swap teams
            (m_Rival, m_Attacker) = (m_Attacker, m_Rival);
            m_CurrentTurn++;
            Console.WriteLine("=============================================");
        }
    }
}