using Game.Scripts.NPC;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.DataBase.CharacterDataBase
{
    [CreateAssetMenu(fileName = "NPC", menuName = "Data/Character/CreateNPC")]
    public class BaseNpcData : SerializedScriptableObject
    {
        public NpcType type;
        public string name;
        public GameObject prefab;
    }
}
