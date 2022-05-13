using System;
using Game.Scripts.Player;
using Game.Scripts.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class EnemyParameters : BaseParameter
    {
        [ShowInInspector]public int Exp => (int) Mathf.Ceil((float)(12 + 5 * ( Math.Pow(Level - 1, 2.6) / 50.0f)));
        
        /// <summary>
        /// Lv:1, Speed:1, MaxHP:100, ATK,DFS,INT:5
        /// </summary>
        /// <returns></returns>
        public override BaseParameter Copy()
        {
            var p = new EnemyParameters();

            p.Level = Level;
            p.MoveSpeed = new Modifier(MoveSpeed.BaseValue, typeof(float));
            p.MaxHP = new Modifier(MaxHP.BaseValue);
            p.STR = new Modifier(STR.BaseValue);
            p.DEF = new Modifier(DEF.BaseValue);
            p.INT = new Modifier(INT.BaseValue);
            p.FireResistance = new Modifier(FireResistance.BaseValue);
            p.IceResistance = new Modifier(IceResistance.BaseValue);
            p.WaterResistance = new Modifier(WaterResistance.BaseValue);
            p.ThunderResistance = new Modifier(ThunderResistance.BaseValue);
            p.EarthResistance = new Modifier(EarthResistance.BaseValue);
            p.SacredResistance = new Modifier(SacredResistance.BaseValue);
            p.DarkResistance = new Modifier(DarkResistance.BaseValue);

            return p;
        }
    }
}
