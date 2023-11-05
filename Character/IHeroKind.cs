using BigBattle.Equipment;

namespace BigBattle.Character
{
    /// <summary>
    /// Determines the kind of hero
    /// </summary>
    public interface IHeroKind
    {
        /// <summary>
        /// A flag that indicates if the hero kind can unequip its equipment, if has any.
        /// </summary>
        public bool CanUnequip { get; }

        /// <summary>
        /// Determines the amount of damage made by a hero
        /// </summary>
        /// <param name="equipment">The equipment used by the hero, if any</param>
        /// <returns>The amount of damage the hero will made.</returns>
        public int DetermineDamage(IEquipment? equipment);

        /// <summary>
        /// Called when the hero changes its kind. It can transform the hero.
        /// </summary>
        /// <param name="hero">The hero that changed its kind</param>
        public void OnTransform(Hero hero);
    }
}