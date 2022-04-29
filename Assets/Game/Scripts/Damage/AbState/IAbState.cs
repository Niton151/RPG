namespace Game.Scripts.Damage.AbState
{
    public interface IAbState
    {
        float Duration { get; }

        void Activate();
    }
}
