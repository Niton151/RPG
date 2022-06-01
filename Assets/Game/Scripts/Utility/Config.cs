using System;
using Game.Scripts.Player;
using Game.Scripts.Player.MyInput;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Scripts.Utility
{
    public class Config : SerializedScriptableObject
    {
        public static IInputEventProvider InputProvider;

        [OdinSerialize] private Type input;
        public PlayerType initPlayerType;
        

        public void Init()
        {
            InputProvider = (IInputEventProvider) Activator.CreateInstance(input);
        }
    }
}
