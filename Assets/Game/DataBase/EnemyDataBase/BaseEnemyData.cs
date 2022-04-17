using System.Collections.Generic;
using Game.DataBase.ItemDataBase;
using Game.Scripts.Enemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.DataBase.EnemyDataBase
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "CreateEnemy")]
    public class BaseEnemyData : SerializedScriptableObject
    {
        public EnemyID id;
        public GameObject prefab;
        public EnemyParameters parameters;
        [DictionaryDrawerSettings(KeyLabel = "item", ValueLabel = "weight")]
        public Dictionary<BaseItemData, float> dropItems = new Dictionary<BaseItemData, float>();
    }
}
