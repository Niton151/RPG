using System;
using UniRx;

namespace Game.Scripts.Player.Input
{
    public class MockInput : IInputEventProvider
    {
        public IObservable<Unit> OnAction => _onActionSubject;
        private static Subject<Unit> _onActionSubject = new Subject<Unit>();

        public static void Action()
        {
            _onActionSubject.OnNext(Unit.Default);
        }
    }
}
