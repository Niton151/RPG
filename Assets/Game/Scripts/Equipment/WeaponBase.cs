using System;
using Game.DataBase.ItemDataBase;
using Game.Scripts.Damage;
using Game.Scripts.Item;
using Game.Scripts.Player;
using UniRx;
using UniRx.Diagnostics;
using UnityEngine;

namespace Game.Scripts.Equipment
{
    public abstract class WeaponBase : ItemBase, IEquipable
    {
        public WeaponData WeaponData { get; private set; }
        public IObservable<IDamageApplicable> OnHit => _onHitSubject;
        protected Subject<IDamageApplicable> _onHitSubject = new Subject<IDamageApplicable>();

        private bool _canEquip => pickedPlayer.CurrentPlayerParameter.Level >= WeaponData.requiredLevel &&
                                  WeaponData.requiredType.Contains(pickedPlayer.Type);

        public override void Init(BaseItemData data)
        {
            base.Init(data);
            WeaponData = Data as WeaponData;

            _onHitSubject.AddTo(this);
        }

        public override void Use()
        {
            if (_canEquip)
            {
                var current = pickedPlayer.Equipment.CurrentWeapon;
                pickedPlayer.Equipment.Equip(this);
                
                _onHitSubject.Subscribe(pickedPlayer.Attacker.Attack);
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
