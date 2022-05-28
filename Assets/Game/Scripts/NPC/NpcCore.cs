using System.Collections.Generic;
using Fungus;
using Game.DataBase.CharacterDataBase;
using Game.Scripts.Player;
using Game.Scripts.Quest;
using Sirenix.Serialization;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Scripts.NPC
{
    public class NpcCore : MonoBehaviour
    {
        [OdinSerialize] private string _message;
        [OdinSerialize]private List<QuestBase> _questFlow = new List<QuestBase>();
        private bool _isTalking = false;

        private Flowchart _flowchart;
        public void Init(BaseNpcData data)
        {
            _flowchart = GetComponent<Flowchart>();
            PlayerCore core = null;

            this.OnTriggerStayAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.E))
                .Where(_ => TryGetComponent(out core))
                .Subscribe(_ => core.SetQuestFlow(_questFlow))
                .AddTo(this);
        }
    }
}
