using System.Collections.Generic;
using System.Linq;
using Game.DataBase.EnemyDataBase;
using Game.Scripts.Enemy;
using Game.Scripts.Player.Skill;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.DataBase.PlayerDataBase
{
    [CreateAssetMenu(fileName = "SkillDataBase", menuName = "CreateSkillDataBase")]
    public class SkillDataBase : SerializedScriptableObject
    {
        public List<BaseSkillData> skills = new List<BaseSkillData>();

        public List<BaseSkillData> GetSkillList => skills;
        
        public BaseSkillData FindData(SkillID id)
        {
            return skills.FirstOrDefault(x => x.id == id);
        }
    }
}
