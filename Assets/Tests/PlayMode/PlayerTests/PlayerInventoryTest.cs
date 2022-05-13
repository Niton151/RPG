using System.Linq;
using Game.Scripts.Item;
using Game.Scripts.Manager;
using Game.Scripts.Player;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode.PlayerTests
{
    public class PlayerInventoryTest
    {
        private PlayerCore core;
        private ItemBase item;

        [SetUp]
        public void SetUp()
        {
            core = PlayerProvider.Create(PlayerType.SwordMan, Vector3.zero);

            item = ItemProvider.Create(ItemID.Herb, Vector3.zero);
            
            item.PickedUp(core);
        }

        [Test]
        public void インベントリを初期化()
        {
            Assert.That(core.Inventory == null, Is.False);
        }
        
        [Test]
        public void インベントリにアイテム追加()
        {
            Assert.That(core.Inventory.ItemList.First(), Is.EqualTo(item));
        }

        [Test]
        public void インベントリからアイテム使用で消費()
        {
            core.Inventory.ItemList[0].Use();
            
            Assert.That(core.Inventory.ItemList.Count, Is.EqualTo(0));
        }

        [Test]
        public void アイテム使用で効果発動()
        {
            core.SetXP(PlayerStatusType.HP, -30);
            
            core.Inventory.ItemList[0].Use();
            
            Assert.That(core.CurrentParameter.HP.Value, Is.EqualTo(95));
        }

        [Test]
        public void インベントリからアイテムを捨てる()
        {
            core.Inventory.ItemList[0].Throw();
            
            Assert.That(core.Inventory.ItemList.Count, Is.EqualTo(0));
        }

        [Test]
        public void アイテムがいっぱいのとき入手できない()
        {
            core.Inventory.MaxSize = 2;

            var item1 = ItemProvider.Create(ItemID.Herb, Vector3.zero);
            
            item1.PickedUp(core);
            
            Assert.That(core.Inventory.IsFull, Is.True);
        }

        [Test]
        public void 同じアイテムを入手したときストックする()
        {
            Assert.That(true, Is.False);
        }

        [Test]
        public void ストック数が最大になったら違うスロットに追加する()
        {
            Assert.That(true, Is.False);
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(core.gameObject);
            GameObject.Destroy(item.gameObject);
        }
    }
}
