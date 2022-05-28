using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Scripts.Player.Input
{
    public class KeyboardInput : MonoBehaviour, IInputEventProvider
    {
        public IObservable<Unit> OnAction => _onActionSubject;
        private Subject<Unit> _onActionSubject = new Subject<Unit>();

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => UnityEngine.Input.GetKeyDown(KeyCode.E))
                .Subscribe(_ => _onActionSubject.OnNext(Unit.Default))
                .AddTo(this);
        }
    }
}
