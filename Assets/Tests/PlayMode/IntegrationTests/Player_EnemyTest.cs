using Game.Scripts.Damage;
using Game.Scripts.Enemy;
using Game.Scripts.Enemy.Enemies;
using Game.Scripts.Equipment.Weapons;
using Game.Scripts.Item;
using Game.Scripts.Manager;
using Game.Scripts.Player;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode.IntegrationTests
{
    public class Player_EnemyTest
    {
        private PlayerCore player;
        private Slime enemy;
        private Sword weapon;
        
        [SetUp]
        public void SetUp()
        {
            player = PlayerProvider.Create(PlayerType.SwordMan, Vector3.zero);
            enemy = (Slime)EnemyProvider.Spawn(EnemyID.Slime, Vector3.zero);
            weapon = (Sword)ItemProvider.Create(ItemID.WoodSword, Vector3.zero);
        }

        [Test]
        public void PlayerからEnemyへの攻撃()
        {
            player.Attacker.Attack(enemy);
            Assert.That(enemy.HP.Value, Is.EqualTo(enemy.Data.parameters.MaxHP - player.CurrentPlayerParameter.ATK + (player.Equipment.CurrentWeapon?.WeaponData.ATK ?? 0)));
        }

        [Test]
        public void EnemyからPlayerへの攻撃()
        {
            enemy.Attack(player);
            Assert.That(player.HP.Value, Is.EqualTo(player.CurrentPlayerParameter.MaxHP - enemy.Data.parameters.ATK));
        }
        
        [Test]
        public void Enemyが死んだときPlayerに報酬を与える()
        {
            enemy.ApplyDamage(new Damage(player.Attacker, 100));
            Assert.That(player.Inventory.ItemList[0].Data.id, Is.EqualTo(ItemID.Herb));
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
