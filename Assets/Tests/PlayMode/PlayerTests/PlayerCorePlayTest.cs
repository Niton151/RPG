using System.Collections;
using System.Linq;
using Game.DataBase.PlayerDataBase;
using Game.Scripts.Damage;
using Game.Scripts.Enemy;
using Game.Scripts.Manager;
using Game.Scripts.Player;
using Game.Scripts.Player.Skill;
using Game.Scripts.Player.Skill.SkillBehavior;
using Game.Scripts.Utility;
using NUnit.Framework;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.PlayerTests
{
    public class PlayerCorePlayTest
    {
        private PlayerCore core;

        [SetUp]
        public void SetUp()
        {
            core = PlayerProvider.Create(PlayerType.SwordMan, Vector3.zero);
        }
        
        [UnityTest]
        public IEnumerator PlayerCoreの初期化完了を通知()
        {
            bool isInitFin = false;
            core.OnInitAsync.Subscribe(_ => isInitFin = true);

            yield return null;
            Assert.That(isInitFin, Is.True);
        }
        
        [Test]
        public void 初期化時xPをMaxxPと同じにする()
        {
            var isMatch = new[] {core.CurrentParameter.HP.Value, core.MP.Value, core.SP.Value}.SequenceEqual(new[] {100f, 100f, 100f});
            Assert.That(isMatch, Is.True);
        }

        [Test]
        public void HPが0になったら死ぬ()
        {
            core.ApplyDamage(new Damage(EnemyProvider.Spawn(EnemyID.Slime, Vector3.zero), 100));

            Assert.That(core.IsAlive, Is.False);
        }

        [Test]
        public void レベルアップしたら要求経験値量が増加する()
        {
            core.LevelUp();
            Assert.That(core.RequiredExp, Is.EqualTo(17));
        }

        [Test]
        public void レベルアップしたらステータスがアップする()
        {
            core.LevelUp();
            core.LevelUp();
            Assert.That(core.CurrentParameter.STR.BaseValue, Is.EqualTo(16));
        }

        [Test]
        public void レベルアップでスキルポイント獲得()
        {
            core.LevelUp();
            Assert.That(core.Skill.SkillPoint, Is.EqualTo(1));
        }

        [Test]
        public void スキル習得で習得済みスキルに追加()
        {
            core.LevelUp();
            core.Skill.SkillUp(new EnhanceStatusSkill());
            Assert.That(core.Skill.SkillTree[SkillID.UpStrP].GetType(), Is.EqualTo(typeof(EnhanceStatusSkill)));
        }

        [Test]
        public void スキルポイントの消費()
        {
            core.LevelUp();
            core.Skill.SkillUp(new EnhanceStatusSkill());
            Assert.That(core.Skill.SkillPoint, Is.EqualTo(0));
        }

        [Test]
        public void スキルポイントが足りないときスキルを上げない()
        {
            core.Skill.SkillUp(new EnhanceStatusSkill());
            
            Assert.That(core.Skill.SkillTree.ContainsKey(SkillID.UpStrP), Is.False);
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(core.gameObject);
            EnemyProvider.Reset();
        }
    }
}
