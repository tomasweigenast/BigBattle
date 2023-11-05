using BigBattle.Equipment;

namespace BigBattle.Character
{
    /// <summary>
    /// A base character in the game
    /// </summary>
    public abstract class BaseCharacter
    {
        /// <summary>
        /// The character name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The character health
        /// </summary>
        public int Health { get; protected set; }

        /// <summary>
        /// Any equipment that the current character has.
        /// </summary>
        public IEquipment? Equipment { get; protected set; }

        /// <summary>
        /// A flag that indicates if the character is still alive
        /// </summary>
        public bool IsAlive => Health > 0;

        /// <summary>
        /// Initializes a new instance of Character
        /// </summary>
        /// <param name="name">The name of the character.</param>
        /// <param name="health">The initial health of the character.</param>
        public BaseCharacter(string name, int health)
        {
            Name = name;
            Health = health;
        }

        /// <summary>
        /// Takes damage
        /// </summary>
        /// <param name="damage">The amount of damage to take.</param>
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
                Health = 0;
        }

        /// <summary>
        /// Equips or unequips an equipment tool
        /// </summary>
        /// <param name="equipment">The equipment to equip, or null to unequip.</param>
        public abstract void Equip(IEquipment? equipment);

        /// <summary>
        /// Attacks another character
        /// </summary>
        /// <param name="target">The character to attack</param>
        public abstract void Attack(BaseCharacter target);
    }
}