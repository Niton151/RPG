using System;
using Game.Scripts.Player.Input;
using UniRx;

namespace Game.Scripts.Player
{
    public class PlayerMover : BasePlayerComponent, IInputEventProvider
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public IObservable<Unit> OnAction { get; }
    }
}
