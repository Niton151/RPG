using Game.Scripts.Enemy;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode.EnemyTests
{
    public class EnemyProviderTest
    {
        private EnemyProvider provider;
        
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void エネミーを特定の位置に生成()
        {
            var enemy = EnemyProvider.Spawn(EnemyID.Slime, new Vector3(1, 1, 1));
            Assert.That(enemy.transform.position, Is.EqualTo(new Vector3(1, 1, 1)));
        }

        [Test]
        public void 上限に達したらスポーンさせない()
        {
            for (int i = 0; i < 11; i++)
            {
                EnemyProvider.Spawn(EnemyID.Slime, Vector3.zero);
            }
            Assert.That(EnemyProvider.EnemyCount, Is.EqualTo(10));
        }

        [TearDown]
        public void TearDown()
        {
            EnemyProvider.Reset();
        }
    }
}
