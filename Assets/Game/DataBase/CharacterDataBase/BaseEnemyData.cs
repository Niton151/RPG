using System;
using System.Collections.Generic;
using Game.DataBase.ItemDataBase;
using Game.Scripts.Enemy;
using Game.Scripts.Utility;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.DataBase.EnemyDataBase
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Data/Character/CreateEnemy")]
    public class BaseEnemyData : SerializedScriptableObject
    {
        public EnemyID id;
        public GameObject prefab;
        [OdinSerialize]public EnemyParameters parameters;
        public ItemPoolProvider poolProvider;
        [DictionaryDrawerSettings(KeyLabel = "item", ValueLabel = "weight")]
        public Dictionary<BaseItemData, float> dropItems = new Dictionary<BaseItemData, float>();
    }
}
