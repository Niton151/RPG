using System;
using System.Collections.Generic;
using Game.DataBase.ItemDataBase;
using Game.Scripts.Item;
using Game.Scripts.Manager;
using Game.Scripts.Player;
using Sirenix.Serialization;
using UniRx;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game.Scripts.Quest
{
    public abstract class QuestBase
    {
        public QuestType Type { get; set; }
        [OdinSerialize] private Dictionary<ItemID, float> _rewardItems = new Dictionary<ItemID, float>();
        [OdinSerialize] private int _expReward;

        public IObservable<Unit> OnClear => onClearSubject;
        protected Subject<Unit> onClearSubject;
        public bool IsClear { get; protected set; }

        public virtual void Begin(PlayerCore player)
        {
        }

        protected void GiveReward(PlayerCore player)
        {
            foreach (var x in _rewardItems)
            {
                var item = ItemProvider.Create(x.Key, player.transform.position);
                item.PickedUp(player);
            }

            player.Exp += _expReward;

            //報酬金の処理
        }
    }
}