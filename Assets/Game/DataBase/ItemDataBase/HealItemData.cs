using Game.Scripts.Player;
using UnityEngine;

namespace Game.DataBase.ItemDataBase
{
    [CreateAssetMenu(fileName = "HealItem", menuName = "Data/Item/CreateHealItem")]
    public class HealItemData : BaseItemData
    {
        [SerializeField] public float healValue;
        [SerializeField] public PlayerStatusType healType;
    }
}
