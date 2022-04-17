using System.Collections.Generic;
using Game.Scripts.Item;

namespace Game.Scripts.Player
{
    public class PlayerInventory
    {
        public List<ItemBase> ItemList => _itemList;
        private List<ItemBase> _itemList = new List<ItemBase>();

        public bool IsFull => MaxSize <= _itemList.Count;

        public int MaxSize { get; set; }
    }
}
