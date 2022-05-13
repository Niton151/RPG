using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Player;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Manager
{
    public class GameInitializer
    {
        public IObservable<Unit> OnInitFinished => _onInitFinishedSubject;
        private AsyncSubject<Unit> _onInitFinishedSubject = new AsyncSubject<Unit>();
        
        public void Initialize(PlayerType playerType = PlayerType.SwordMan)
        {
            PlayerProvider.Create(playerType, Vector3.zero);
            _onInitFinishedSubject.OnNext(Unit.Default);
            _onInitFinishedSubject.OnCompleted();
        }
    }
}
