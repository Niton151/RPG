using System;
using Game.Scripts.Damage;
using Game.Scripts.Damage.AbState;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

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
            var weapon = _core.Equipment.CurrentWeapon;
            float totalATK = _core.CurrentParameter.STR.ModifiedValue;
            IAbState abState = null;
            if (weapon != null)
            {
                if(weapon.WeaponData.AbState != null)
                    abState = (IAbState) Activator.CreateInstance(weapon.WeaponData.AbState.GetType(), target);
                totalATK += weapon.WeaponData.ATK;
            }
            
            target.ApplyDamage
            (
                new Damage.Damage(
                    this,
                    totalATK,
                    abState
                )
            );
        }
    }
}