using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Player;
using UnityEngine;
using UnityEngine.TestTools;

namespace Test.Player
{
    public class PlayerCorePlayTest
    {
        private Slime slime;
        private PlayerCore core;

        [SetUp]
        public void SetUp()
        {
            var parameter = new PlayerParameters();
            parameter.MaxHP = 100f;
            parameter.MaxMP = 100f;
            parameter.MaxSP = 100f;
            var player = new GameObject("player");
            core = player.AddComponent<PlayerCore>();
            core.Init(parameter);

            var enemy = new GameObject("enemy");
            slime = enemy.AddComponent<Slime>();
        }
        
        [Test]
        public void 初期化時xPをMaxxPと同じにする()
        {
            var isMatch = new[] {core.HP.Value, core.MP.Value, core.SP.Value}.SequenceEqual(new[] {100f, 100f, 100f});
            Assert.That(isMatch, Is.True);
        }
        
        [Test]
        public void ダメージを受けたらHPが減る()
        {
            var damage = new Damage(slime, 10);

            core.ApplyDamage(damage);

            Assert.That(core.HP.Value, Is.EqualTo(90f));
        }

        [Test]
        public void HPが0になったら死ぬ()
        {
            var damage = new Damage(slime, 100);
            
            core.ApplyDamage(damage);

            Assert.That(core.IsAlive, Is.False);
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(core.gameObject);
            GameObject.Destroy(slime.gameObject);
        }
    }
}
