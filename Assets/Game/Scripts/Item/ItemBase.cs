using Game.DataBase.ItemDataBase;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Item
{
    public abstract class ItemBase : MonoBehaviour, IAvailable
    {
        [SerializeField] protected ItemID id;
        protected PlayerCore pickedPlayer;
        
        public BaseItemData Data { get; private set; }

        public virtual void Init(BaseItemData data)
        {
            Data = data;
        }

        public virtual void PickedUp(PlayerCore player)
        {
            pickedPlayer = player;
            player.Inventory.ItemList.Add(this);

            this.gameObject.SetActive(false);
        }

        public virtual void Use()
        {
            pickedPlayer.Inventory.ItemList.Remove(this);
        }

        public void Throw()
        {
            pickedPlayer.Inventory.ItemList.Remove(this);
            
            if (Data.poolProvider != null)
            {
                Data.poolProvider.Get().Return(this);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}