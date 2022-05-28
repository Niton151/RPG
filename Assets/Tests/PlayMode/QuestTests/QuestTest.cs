using System.Collections;
using Game.Scripts.Manager;
using Game.Scripts.NPC;
using Game.Scripts.Player;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.QuestTests
{
    public class QuestTest
    {
        private NpcCore npc;
        private PlayerCore player;

        [SetUp]
        public void SetUp()
        {
            npc = NpcProvider.Create(Vector3.zero, NpcType.Villager);
            player = PlayerProvider.Create(PlayerType.SwordMan, Vector3.forward * 3);
        }
        
        [Test]
        public void クエストを受注する()
        {
            Assert.That(true, Is.False);
        }

        [Test]
        public void クエストの条件を満たしたらクリア()
        {
            Assert.That(true, Is.False);
        }

        [Test]
        public void クエストをクリアする()
        {
            Assert.That(true, Is.False);
        }

        [Test]
        public void プレイヤーに報酬を与える()
        {
            Assert.That(true, Is.False);
        }
    }
}
