using System;
using System.Collections.Generic;
using Game.DataBase.EnemyDataBase;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Enemy
{
    [Serializable]
    public class EnemyProvider
    {
        public static EnemyDataBase DataBase = Resources.Load<EnemyDataBase>("EnemyDataBase");
        public static int EnemyCount { get; private set; }
        private static int _maxEnemyCount = 10;
        private static List<EnemyBase> enemies = new List<EnemyBase>();

        public static EnemyBase Spawn(EnemyID id, Vector3 pos)
        {
            if (_maxEnemyCount > EnemyCount)
            {
                var data = DataBase.FindData(id);
                var enemy = Object.Instantiate(data.prefab, pos, Quaternion.identity).GetComponent<EnemyBase>();
                enemy.Init(data);
                enemies.Add(enemy);
                EnemyCount++;
                return enemy;
            }

            return null;
        }

        public static void Reset()
        {
            enemies.ForEach(x => Object.Destroy(x.gameObject));
            enemies.Clear();
            EnemyCount = 0;
        }
    }
}
