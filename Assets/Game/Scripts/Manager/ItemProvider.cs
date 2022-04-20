using Game.DataBase.ItemDataBase;
using Game.Scripts.Item;
using UnityEngine;

namespace Game.Scripts.Manager
{
    public class ItemProvider
    {
        private static ItemDataBase DataBase = Resources.Load<ItemDataBase>("ItemDataBase");
    
        public static ItemBase Create(ItemID id, Vector3 pos)
        {
            var data = DataBase.FindData(id);
            ItemBase item;
            if (data.poolProvider != null)
            {
                item = (ItemBase) data.poolProvider.Get().Rent();
                item.transform.position = pos;
            }
            else
            {
                item = Object.Instantiate(data.prefab, pos, Quaternion.identity).GetComponent<ItemBase>();   
            }
            item.Init(data);
            return item;
        }
    }
}
