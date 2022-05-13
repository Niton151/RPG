using System;
using System.Collections;
using System.Collections.Generic;
using Game.DataBase.EnemyDataBase;
using Game.Scripts.Damage;
using Game.Scripts.Damage.AbState;
using Game.Scripts.Enemy;
using Game.Scripts.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Spider : EnemyBase
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

    public override void Attack(IDamageApplicable target)
    {
        target.ApplyDamage(new Damage(this, CurrentParameter.STR.ModifiedValue, new Poison(target)));
    }
}
