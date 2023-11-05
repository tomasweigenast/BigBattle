using BigBattle.Equipment;
using static BigBattle.Constants.Constants;

namespace BigBattle.Character
{
    public class Monster : BaseCharacter
    {
        public Monster(string name, int health) : base(name, health) { }

        public override void Attack(BaseCharacter target)
        {
            if (IsAlive && target.IsAlive)
            {
                // monster will always have an equipment
                target.TakeDamage(Equipment!.Damage);
            }
        }

        public override void Equip(IEquipment? equipment)
        {
            if (equipment == null)
                Equipment = DEFAULT_MONSTER_EQUIPMENT;
        }

        public override string ToString() => $"Monster [{Name}] Health [{Health}] Equipment [{Equipment}]";
    }
}