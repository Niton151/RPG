using Game.DataBase.PlayerDataBase;
using Game.Scripts.Manager;
using Game.Scripts.Player;
using Game.Scripts.Player.Skill;
using Game.Scripts.Player.Skill.SkillBehavior;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode.PlayerTests
{
    public class PassiveSkillTest
    {
        private PlayerCore player;

        [SetUp]
        public void SetUp()
        {
            player = PlayerProvider.Create(PlayerType.SwordMan, Vector3.zero);
        }
        
        [Test]
        public void ステ強化系パッシブスキルで強化()
        {
            player.LevelUp();
            player.Skill.SkillUp(new EnhanceStatusSkill());
            Assert.That(player.CurrentParameter.STR.ModifiedValue, Is.EqualTo(14));
        }
        
        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(player.gameObject);
        }
    }
}
