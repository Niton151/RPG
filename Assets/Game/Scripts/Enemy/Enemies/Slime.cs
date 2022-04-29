using System;
using Game.DataBase.EnemyDataBase;
using Game.Scripts.Damage;
using Game.Scripts.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Scripts.Enemy.Enemies
{
    public class Slime : EnemyBase
    {
        public IObservable<IDamageApplicable> OnAttack => _onAttackSubject;
        private Subject<IDamageApplicable> _onAttackSubject = new Subject<IDamageApplicable>();

        public override void Init(BaseEnemyData data)
        {
            base.Init(data);

            _onAttackSubject.AddTo(this);

            PlayerCore player = null;
            this.OnCollisionEnterAsObservable()
                .Where(x => x.gameObject.TryGetComponent(out player))
                .Subscribe(_ => _onAttackSubject.OnNext(player)).AddTo(this);

            _onAttackSubject.Subscribe(Attack);
        }
    }
}
