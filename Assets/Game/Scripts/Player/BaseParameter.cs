using System;

namespace Game.Scripts.Player
{
    [Serializable]
    public class BaseParameter
    {
        public int Level;

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
        public static BaseParameter GetDefaultParameters()
        {
            var parameters = new BaseParameter();
            parameters.Level = 1;
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
