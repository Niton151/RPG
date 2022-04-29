using System;
using Game.DataBase.ItemDataBase;
using Game.Scripts.Damage;
using Game.Scripts.Enemy;
using Game.Scripts.Player;
using UniRx;
using UniRx.Diagnostics;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Scripts.Equipment.Weapons
{
    public class Sword : WeaponBase
    {
        public override void Init(BaseItemData data)
        {
            base.Init(data);
            EnemyBase enemy = null;
            this.OnCollisionEnterAsObservable()
                .Where(x => x.gameObject.TryGetComponent(out enemy))
                .Subscribe(_ =>
                {
                    _onHitSubject.OnNext(enemy);
                }).AddTo(this);
        }
    }
}
