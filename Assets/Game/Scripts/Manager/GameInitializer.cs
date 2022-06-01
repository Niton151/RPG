using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Player;
using Game.Scripts.Utility;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Manager
{
    public class GameInitializer
    {
        public IObservable<Unit> OnInitFinished => _onInitFinishedSubject;
        private AsyncSubject<Unit> _onInitFinishedSubject = new AsyncSubject<Unit>();

        private PlayerCore _core;
        
        public PlayerCore Initialize(Config config)
        {
            config.Init();
            _core = PlayerProvider.Create(config.initPlayerType, Vector3.zero);
            _onInitFinishedSubject.OnNext(Unit.Default);
            _onInitFinishedSubject.OnCompleted();
            return _core;
        }
    }
}
