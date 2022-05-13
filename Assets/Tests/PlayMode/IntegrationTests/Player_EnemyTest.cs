using System;
using System.Collections;
using System.Threading.Tasks;
using Game.Scripts.Damage;
using Game.Scripts.Damage.AbState;
using Game.Scripts.Enemy;
using Game.Scripts.Enemy.Enemies;
using Game.Scripts.Equipment.Weapons;
using Game.Scripts.Item;
using Game.Scripts.Manager;
using Game.Scripts.Player;
using NUnit.Framework;
using UniRx;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.IntegrationTests
{
    public class Player_EnemyTest
    {
        private PlayerCore player;
        private Slime enemy;
        private Sword weapon;
        private ColliderTest ct;
        
        [SetUp]
        public void SetUp()
        {
            player = PlayerProvider.Create(PlayerType.SwordMan, Vector3.up * 5);
            enemy = (Slime)EnemyProvider.Spawn(EnemyID.Slime, Vector3.right * 5);
            weapon = (Sword)ItemProvider.Create(ItemID.WoodSword, Vector3.down * 5);
            ct = new ColliderTest();

            Time.timeScale = 10;
        }

        [UnityTest]
        public IEnumerator PlayerからEnemyへの攻撃()
        {
            weapon.PickedUp(player);
            player.Inventory.ItemList[0].Use();
            
            ct.Clash(enemy.gameObject, weapon.gameObject);

            yield return ct.OnTestFinished.Timeout(TimeSpan.FromSeconds(3)).ToYieldInstruction(throwOnError:false);
            
            Assert.That(enemy.CurrentParameter.HP.Value, Is.EqualTo(85));
        }

        [UnityTest]
        public IEnumerator EnemyからPlayerへの攻撃()
        {
            ct.Clash(enemy.gameObject, player.gameObject);

            yield return ct.OnTestFinished.Timeout(TimeSpan.FromSeconds(3)).ToYieldInstruction(throwOnError:false);
            
            Assert.That(player.CurrentParameter.HP.Value, Is.EqualTo(95));
        }
        
        [UnityTest]
        public IEnumerator EnemyからPlayerへの属性攻撃()
        {
            var spider = EnemyProvider.Spawn(EnemyID.Spider, Vector3.left * 5);

            ct.Clash(spider.gameObject, player.gameObject);

            yield return ct.OnTestFinished.Timeout(TimeSpan.FromSeconds(3)).ToYieldInstruction(throwOnError:false);

            yield return new WaitForSeconds(20);
            
            GameObject.Destroy(spider.gameObject);
            
            Assert.That(player.CurrentParameter.HP.Value, Is.EqualTo(35));
        }

        [UnityTest]
        public IEnumerator PlayerからEnemyへの属性攻撃()
        {
            var poisonSword = ItemProvider.Create(ItemID.PoisonSword, Vector3.left * 10);
            poisonSword.PickedUp(player);
            player.Inventory.ItemList[0].Use();
            
            ct.Clash(enemy.gameObject, poisonSword.gameObject);
            
            yield return ct.OnTestFinished.Timeout(TimeSpan.FromSeconds(3)).ToYieldInstruction(throwOnError:false);

            yield return new WaitForSeconds(20);
            
            GameObject.Destroy(poisonSword.gameObject);
            
            Assert.That(enemy.CurrentParameter.HP.Value, Is.EqualTo(10));
        }
        
        [Test]
        public void Enemyが死んだときPlayerに報酬を与える()
        {
            enemy.ApplyDamage(new Damage(player.Attacker, 100));
            Assert.That(player.Inventory.ItemList[0].Data.id, Is.EqualTo(ItemID.Herb));
        }

        [Test]
        public void Enemyが死んだときPlayerのExpが増加する()
        {
            enemy.ApplyDamage(new Damage(player.Attacker, 100));
            Assert.That(player.Exp == 12);
        }

            [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(player.gameObject);
            EnemyProvider.Reset();
            GameObject.Destroy(weapon.gameObject);
        }
    }
}
