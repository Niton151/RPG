using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.DataBase.EnemyDataBase;
using Game.DataBase.ItemDataBase;
using Game.Scripts.Damage;
using Game.Scripts.Damage.AbState;
using Game.Scripts.Manager;
using Game.Scripts.Player;
using UniRx;
using UnityEngine;
using static Game.Scripts.Utility.Util;

namespace Game.Scripts.Enemy
{
    public abstract class EnemyBase : MonoBehaviour, IAttacker, IDamageApplicable
    {
        public BaseEnemyData Data { get; private set; }
        public IObservable<Damage.Damage> OnDead => _onDeadSubject;
        private Subject<Damage.Damage> _onDeadSubject = new Subject<Damage.Damage>();

        public IObservable<Damage.Damage> OnDamage => _onDamageSubject;
        private Subject<Damage.Damage> _onDamageSubject = new Subject<Damage.Damage>();

        private readonly AutoResetUniTaskCompletionSource _utsOnInit = AutoResetUniTaskCompletionSource.Create();

        public BaseParameter CurrentParameter => _currentParameter;
        private EnemyParameters _currentParameter;
        
        public Dictionary<Type, IAbState> AbStates => _abStates;
        private Dictionary<Type, IAbState> _abStates = new Dictionary<Type, IAbState>();

        private async void Awake()
        {
            await _utsOnInit.Task;

            _onDamageSubject
                .Subscribe(x =>
                {
                    _currentParameter.HP.Value -= x.Value;
                    
                    if (x.AbState != null)
                    {
                        if (!_abStates.ContainsKey(x.AbState.GetType()))
                        {
                            _abStates.Add(x.AbState.GetType(), x.AbState);
                        }
                        else
                        {
                            _abStates[x.AbState.GetType()].Dispose();
                            _abStates[x.AbState.GetType()] = x.AbState;
                        }

                        x.AbState.Activate();
                    }
                    
                    if (_currentParameter.HP.Value <= 0)
                    {
                        _onDeadSubject.OnNext(x);
                        _onDeadSubject.OnCompleted();
                    }
                }).AddTo(this);

            _onDeadSubject
                .Subscribe(x =>
                {
                    DropItem(x.Attacker);
                    DropExp(x.Attacker);
                }).AddTo(this);
        }

        public virtual void Init(BaseEnemyData data)
        {
            _onDamageSubject.AddTo(this);
            _onDeadSubject.AddTo(this);

            Data = data;
            _currentParameter = (EnemyParameters)data.parameters.Copy();
            _currentParameter.HP.Value = _currentParameter.MaxHP.BaseValue;
            _currentParameter.HP.AddTo(this);

            _utsOnInit.TrySetResult();
        }

        public void ApplyDamage(Damage.Damage damage)
        {
            _onDamageSubject.OnNext(damage);
        }

        public virtual void Attack(IDamageApplicable target)
        {
            target.ApplyDamage(new Damage.Damage(this, _currentParameter.STR.ModifiedValue));
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

        private void DropExp(IAttacker attacker)
        {
            if (attacker is PlayerAttack)
            {
                ((PlayerAttack)attacker).PlayerCore.Exp += _currentParameter.Exp;
            }
        }
    }
}
