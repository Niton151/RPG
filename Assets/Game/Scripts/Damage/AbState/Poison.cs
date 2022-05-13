using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Damage.AbState
{
    public class Poison : IAbState, IAttacker
    {
        private float _duration = 15;
        private float _damage;
        private float _span = 1;
        
        private IDamageApplicable _target;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public Poison(IDamageApplicable target)
        {
            _target = target;
            _damage = _target.CurrentParameter.MaxHP.BaseValue / 25;
        }

        public async void Activate()
        {
            await UniTask.WhenAny(
                UniTask.Delay(TimeSpan.FromSeconds(_duration)),
                UniTaskAsyncEnumerable
                    .Timer(TimeSpan.Zero, TimeSpan.FromSeconds(_span))
                    .ForEachAsync(_ =>
                    {
                        if (_target.CurrentParameter.HP.Value > _damage)
                        {
                            Attack(_target);
                        }
                    }, cancellationToken: _cts.Token)
            );
            Dispose();
        }

        public void Attack(IDamageApplicable target)
        {
            target.ApplyDamage(new Damage(this, _damage));
        }
        
        public void Dispose()
        {
            _cts?.Cancel();
        }
    }
}
