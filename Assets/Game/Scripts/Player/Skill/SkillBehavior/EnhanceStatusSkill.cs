using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.DataBase.PlayerDataBase;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Player.Skill.SkillBehavior
{
    [Serializable]
    public class EnhanceStatusSkill : SkillBase, IPassiveSkill
    {
        public override BaseSkillData Data => _data;
        private PassiveSkillData _data;
        
        public EnhanceStatusSkill()
        {
            _data = SkillDataBase.FindData(SkillID.UpStrP) as PassiveSkillData;
        }
        
        public override void Activate(PlayerCore player)
        {
            player.CurrentParameter.STR.Add(_data.multiple, UniTask.Never(CancellationToken.None));
        }
    }
}
