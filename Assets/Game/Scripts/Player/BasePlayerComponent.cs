using Game.Scripts.Player.Input;
using UnityEngine;

namespace Game.Scripts.Player
{
    public abstract class BasePlayerComponent : MonoBehaviour
    {
        private IInputEventProvider _inputEventProvider;
        
        protected PlayerCore core;
        protected IInputEventProvider InputEventProvider => _inputEventProvider;
        protected PlayerParameters CurrentPlayerParameters => core.CurrentPlayerParameter;
        protected PlayerType PlayerType => core.Type;
        
        private void Start()
        {
            core = GetComponent<PlayerCore>();
            _inputEventProvider = GetComponent<IInputEventProvider>();
            
            
        }
    }
}
