using System.Collections.Generic;
using Game.Scripts.NPC;
using Game.Scripts.Quest;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.DataBase.CharacterDataBase
{
    [CreateAssetMenu(fileName = "NPC", menuName = "Data/Character/CreateNPC")]
    public class BaseNpcData : SerializedScriptableObject
    {
        public NpcType type;
        public string npcName;
        public GameObject prefab;
        public string message;
        [OdinSerialize] public QuestFlow questFlow;
    }
}
