using System;

namespace Game.Scripts.Enemy
{
    [Serializable]
    public class EnemyParameters
    {
        public int Level;

        public float MoveSpeed;

        public float MaxHP;
        public int ATK;
        public int DEF;
        public int INT;

        /// <summary>
        /// Lv:1, Speed:1, MaxHP:100, ATK,DFS,INT:5
        /// </summary>
        /// <returns></returns>
        public static EnemyParameters GetDefaultParameters()
        {
            var p = new EnemyParameters();

            p.Level = 1;
            p.MoveSpeed = 1;
            p.MaxHP = 100;
            p.ATK = 5;
            p.DEF = 5;
            p.INT = 5;

            return p;
        }
    }
}
