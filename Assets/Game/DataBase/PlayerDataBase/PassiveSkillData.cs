using Game.Scripts.Player;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.DataBase.PlayerDataBase
{
    [CreateAssetMenu(fileName = "PassiveSkill", menuName = "Data/Player/CreatePassiveSkill")]
    public class PassiveSkillData : BaseSkillData
    {
        public PlayerStatusType type;
        public double multiple;
    }
}
