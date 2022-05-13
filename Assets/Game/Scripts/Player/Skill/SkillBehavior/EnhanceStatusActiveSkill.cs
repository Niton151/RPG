using System;
using Cysharp.Threading.Tasks;
using Game.DataBase.PlayerDataBase;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Player.Skill.SkillBehavior
{
    [Serializable]
    public class EnhanceStatusActiveSkill : SkillBase, IActiveSkill
    {
        public bool CanUse { get; private set; }
        public override BaseSkillData Data => _data;
        private ActiveSkillData _data;
        private int _currentStock;

        public EnhanceStatusActiveSkill()
        {
            _data = (ActiveSkillData)SkillDataBase.FindData(SkillID.UpStrA);
            CanUse = true;
            _currentStock = _data.stock;
        }

        public override async void Activate(PlayerCore player)
        {
            if (CanUse)
            {
                player.CurrentParameter.STR.Add(_data.multiple, UniTask.Delay(TimeSpan.FromSeconds(_data.duration)));
                CanUse = false;
                if (--_currentStock <= 0)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_data.coolTime));
                }
            }

            CanUse = true;
        }
    }
}