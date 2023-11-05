namespace BigBattle.Equipment
{
    /// <summary>
    /// A tool that can be equipped and takes damage from characters when used.
    /// </summary>
    public interface IEquipment
    {
        /// <summary>
        /// The name of the equipment tool
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The damage taken 
        /// </summary>
        public int Damage { get; }
    }
}