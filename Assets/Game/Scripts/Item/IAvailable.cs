using Game.Scripts.Player;

namespace Game.Scripts.Item
{
    public interface IAvailable
    {
        void PickedUp(PlayerCore player);
    }
}