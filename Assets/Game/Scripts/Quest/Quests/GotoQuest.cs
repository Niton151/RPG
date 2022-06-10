using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Player;
using Sirenix.Serialization;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Quest.Quests
{
    public class GotoQuest : QuestBase
    {
        [OdinSerialize] private Vector3 _targetPos;
        [OdinSerialize] private GameObject _markerPrefab;
        private GameObject _marker;

        public override void Begin(PlayerCore player)
        {
            onClearSubject = new Subject<Unit>();
            _marker = Object.Instantiate(_markerPrefab, _targetPos, Quaternion.identity);
            _marker.OnTriggerEnterAsObservable()
                .Select(x => x.CompareTag("Player"))
                .First()
                .Subscribe(_ =>
                {
                    onClearSubject.OnNext(Unit.Default);
                    onClearSubject.OnCompleted();
                }).AddTo(_marker);

            onClearSubject
                .Subscribe(_ =>
                {
                    IsClear = true;
                    GiveReward(player);
                }).AddTo(_marker);
        }
    }
}