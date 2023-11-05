using BigBattle.Character;

namespace BigBattle.Match
{
    public class Team
    {
        private readonly List<BaseCharacter> m_Members;

        /// <summary>
        /// The name of the team
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Indicates if the team is alive.
        /// </summary>
        public bool IsAlive => m_Members.Any(x => x.IsAlive);

        /// <summary>
        /// The list of members in the team
        /// </summary>
        public IReadOnlyCollection<BaseCharacter> Members => m_Members.AsReadOnly();

        public Team(string name, List<BaseCharacter> members)
        {
            m_Members = members;
            Name = name;
        }

        public override string ToString() => $"{Name} (miembros vivos: {Members.Where(x => x.IsAlive).Count()})";
    }
}