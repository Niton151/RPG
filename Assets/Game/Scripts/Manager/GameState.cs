using System;

namespace Game.Scripts.Manager
{
    [Flags]
    public enum GameState
    {
        Initializing = 1 << 0,
        Battle = 1 << 1, 
        Field = 1 << 2,
        Town = 1 << 3,
        Menu = 1 << 4,
        Talking = 1 << 5
    }
}
