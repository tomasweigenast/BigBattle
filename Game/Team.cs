using BigBattle.Character;

namespace BigBattle.Game
{
    public class Team
    {
        private readonly List<BaseCharacter> m_Members;

        /// <summary>
        /// The name of the team
        /// </summary>
        public string Name { get; }

        public bool IsAlive => m_Members.Any(x => x.IsAlive);

        public IReadOnlyCollection<BaseCharacter> Members => m_Members.AsReadOnly();

        public Team(string name, List<BaseCharacter> members)
        {
            m_Members = members;
            Name = name;
        }

        public override string ToString() => $"{Name} (miembros vivos: {Members.Where(x => x.IsAlive).Count()})";
    }
}