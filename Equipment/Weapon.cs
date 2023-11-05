namespace BigBattle.Equipment
{
    public class Weapon : IEquipment
    {
        public int Damage { get; }

        public string Name { get; }

        public Weapon(string name, int damage)
        {
            Name = name;
            Damage = damage;
        }

        public override string ToString() => $"Weapon [{Name}] Damage [{Damage}]";
    }
}