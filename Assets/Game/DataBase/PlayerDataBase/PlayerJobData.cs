using Game.Scripts.Player;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.DataBase.PlayerDataBase
{
    [CreateAssetMenu(fileName = "Job", menuName = "CreateJob")]
    public class PlayerJobData : SerializedScriptableObject
    {
        [OdinSerialize] public PlayerParameters parameters;
        [SerializeField] public GameObject prefab;
    }
}
