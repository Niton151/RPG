using Game.Scripts.Manager;
using Game.Scripts.Player;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayMode.ManagerTests
{
    public class PlayerProviderTest
    {
        [Test]
        public void プレイヤークラスに応じたパラメータを使ってプレイヤー生成()
        {
            var player = PlayerProvider.Create(PlayerType.SwordMan, Vector3.zero);
            Assert.That(player.transform.position, Is.EqualTo(Vector3.zero));
        }
    }
}
