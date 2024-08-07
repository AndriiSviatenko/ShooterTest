namespace Patterns.EventBus.Events
{
    public class AmmoStatus
    {
        public bool IsHaveAmmo { get; private set; }
        public AmmoStatus(bool value)
        {
            IsHaveAmmo = value;
        }
    }
}
