using System.Collections;
using Game.Scripts.Item;
using Game.Scripts.Manager;
using Game.Scripts.Player;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.ItemTests
{
    public class AllItemBehaviourTest
    {
        private ItemBase item;
        
        [SetUp]
        public void SetUp()
        {
            item = ItemProvider.Create(ItemID.Herb, Vector3.zero);
        }

        [Test]
        public void 初期化テスト()
        {
            Assert.That(item.Data.id, Is.EqualTo(ItemID.Herb));
        }

        [UnityTest]
        public IEnumerator 拾うと消える()
        {
            item.PickedUp(PlayerProvider.Create(PlayerType.SwordMan, Vector3.zero));
            yield return null;
            Assert.That(item == null, Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            if (item != null)
            {
                Object.Destroy(item.gameObject);   
            }
        }
    }
}
