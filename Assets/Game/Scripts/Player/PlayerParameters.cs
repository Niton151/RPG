using System;

namespace Game.Scripts.Player
{
    [Serializable]
    public struct PlayerParameters
    {
        public PlayerType PlayerType;

        public int Level;

        public float JumpPower;
        public float MoveSpeed;
        public float RunSpeed;

        public float MaxHP;
        public float MaxMP;
        public float MaxSP;

        public int ATK;
        public int DEF;
        public int INT;

        /// <summary>
        /// Job:SwordMan, lv:1, jump:1, walk:1, run:3, maxHP,MP,SP:100, ATK,DFS,INT:10, InvSize:3 
        /// </summary>
        /// <returns></returns>
        public static PlayerParameters GetDefaultParameters()
        {
            var parameters = new PlayerParameters();
            parameters.PlayerType = PlayerType.SwordMan;
            parameters.Level = 1;
            parameters.JumpPower = 1;
            parameters.MoveSpeed = 1;
            parameters.RunSpeed = 3;
            parameters.MaxHP = 100;
            parameters.MaxMP = 100;
            parameters.MaxSP = 100;
            parameters.ATK = 10;
            parameters.DEF = 10;
            parameters.INT = 10;
        
            return parameters;
        }
    }
}