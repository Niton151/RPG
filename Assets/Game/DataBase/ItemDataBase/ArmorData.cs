using System.Collections.Generic;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.DataBase.ItemDataBase
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Data/Item/CreateArmor")]
    public class ArmorData : BaseItemData
    {
        [SerializeField] public float DEF;
        [SerializeField] public float requiredLevel;
        [SerializeField] public List<PlayerType> requiredType = new List<PlayerType>();
    }
}
