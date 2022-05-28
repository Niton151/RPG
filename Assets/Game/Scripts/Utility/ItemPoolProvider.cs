using System;
using Game.Scripts.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Utility
{
    [CreateAssetMenu(fileName = "ItemPoolProvider", menuName = "Utility/CreateItemPoolProvider")]
    public class ItemPoolProvider : ScriptableObject
    {
        [SerializeField]
        private MonoBehaviour item;

        private ItemPool _pool;

        public ItemPool Get()
        {
            //すでに準備済みならそちらを返す
            if (_pool != null) return _pool;
        
            //ObjectPoolを作成
            _pool = new ItemPool(item);
            
            return _pool;
        }
    }
}
