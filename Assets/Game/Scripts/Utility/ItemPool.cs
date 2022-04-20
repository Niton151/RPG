using Game.Scripts.Item;
using UniRx.Toolkit;
using UnityEngine;

namespace Game.Scripts.Utility
{
    public class ItemPool : ObjectPool<MonoBehaviour>
    {
        private readonly MonoBehaviour _prefab;
        private readonly Transform _root;

        public ItemPool(MonoBehaviour prefab)
        {
            _prefab = prefab;

            //親になるObject
            _root = new GameObject().transform;
            _root.name = $"{prefab.name}s";
            _root.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
        
        protected override MonoBehaviour CreateInstance()
        {
            //インスタンスが新しく必要になったらInstantiate
            var newItem = Object.Instantiate(_prefab, _root, true);
            
            return newItem;
        }
    }
}
