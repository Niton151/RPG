using Game.Scripts.Equipment;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerEquipment
    {
        public WeaponBase CurrentWeapon { get; private set; }
        public ArmorBase CurrentArmor { get; private set; }

        public void Equip([CanBeNull] IEquipable item)
        {
            switch (item)
            {
                case WeaponBase w:
                    CurrentWeapon = (w !=  CurrentWeapon) ? w : null;
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