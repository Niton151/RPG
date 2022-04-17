using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Item;
using UnityEngine;

namespace Game.DataBase.ItemDataBase
{
    public class ItemDataBase : ScriptableObject
    {
        [SerializeField] private List<BaseItemData> items = new List<BaseItemData>();

        public List<BaseItemData> GetItemList => items;

        public BaseItemData FindData(ItemID id)
        {
            return items.FirstOrDefault(x => x.id == id);
        }
    }
}
