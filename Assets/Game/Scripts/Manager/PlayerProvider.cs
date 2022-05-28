using Game.DataBase.PlayerDataBase;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Manager
{
    public static class PlayerProvider
    {
        private static PlayerJobDataBase DataBase = Resources.Load<PlayerJobDataBase>("JobDataBase");
    
        public static PlayerCore Create(PlayerType job, Vector3 pos)
        {
            var data = DataBase.FindData(job);
            var player = Object.Instantiate(data.prefab, pos, Quaternion.identity).GetComponent<PlayerCore>();
            player.Init(data);
            return player;
        }
    }
}
