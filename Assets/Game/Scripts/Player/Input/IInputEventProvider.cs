using System;
using UniRx;

namespace Game.Scripts.Player.Input
{
    public interface IInputEventProvider
    {
        IObservable<Unit> OnAction { get; }
    }
}
