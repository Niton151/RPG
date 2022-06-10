using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.Player;
using Sirenix.Serialization;

namespace Game.Scripts.Quest
{
    public class QuestFlow
    {
        [field: OdinSerialize] public List<QuestBase> Quests { get; } = new List<QuestBase>();

        public int Progress { get; private set; }
        public bool IsFlowClear { get; private set; }

        public async void StartFlow(PlayerCore player)
        {
            Progress = 0;
            
            while (Progress < Quests.Count)
            {
                Quests[Progress].Begin(player);
                await Quests[Progress].OnClear.ToUniTask();
                Progress++;
            }

            IsFlowClear = true;
        }
    }
}