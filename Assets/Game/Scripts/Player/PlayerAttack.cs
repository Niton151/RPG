using Game.Scripts.Damage;
using UniRx;
using UniRx.Triggers;

namespace Game.Scripts.Player
{
    public class PlayerAttack : IAttacker
    {
        public PlayerCore PlayerCore => _core;
        private PlayerCore _core;

        public PlayerAttack(PlayerCore core)
        {
            _core = core;
        }

        public void Attack(IDamageApplicable target)
        {
            var totalATK = _core.CurrentPlayerParameter.ATK + _core.Equipment.CurrentWeapon?.WeaponData.ATK ?? _core.CurrentPlayerParameter.ATK;
            target.ApplyDamage(new Damage.Damage(this, totalATK));
        }
    }
}
