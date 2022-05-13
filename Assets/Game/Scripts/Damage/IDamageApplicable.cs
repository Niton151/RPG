using Game.Scripts.Player;

namespace Game.Scripts.Damage
{
    public interface IDamageApplicable
    {
        BaseParameter CurrentParameter { get;}
        void ApplyDamage(Damage damage);
    }
}
