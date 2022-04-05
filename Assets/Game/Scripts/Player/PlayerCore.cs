using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
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
        
        public PlayerParameters CurrentPlayerParameter => _currentPlayerParameter;
        public bool IsAlive { get; private set; }
        public IObservable<Damage> OnDamage => _onDamageSubject;
        private Subject<Damage> _onDamageSubject = new Subject<Damage>();

        public IObservable<Damage> OnDead => _onDeadSubject;
        private Subject<Damage> _onDeadSubject = new Subject<Damage>();
        
        private PlayerParameters _currentPlayerParameter;
        
        public void Init(PlayerParameters playerParameters)
        {
            _hp.Value = playerParameters.MaxHP;
            _mp.Value = playerParameters.MaxMP;
            _sp.Value = playerParameters.MaxSP;

            _currentPlayerParameter = playerParameters;
            IsAlive = true;
            
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
        }
        
        public void SetPlayerParameter(PlayerParameters playerParameter)
        {
            _currentPlayerParameter = playerParameter;
        }

        public void ApplyDamage(Damage damage)
        {
            _onDamageSubject.OnNext(damage);
        }

        private void OnDestroy()
        {
            _onDamageSubject.Dispose();
        }
    }
}