using Game.Scripts.Player;
using UnityEngine;

namespace Game.DataBase.PlayerDataBase
{
    [CreateAssetMenu(fileName = "Job", menuName = "CreateJob")]
    public class PlayerJobData : ScriptableObject
    {
        [SerializeField] public PlayerParameters parameters;
        [SerializeField] public GameObject prefab;
    }
}
