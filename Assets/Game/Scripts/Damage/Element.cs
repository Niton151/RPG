using System;

namespace Game.Scripts.Damage
{
    [Flags]
    public enum Element
    {
        Nomal = 1 << 0,
        Poison = 1 << 1
    }
}
