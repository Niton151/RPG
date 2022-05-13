using System;
using Codice.Client.ChangeTrackerService;
using Game.DataBase.PlayerDataBase;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Scripts.Player.Skill
{
    [Serializable]
    public abstract class SkillBase
    {
        public virtual BaseSkillData Data { get; protected set; }
        protected SkillDataBase SkillDataBase => _skillDataBase;
        private SkillDataBase _skillDataBase = Resources.Load<SkillDataBase>("SkillDataBase");
        public abstract void Activate(PlayerCore player);
    }
}
