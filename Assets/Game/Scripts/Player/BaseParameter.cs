using System;
using Game.Scripts.Utility;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Player
{
    [Serializable]
    public abstract class BaseParameter
    {
        public int Level;

        public Modifier MoveSpeed = new Modifier(1, typeof(float));

        public Modifier MaxHP;
        public Modifier MaxMP;
        public Modifier MaxSP;

        [ReadOnly]
        public ReactiveProperty<float> HP = new ReactiveProperty<float>();

        public Modifier STR;
        public Modifier DEF;
        public Modifier INT;

        public Modifier FireResistance = new Modifier(1);
        public Modifier IceResistance = new Modifier(0);
        public Modifier WaterResistance = new Modifier(0);
        public Modifier ThunderResistance = new Modifier(0);
        public Modifier EarthResistance = new Modifier(0);
        public Modifier SacredResistance = new Modifier(0);
        [SerializeReference]public Modifier DarkResistance = new Modifier(0);

        public abstract BaseParameter Copy();
    }
}
