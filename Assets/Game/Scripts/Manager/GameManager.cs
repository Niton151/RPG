using Cysharp.Threading.Tasks;
using Game.Scripts.Player;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerType initPlayerType;

        public ReactiveProperty<GameState> State => _state;
        private ReactiveProperty<GameState> _state = new ReactiveProperty<GameState>();
        public GameInitializer Initializer => _initializer;
        private GameInitializer _initializer = new GameInitializer();

        private async void Start()
        {
            Initializer.Initialize(initPlayerType);
            
            _state.Value = GameState.Initializing;
            
            await Initializer.OnInitFinished.ToUniTask();

            _state.Value = GameState.Town;
        }

        public void AddState(GameState state)
        {
            _state.Value |= state;
        }

        public void RemoveState(GameState state)
        {
            _state.Value &= ~state;
        }
    }
}
