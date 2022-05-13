using System;

namespace Game.Scripts.Damage.AbState
{
    public interface IAbState : IDisposable
    {
        void Activate();
    }
}
