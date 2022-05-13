using System.Collections.Generic;
using Game.Scripts.Damage.AbState;
using Game.Scripts.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.DataBase.ItemDataBase
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "CreateWeapon")]
    public class WeaponData : BaseItemData
    {
        [SerializeField] public float ATK;
        [SerializeReference] public IAbState AbState;
        [SerializeField] public int requiredLevel;
        [ShowInInspector] public List<PlayerType> requiredType = new List<PlayerType>();
    }
}