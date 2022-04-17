using System.Collections;
using System.Linq;
using Game.Scripts.Damage;
using Game.Scripts.Enemy;
using Game.Scripts.Manager;
using Game.Scripts.Player;
using NUnit.Framework;
using UniRx;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.PlayerTests
{
    public class PlayerCorePlayTest
    {
        private PlayerCore core;

        [SetUp]
        public void SetUp()
        {
            var parameter = new PlayerParameters();
            parameter.MaxHP = 100f;
            parameter.MaxMP = 100f;
            parameter.MaxSP = 100f;
            parameter.ATK = 1;
            core = PlayerProvider.Create(PlayerType.SwordMan, Vector3.zero);
        }
        
        [UnityTest]
        public IEnumerator PlayerCoreの初期化完了を通知()
        {
            bool isInitFin = false;
            core.OnInitAsync.Subscribe(_ => isInitFin = true);

            yield return null;
            Assert.That(isInitFin, Is.True);
        }
        
        [Test]
        public void 初期化時xPをMaxxPと同じにする()
        {
            var isMatch = new[] {core.HP.Value, core.MP.Value, core.SP.Value}.SequenceEqual(new[] {100f, 100f, 100f});
            Assert.That(isMatch, Is.True);
        }

        [Test]
        public void HPが0になったら死ぬ()
        {
            core.ApplyDamage(new Damage(EnemyProvider.Spawn(EnemyID.Slime, Vector3.zero), 100));

            Assert.That(core.IsAlive, Is.False);
        }

        [Test]
        public void SetPlayerParametersのテスト()
        {
            var enhancedParameters = core.CurrentPlayerParameter;
            var temp = enhancedParameters;
            enhancedParameters.ATK++;
            
            core.SetPlayerParameter(enhancedParameters);
            
            Assert.That(core.CurrentPlayerParameter.ATK, Is.EqualTo(temp.ATK + 1));
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(core.gameObject);
            EnemyProvider.Reset();
        }
    }
}
