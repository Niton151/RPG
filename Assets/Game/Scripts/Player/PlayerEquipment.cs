using System;
using Game.Scripts.Equipment;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerEquipment
    {
        public WeaponBase CurrentWeapon { get; private set; }
        public ArmorBase CurrentArmor { get; private set; }

        private Transform _rHand;

        public PlayerEquipment(Transform player)
        {
            _rHand = player.Find("RHand").transform;
        }

        public void Equip([CanBeNull] IEquipable item)
        {
            switch (item)
            {
                case WeaponBase w:
                    CurrentWeapon = (w !=  CurrentWeapon) ? w : null;
                    Transform transform = w.transform;
                    transform.SetParent(_rHand);
                    transform.position = _rHand.position;
                    w.gameObject.SetActive(true);
                    break;
                case ArmorBase a:
                    CurrentArmor = (a != CurrentArmor) ? a : null;
                    break;
                default:
                    Debug.LogError("不明なアイテムが装備されました。");
                    break;
            }
        }
    }
}