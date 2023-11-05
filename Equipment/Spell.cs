namespace BigBattle.Equipment
{
    public class Spell : IEquipment
    {
        public string Name { get; }

        public int Damage => 20;

        public Spell(string name)
        {
            Name = name;
        }

        public override string ToString() => $"Spell [{Name}]";
    }
}