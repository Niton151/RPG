using System;
using Cysharp.Threading.Tasks;
using Sirenix.Serialization;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Quest.Quests
{
    public class GotoQuest : QuestBase
    {
        public IObservable<Unit> OnClear => _onClearSubject;

        [OdinSerialize] private Vector3 _targetPos;
        [OdinSerialize] private GameObject _markerPrefab;
        private GameObject _marker;
        private Subject<Unit> _onClearSubject = new Subject<Unit>();

        public override void Begin()
        {
            _marker = Object.Instantiate(_markerPrefab, _targetPos, Quaternion.identity);
            _marker.OnTriggerEnterAsObservable()
                .Select(x => x.CompareTag("Player"))
                .First()
                .Subscribe(_ =>
                {
                    _onClearSubject.OnNext(Unit.Default);
                    _onClearSubject.OnCompleted();
                    Object.Destroy(_marker);
                });
        }
    }
}
