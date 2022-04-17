namespace Game.Scripts.Damage
{
    public interface IAttacker
    {
        public void Attack(IDamageApplicable target);
    }
}
