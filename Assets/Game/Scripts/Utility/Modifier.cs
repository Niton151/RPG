using System;
using System.Collections.Generic;
using Codice.CM.SEIDInfo;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Utility
{
    public class Modifier
    {
        public int BaseValue
        {
            get => _base;
            set
            {
                _base = value;
                ModifyAll();
            }
        }

        [OdinSerialize] private int _base;
        private Type _returnType;

        public float ModifiedValue
        {
            get
            {
                if (_returnType == typeof(int))
                {
                    return (int) Math.Round(_modifiedValue, 1);
                }

                return (float) _modifiedValue;
            }
        }

        private double _modifiedValue;
        private List<(double, UniTask)> _modifyList = new List<(double, UniTask)>();

        public Modifier(int baseValue, Type returnType = null)
        {
            _base = baseValue;
            _modifiedValue = baseValue;

            _returnType = returnType ?? typeof(int);
        }

        public async void Add(double value, UniTask until)
        {
            _modifyList.Add((value, until));
            _modifiedValue *= value;

            await until;

            _modifiedValue /= value;
            _modifyList.Remove((value, until));
        }

        public void ModifyAll()
        {
            double multiple = 1;
            foreach (var modify in _modifyList)
            {
                multiple *= modify.Item1;
            }

            _modifiedValue = _base * multiple;
        }
    }
}