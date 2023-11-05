using BigBattle.Equipment;

namespace BigBattle.Character
{
    public class Warrior : IHeroKind
    {
        public bool CanUnequip => true;

        public int DetermineDamage(IEquipment? equipment)
        {
            if (equipment == null)
                return 0;

            return equipment.Damage * 3;
        }

        public void OnTransform(Hero hero)
        {
            // do nothing
        }
        public override string ToString() => "Warrior";
    }
}