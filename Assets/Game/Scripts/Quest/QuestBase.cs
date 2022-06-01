using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game.Scripts.Quest
{
    public abstract class QuestBase
    {
        public QuestType Type { get; set; }
        public bool IsClear { get; protected set; }

        public virtual void Begin(){}
    }
}
