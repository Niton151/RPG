using System;
using Game.DataBase.PlayerDataBase;
using Game.Scripts.Damage;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerCore : MonoBehaviour, IDamageApplicable
    {
        public PlayerType Type => _type;
        private PlayerType _type;
        
        public IReadOnlyReactiveProperty<float> HP => _hp;
        private FloatReactiveProperty _hp = new FloatReactiveProperty();
        
        public IReadOnlyReactiveProperty<float> MP => _mp;
        private FloatReactiveProperty _mp = new FloatReactiveProperty();
        
        public IReadOnlyReactiveProperty<float> SP => _sp;
        private FloatReactiveProperty _sp = new FloatReactiveProperty();
        
        public bool IsAlive { get; private set; }
        
        public IObservable<Unit> OnInitAsync => _onInitAsyncSubject;
        private AsyncSubject<Unit> _onInitAsyncSubject = new AsyncSubject<Unit>();

        public IObservable<Damage.Damage> OnDamage => _onDamageSubject;
        private Subject<Damage.Damage> _onDamageSubject = new Subject<Damage.Damage>();

        public IObservable<Damage.Damage> OnDead => _onDeadSubject;
        private Subject<Damage.Damage> _onDeadSubject = new Subject<Damage.Damage>();
        
        public PlayerParameters CurrentPlayerParameter => _currentPlayerParameter;
        private PlayerParameters _currentPlayerParameter;
        
        public PlayerInventory Inventory => _inventory;
        private PlayerInventory _inventory = new PlayerInventory();

        public PlayerEquipment Equipment => _equipment;
        private PlayerEquipment _equipment = new PlayerEquipment();

        public PlayerAttack Attacker => _attacker;
        private PlayerAttack _attacker;

        public PlayerCore()
        {
            _attacker = new PlayerAttack(this);
        }

        private void Awake()
        {
            _onInitAsyncSubject.Subscribe(_ =>
            {
                OnDamage
                    .Subscribe(x =>
                    {
                        _hp.Value -= x.Value;
                        if (_hp.Value <= 0)
                        {
                            _onDeadSubject.OnNext(x);
                        }
                    }).AddTo(this);

                OnDead
                    .Subscribe(_ =>
                    {
                        IsAlive = false;
                    }).AddTo(this);
            }).AddTo(this);
        }

        public void Init(PlayerJobData data)
        {
            _hp.Value = data.parameters.MaxHP;
            _mp.Value = data.parameters.MaxMP;
            _sp.Value = data.parameters.MaxSP;

            _currentPlayerParameter = data.parameters;
            IsAlive = true;

            _onInitAsyncSubject.OnNext(Unit.Default);
            _onInitAsyncSubject.OnCompleted();
        }
        
        public void SetPlayerParameter(PlayerParameters playerParameter)
        {
            _currentPlayerParameter = playerParameter;
        }

        public void SetXP(PlayerXPType healType, float value)
        {
            switch (healType)
            {
                case PlayerXPType.HP :
                    _hp.Value += value;
                    break;
                case PlayerXPType.MP :
                    _mp.Value += value;
                    break;
                case PlayerXPType.SP :
                    _sp.Value += value;
                    break;
            }
        }

        public void ApplyDamage(Damage.Damage damage)
        {
            _onDamageSubject.OnNext(damage);
        }

        private void OnDestroy()
        {
            _onDamageSubject.Dispose();
        }
    }
}