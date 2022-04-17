using Game.DataBase.ItemDataBase;
using Game.Scripts.Item;
using UnityEngine;

namespace Game.Scripts.Equipment
{
    public abstract class ArmorBase : ItemBase, IEquipable
    {
        public ArmorData ArmorData { get; private set; }
        
        private bool _canEquip => pickedPlayer.CurrentPlayerParameter.Level >= ArmorData.requiredLevel &&
                                  ArmorData.requiredType.Contains(pickedPlayer.Type);

        public override void Init(BaseItemData data)
        {
            base.Init(data);
            ArmorData = Data as ArmorData;
        }

        public override void Use()
        {
            base.Use();
            
            if (_canEquip)
            {
                var current = pickedPlayer.Equipment.CurrentWeapon;
                if (current != null)
                {
                    current.PickedUp(pickedPlayer);
                }
                pickedPlayer.Equipment.Equip(this);
            }
            else
            {
                Debug.Log("要求レベルを満たしていません");
            }
        }
        
        public void Remove()
        {
            PickedUp(pickedPlayer);
            pickedPlayer.Equipment.Equip(this);
        }
    }
}
