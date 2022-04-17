namespace Game.Scripts.Damage
{
    public readonly struct Damage
    {
        public Damage(IAttacker attacker, float value)
        {
            Value = value;
            Attacker = attacker;
        }

        public float Value { get; }
        public IAttacker Attacker { get; }
    }
}
