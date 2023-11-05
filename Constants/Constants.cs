using BigBattle.Equipment;

namespace BigBattle.Constants
{
    public static class Constants
    {
        /// <summary>
        /// The default weapon monsters have when they do not have any other equipment
        /// </summary>
        public static readonly IEquipment DEFAULT_MONSTER_EQUIPMENT = new Weapon("Monster Equipment", 1);
    }
}