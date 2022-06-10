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
        private string _message;
        private QuestFlow _questFlow;
        private bool _isTalking = false;
        public bool IsTalking => _isTalking;
        private PlayerCore core;

        private Flowchart _flowchart;
        public void Init(BaseNpcData data)
        {
            _flowchart = GetComponent<Flowchart>();
            _message = data.message;
            _questFlow = data.questFlow;

            this.OnTriggerStayAsObservable()
                .Where(_ => Config.InputProvider.OnAction())
                .Where(x => x.TryGetComponent(out core))
                .Subscribe(_ =>
                {
                    Talk();
                })
                .AddTo(this);
        }
        
        private async void Talk()
        {
            if (_isTalking)
            {
                return;
            }

            _isTalking = true;
            
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
