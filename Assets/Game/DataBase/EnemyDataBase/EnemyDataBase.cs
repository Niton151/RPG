using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Enemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.DataBase.EnemyDataBase
{
    [CreateAssetMenu(fileName = "EnemyDataBase", menuName = "CreateEnemyDataBase")]
    public class EnemyDataBase : SerializedScriptableObject
    {
        public List<BaseEnemyData> enemies = new List<BaseEnemyData>();

        public List<BaseEnemyData> GetEnemyList => enemies;

        public BaseEnemyData FindData(EnemyID id)
        {
            return enemies.FirstOrDefault(x => x.id == id);
        }
    }
}