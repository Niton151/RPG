using Game.Scripts.Player.MyInput;
using UnityEngine;

namespace Game.Scripts.Player
{
    public abstract class BasePlayerComponent : MonoBehaviour
    {
        private IInputEventProvider _inputEventProvider;
        
        protected PlayerCore core;
        protected IInputEventProvider InputEventProvider => _inputEventProvider;
        protected BaseParameter CurrentParameters => core.CurrentParameter;
        protected PlayerType PlayerType => core.Type;
        
        private void Start()
        {
            core = GetComponent<PlayerCore>();
            _inputEventProvider = GetComponent<IInputEventProvider>();
            
            
        }
    }
}
