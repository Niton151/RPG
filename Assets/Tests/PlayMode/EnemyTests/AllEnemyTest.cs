using System;
using System.Collections;
using Game.Scripts.Damage;
using Game.Scripts.Enemy;
using NUnit.Framework;
using UniRx;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.EnemyTests
{
    public class AllEnemyTest
    {
        private EnemyBase enemy;
        
        [SetUp]
        public void SetUp()
        {
            enemy = EnemyProvider.Spawn(EnemyID.Slime, Vector3.zero);
        }

        [Test]
        public void ステータスの初期化()
        {
            Assert.That(enemy.Data.parameters.Level, Is.EqualTo(1));
        }

        [Test]
        public void HPをMaxHPにする()
        {
            Assert.That(enemy.HP.Value, Is.EqualTo(100));
        }

        [UnityTest]
        public IEnumerator HPが0以下で死ぬ()
        {
            var yi = enemy.OnDead.Timeout(TimeSpan.FromSeconds(1f)).ToYieldInstruction(throwOnError:false);
            enemy.ApplyDamage(new Damage(enemy, 100));
            yield return yi;
            Assert.That(yi.HasError, Is.False);
        }

        [Test]
        public void 戦闘モードと徘徊モードの切り替え()
        {
            Assert.That(true, Is.False);
        }

        [TearDown]
        public void End()
        {
            EnemyProvider.Reset();
        }
    }
}
