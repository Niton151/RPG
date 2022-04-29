using Game.Scripts.Equipment;
using Game.Scripts.Item;
using Game.Scripts.Manager;
using Game.Scripts.Player;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode.PlayerTests
{
    public class PlayerEquipmentTest
    {
        private PlayerCore core;
        private WeaponBase weapon;
        private ArmorBase armor;
        
        [SetUp]
        public void SetUp()
        {
            core = PlayerProvider.Create(PlayerType.SwordMan, Vector3.zero);

            weapon = (WeaponBase)ItemProvider.Create(ItemID.WoodSword, Vector3.zero);
            
            weapon.PickedUp(core);
        }
        
        [Test]
        public void 装備装着()
        {
            core.Inventory.ItemList[0].Use();
            
            Assert.That(core.Equipment.CurrentWeapon, Is.EqualTo(weapon));
        }

        [Test]
        public void 装備したらオブジェクトを出す()
        {
            core.Inventory.ItemList[0].Use();
            Assert.That(GameObject.Find("WeaponSample(Clone)"), !Is.Null);
        }

        [Test]
        public void 武器の装備によるステータスアップ()
        {
            Assert.That(true, Is.False);
        }

        [Test]
        public void 防具装備によるステータスアップ()
        {
            Assert.That(true, Is.False);
        }

        [Test]
        public void 装備の入れ替え()
        {
            var weapon1 = ItemProvider.Create(ItemID.WoodSword, Vector3.zero);
            weapon1.PickedUp(core);
            core.Inventory.ItemList[0].Use();
            core.Inventory.ItemList[1].Use();

            Assert.That(core.Inventory.ItemList[1] == weapon1 && core.Equipment.CurrentWeapon == weapon1, Is.True);
        }
        
        [Test]
        public void 装備を外す()
        {
            core.Inventory.ItemList[0].Use();
            weapon.Remove();
            Assert.That(core.Equipment.CurrentWeapon, Is.Null);
        }
        
        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(core.gameObject);
            GameObject.Destroy(weapon.gameObject);
        }
    }
}