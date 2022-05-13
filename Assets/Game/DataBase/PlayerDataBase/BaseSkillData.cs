using System;
using System.Collections.Generic;
using Game.Scripts.Player.Skill;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.DataBase.PlayerDataBase
{
    public class BaseSkillData : SerializedScriptableObject
    {
        public SkillID id;
        public string displayName;
        public int cost;
        public List<SkillID> requiredSkills = new List<SkillID>();
    }
}
