namespace Game.Scripts.Damage.AbState
{
    public class Poison : IAbState
    {
        public float Duration { get; }

        public Poison(float duration)
        {
            Duration = duration;

            Activate();
        }

        public void Activate()
        {
            
        }
    }
}
