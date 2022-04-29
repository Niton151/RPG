using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enemy;
using Game.Scripts.Item;
using Game.Scripts.Manager;
using NUnit.Framework;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Object = System.Object;

public class CollisionTestTest
{
    private EnemyBase a;
    private ItemBase b;
    
    [OneTimeSetUp]
    public void SetUp()
    {
        a = EnemyProvider.Spawn(EnemyID.Slime, Vector3.up * 5);
        b = ItemProvider.Create(ItemID.WoodSword, Vector3.zero);   
    }
    
    [UnityTest]
    public IEnumerator ColliderTestが動作するかのテスト()
    {
        var ct = new ColliderTest();
        ct.Clash(a.gameObject, b.gameObject);

        var yi = ct.OnTestFinished.Timeout(TimeSpan.FromSeconds(3f)).ToYieldInstruction(throwOnError: false);
        yield return yi;

        Assert.That(yi.HasError, Is.False);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        EnemyProvider.Reset();
        GameObject.Destroy(b.gameObject);
    }
}
