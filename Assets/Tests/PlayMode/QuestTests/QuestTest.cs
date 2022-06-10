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

        [UnitySetUp]
        public IEnumerator UnitySetUp()
        {
            var gameManager = new GameObject("Manager").AddComponent<GameManager>();
            player = gameManager.Player;
            npc = NpcProvider.Create(Vector3.zero, NpcType.Villager);
            ct = new ColliderTest();
            
            yield return null;
            ct.Clash(player.gameObject, npc.gameObject);
            yield return ct.OnTestFinished.Timeout(TimeSpan.FromSeconds(3)).ToYieldInstruction(throwOnError:false);
            
            player.GetQuestFlowList[0].StartFlow(player);
            yield return player.GetQuestFlowList[0].Quests[0].OnClear.ToYieldInstruction();
        }

        [Test]
        public void プレイヤーに報酬を与える()
        {
            Assert.That(player.Inventory.ItemList.Count, Is.EqualTo(1));
        }

        [Test]
        public void クエストが進む()
        {
            Assert.That(player.GetQuestFlowList[0].Progress, Is.EqualTo(1));
        }

        [Test]
        public void クエストを破棄する()
        {
            Assert.That(true, Is.False);
        }

        [Test]
        public void クエストフロークリア()
        {
            Assert.That(player.GetQuestFlowList[0].IsFlowClear, Is.True);
        }

        [Test]
        public void Gotoクエストをクリア()
        {
            Assert.That(player.GetQuestFlowList[0].Quests[0].IsClear, Is.True);
        }
    }
}
