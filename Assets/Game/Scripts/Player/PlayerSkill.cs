using System;
using System.Collections.Generic;
using Game.DataBase.PlayerDataBase;
using Game.Scripts.Player.Skill;
using UniRx;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game.Scripts.Player
{
    public class PlayerSkill
    {
        public int SkillPoint => _skillPoint;
        private int _skillPoint;
        private PlayerCore _core;
        public Dictionary<SkillID, SkillBase> SkillTree = new Dictionary<SkillID, SkillBase>();

        public PlayerSkill(PlayerCore core)
        {
            _core = core;
            Init();
        }

        private void Init()
        {
            _core.OnLevelUp.Subscribe(_ => _skillPoint++).AddTo(_core.gameObject);
        }
        
        public void SkillUp(SkillBase skill)
        {
            var skillData = skill.Data;

            if (_skillPoint >= skillData.cost)
            {
                _skillPoint -= skillData.cost;
                try
                {
                    SkillTree.Add(skillData.id, skill);
                    if (skillData is PassiveSkillData)
                    {
                        skill.Activate(_core);
                    }
                }
                catch (ArgumentException)
                {
                    Debug.Log("重複したスキルを習得しようとしています");
                }
            }
            else
            {
                Debug.Log("スキルポイントが不足しています");
            }
        }
    }
}
