using Game.DataBase.ItemDataBase;

namespace Game.Scripts.Item
{
    public class HealItem : ItemBase
    {
        public override void Use()
        {
            pickedPlayer.SetXP(((HealItemData) Data).healType, ((HealItemData) Data).healValue);

            //Destroy
            base.Use();
        }
    }
}