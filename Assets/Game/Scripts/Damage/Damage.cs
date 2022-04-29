namespace Game.Scripts.Damage
{
    public readonly struct Damage
    {
        public Damage(IAttacker attacker, float value, Element element = Element.Nomal)
        {
            Value = value;
            Attacker = attacker;
            Element = element;
        }

        public float Value { get; }
        public IAttacker Attacker { get; }
        public Element Element { get; }
    }
}
