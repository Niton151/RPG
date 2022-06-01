using System;
using System.Collections;
using Game.Scripts.Manager;
using Game.Scripts.Player;
using Game.Scripts.Utility;
using NUnit.Framework;
using UniRx;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.ManagerTests
{
    public class GameManagerTest
    {
        private GameManager manager;
        
        [SetUp]
        public void SetUp()
        {
            manager = new GameObject("GameManager").AddComponent<GameManager>();
            manager.Initializer.Initialize(ScriptableObject.CreateInstance<Config>());
        }
        
        [UnityTest]
        public IEnumerator ゲームの初期化()
        {
            var yi =  manager.Initializer.OnInitFinished.Timeout(TimeSpan.FromSeconds(3)).ToYieldInstruction(throwOnError:false);
            yield return yi;
            Assert.That(yi.HasError, Is.False);
        }

        [UnityTest]
        public IEnumerator Initが終わったらゲームステートを変更()
        {
            var yi =  manager.Initializer.OnInitFinished.Timeout(TimeSpan.FromSeconds(3)).ToYieldInstruction(throwOnError:false);
            yield return yi;
            Assert.That(manager.State.Value, Is.EqualTo(GameState.Town));
        }

        [Test]
        public void Fieldになったら攻撃可能()
        {
            Assert.That(true, Is.False);
        }

        [Test]
        public void Townでは攻撃不可()
        {
            Assert.That(true, Is.False);
        }

        [Test]
        public void Menuではマウスカーソルを表示()
        {
            Assert.That(true, Is.False);
        }
    }
}
