using System;
using System.Collections.Generic;
using Game.DataBase.PlayerDataBase;
using Game.Scripts.Player.Skill;
using Game.Scripts.Utility;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerParameters : BaseParameter
    {
        public PlayerType PlayerType;

        public Modifier JumpPower = new Modifier(1);
        public Modifier RunSpeed = new Modifier(2);

        [Range(0.01f, 0.10f)] public float dSTR;
        [Range(0.01f, 0.10f)] public float dDEF;
        [Range(0.01f, 0.10f)] public float dINT;

        [Range(0.01f, 0.10f)] public float dMaxHP;
        [Range(0.01f, 0.10f)] public float dMaxMP;
        [Range(0.01f, 0.10f)] public float dMaxSP;

        public override BaseParameter Copy()
        {
            var p = new PlayerParameters();

            p.PlayerType = PlayerType;
            p.Level = Level;
            p.JumpPower = new Modifier(JumpPower.BaseValue, typeof(float));
            p.MoveSpeed = new Modifier(MoveSpeed.BaseValue, typeof(float));
            p.RunSpeed = new Modifier(RunSpeed.BaseValue, typeof(float));
            p.MaxHP = new Modifier(MaxHP.BaseValue);
            p.MaxMP = new Modifier(MaxMP.BaseValue);
            p.MaxSP = new Modifier(MaxSP.BaseValue);
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