using System;
using Game.Scripts.Item;
using Game.Scripts.Utility;
using UnityEngine;

namespace Game.DataBase.ItemDataBase
{
    [Serializable]
    public class BaseItemData : ScriptableObject
    {
        [SerializeField] public ItemID id;
        [SerializeField] public string description;
        [SerializeField] public int price;
        [SerializeField] public GameObject prefab;
        [SerializeField] public ItemPoolProvider poolProvider;
    }
}