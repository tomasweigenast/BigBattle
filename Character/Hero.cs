using BigBattle.Equipment;

namespace BigBattle.Character
{
    public class Hero : BaseCharacter
    {
        public IHeroKind Kind { get; private set; }

        public Hero(string name, IHeroKind kind, int health = 100) : base(name, health)
        {
            Kind = kind;
        }

        public override void Attack(BaseCharacter target)
        {
            if (IsAlive && target.IsAlive)
            {
                int damage = Kind.DetermineDamage(Equipment);
                target.TakeDamage(damage);
            }
        }

        /// <summary>
        /// Changes the kind of hero
        /// </summary>
        /// <param name="kind">The kind to set.</param>
        public void ChangeKind(IHeroKind kind)
        {
            Kind = kind;
            Kind.OnTransform(this);
        }


        public override void Equip(IEquipment? equipment)
        {
            if (equipment == null && Kind.CanUnequip)
            {
                Equipment = null;
                return;
            }

            Equipment = equipment;
        }

        public override string ToString() => $"Hero {Kind} {Name}: {Health} HP";

    }
}