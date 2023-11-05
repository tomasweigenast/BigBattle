using BigBattle.Match;
using BigBattle.Equipment;

namespace BigBattle.Character
{
    public class Wizard : IHeroKind
    {
        public bool CanUnequip => false;

        public int DetermineDamage(IEquipment? equipment) => equipment!.Damage; // wizards will always have an equipment

        public void OnTransform(Hero hero)
        {
            // TODO: equip with a new spell
            // this ensures the wizard will always have an equipment
            var spell = Game.AvailableEquipments.First(x => x is Spell);
            hero.Equip(spell);
        }

        public override string ToString() => "Wizard";
    }
}