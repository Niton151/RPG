using System;
using Cysharp.Threading.Tasks;
using Game.DataBase.EnemyDataBase;
using Game.DataBase.ItemDataBase;
using Game.Scripts.Damage;
using Game.Scripts.Manager;
using Game.Scripts.Player;
using UniRx;
using UnityEngine;
using static Game.Scripts.Utility.Util;

namespace Game.Scripts.Enemy
{
    public abstract class EnemyBase : MonoBehaviour, IAttacker, IDamageApplicable
    {
        public ReactiveProperty<float> HP = new ReactiveProperty<float>();
        public BaseEnemyData Data { get; private set; }
        public IObservable<Damage.Damage> OnDead => _onDeadSubject;
        private Subject<Damage.Damage> _onDeadSubject = new Subject<Damage.Damage>();

        public IObservable<Damage.Damage> OnDamage => _onDamageSubject;
        private Subject<Damage.Damage> _onDamageSubject = new Subject<Damage.Damage>();

        private readonly AutoResetUniTaskCompletionSource _utsOnInit = AutoResetUniTaskCompletionSource.Create();

        private async void Awake()
        {
            await _utsOnInit.Task;

            _onDamageSubject
                .Subscribe(x =>
                {
                    HP.Value -= x.Value;
                    if (HP.Value <= 0)
                    {
                        _onDeadSubject.OnNext(x);
                        _onDeadSubject.OnCompleted();
                    }
                }).AddTo(this);

            _onDeadSubject
                .Subscribe(x =>
                {
                    DropItem(x.Attacker);
                }).AddTo(this);
        }

        public virtual void Init(BaseEnemyData data)
        {
            _onDamageSubject.AddTo(this);
            _onDeadSubject.AddTo(this);
            HP.AddTo(this);

            Data = data;
            HP.Value = Data.parameters.MaxHP;

            _utsOnInit.TrySetResult();
        }

        public void ApplyDamage(Damage.Damage damage)
        {
            _onDamageSubject.OnNext(damage);
        }

        public void Attack(IDamageApplicable target)
        {
            target.ApplyDamage(new Damage.Damage(this, Data.parameters.ATK));
        }

        private void DropItem(IAttacker attacker)
        {
            if (attacker is PlayerAttack)
            {
                var itemData = Raffle<BaseItemData>(Data.dropItems);
                var item = ItemProvider.Create(itemData.id, this.transform.position);
            
                item.PickedUp(((PlayerAttack)attacker).PlayerCore);   
            }
        }
    }
}
