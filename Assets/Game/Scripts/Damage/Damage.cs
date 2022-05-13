using Game.Scripts.Damage.AbState;
using JetBrains.Annotations;

namespace Game.Scripts.Damage
{
    public readonly struct Damage
    {
        public Damage(IAttacker attacker, float value, IAbState abState = null)
        {
            Value = value;
            Attacker = attacker;
            AbState = abState;
        }

        public float Value { get; }
        public IAttacker Attacker { get; }
        [CanBeNull] public IAbState AbState { get; }
    }
}
