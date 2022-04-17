using Game.Scripts.Player;
using UnityEngine;

namespace Game.DataBase.ItemDataBase
{
    [CreateAssetMenu(fileName = "HealItem", menuName = "CreateHealItem")]
    public class HealItemData : BaseItemData
    {
        [SerializeField] public float healValue;
        [SerializeField] public PlayerXPType healType;
    }
}
