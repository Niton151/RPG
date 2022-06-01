using System;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Player.MyInput
{
    public class MockInput : IInputEventProvider
    {
        public MockInput()
        {
            Debug.Log("MockInput constructor");
        }
        public bool OnAction()
        {
            return true;
        }
    }
}
