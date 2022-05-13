using System.Collections;
using System.Linq;
using Game.DataBase.PlayerDataBase;
using Game.Scripts.Manager;
using Game.Scripts.Player;
using Game.Scripts.Player.Skill;
using Game.Scripts.Player.Skill.SkillBehavior;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.PlayerTests
{
    public class ActiveSkillTest
    {
        private PlayerCore player;

        [SetUp]
        public void SetUp()
        {
            player = PlayerProvider.Create(PlayerType.SwordMan, Vector3.zero);
            player.LevelUp();
            player.Skill.SkillUp(new EnhanceStatusActiveSkill());
            player.Skill.SkillTree[SkillID.UpStrA].Activate(player);
        }

        [Test]
        public void アクティブステ上昇スキル使用でステ上昇()
        {
            Assert.That(player.CurrentParameter.STR.ModifiedValue, Is.EqualTo(26));
        }

        [UnityTest]
        public IEnumerator クールタイム中は使用不可()
        {
            yield return new WaitForSeconds(3);
            
            player.Skill.SkillTree[SkillID.UpStrA].Activate(player);
            Assert.That(player.CurrentParameter.STR.ModifiedValue, Is.EqualTo(13));
        }

        [Test]
        public void 効果適用中は使用不可()
        {
            player.Skill.SkillTree[SkillID.UpStrA].Activate(player);
            Assert.That(player.CurrentParameter.STR.ModifiedValue,Is.EqualTo(26));
        }

        [UnityTest]
        public IEnumerator 効果が切れたらステータスが戻る()
        {
            yield return new WaitForSeconds(3);
            Assert.That(player.CurrentParameter.STR.ModifiedValue, Is.EqualTo(13));
        }
    
        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(player.gameObject);
        }
    }
}
