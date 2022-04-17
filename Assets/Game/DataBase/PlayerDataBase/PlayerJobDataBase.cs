using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.DataBase.PlayerDataBase
{
    [CreateAssetMenu(fileName = "JobDataBase", menuName = "CreateJobDataBase")]
    public class PlayerJobDataBase : ScriptableObject
    {
        [SerializeField] private List<PlayerJobData> _jobs = new List<PlayerJobData>();

        public List<PlayerJobData> GetItemList => _jobs;

        public PlayerJobData FindData(PlayerType type)
        {
            return _jobs.FirstOrDefault(x => x.parameters.PlayerType == type);
        }
    }
}
