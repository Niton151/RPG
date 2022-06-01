using System;
using System.Collections;
using Game.Scripts.Manager;
using Game.Scripts.NPC;
using Game.Scripts.Player;
using NUnit.Framework;
using UniRx;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.QuestTests
{
    public class QuestTest
    {
        private NpcCore npc;
        private PlayerCore player;
        private ColliderTest ct;

        [SetUp]
        public void SetUp()
        {
            var gameManager = new GameObject("Manager").AddComponent<GameManager>();
            player = gameManager.Player;
            npc = NpcProvider.Create(Vector3.zero, NpcType.Villager);
            ct = new ColliderTest();
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

        [UnityTest]
        public IEnumerator Gotoクエストをクリア()
        {
            yield return null;
            ct.Clash(player.gameObject, npc.gameObject);
            yield return ct.OnTestFinished.Timeout(TimeSpan.FromSeconds(3)).ToYieldInstruction(throwOnError:false);
            yield return new WaitUntil(() => npc.IsTalking);
            player.GetQuestFlowList[0][0].Begin();
            Assert.That(player.GetQuestFlowList[0][0].IsClear, Is.True);
        }
    }
}
