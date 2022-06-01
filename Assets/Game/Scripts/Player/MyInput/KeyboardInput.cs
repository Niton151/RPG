using UnityEngine;

namespace Game.Scripts.Player.MyInput
{
    public class KeyboardInput : IInputEventProvider
    {
        public bool OnAction()
        {
            return Input.GetKeyDown(KeyCode.E);
        }
    }
}
