using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fungus;
using Game.DataBase.CharacterDataBase;
using Game.Scripts.Player;
using Game.Scripts.Quest;
using Game.Scripts.Utility;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UniRx;
using UniRx.Diagnostics;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Scripts.NPC
{
    public class NpcCore : SerializedMonoBehaviour
    {
        [OdinSerialize] private string _message;
        [OdinSerialize] private List<QuestBase> _questFlow = new List<QuestBase>();
        private bool _isTalking = false;
        public bool IsTalking => _isTalking;
        private PlayerCore core;

        private Flowchart _flowchart;
        public void Init(BaseNpcData data)
        {
            _flowchart = GetComponent<Flowchart>();

            this.OnTriggerStayAsObservable()
                .Where(_ => Config.InputProvider.OnAction())
                .Where(_ => TryGetComponent(out core))
                .Subscribe(_ =>
                {
                    Debug.Log("Talk subscribe");
                    Talk();
                })
                .AddTo(this);
        }
        
        private async void Talk()
        {
            if (_isTalking)
            {
                Debug.Log("talking");
                return;
            }

            _isTalking = true;
        
            Debug.Log("talk");
            _flowchart.SendFungusMessage(_message);
            await UniTask.WaitUntil(() => _flowchart.GetExecutingBlocks().Count == 0);

            _isTalking = false;
        }

        public void OrderQuest()
        {
            core.SetQuestFlow(_questFlow);
        }
    }
}
