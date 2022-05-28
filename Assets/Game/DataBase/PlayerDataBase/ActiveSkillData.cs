using UnityEngine;

namespace Game.DataBase.PlayerDataBase
{
    [CreateAssetMenu(fileName = "ActiveSkill", menuName = "Data/Player/CreateActiveSkill")]
    public class ActiveSkillData : BaseSkillData
    {
        public double multiple;
        public int coolTime;
        public double duration;
        public int stock;
    }
}
