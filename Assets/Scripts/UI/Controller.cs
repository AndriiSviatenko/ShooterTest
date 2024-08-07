using Patterns.EventBus;
using Patterns.EventBus.Events;
using UI.Panels;
using UnityEngine;

namespace UI
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Dead deadPanel;
        [SerializeField] private BasePanel noAmmo;
        public void Init()
        {
            EventBus.Instance.Subscribe<DeadEvent<Unit.Player.Controller>>(ShowDeadPanel);
            EventBus.Instance.Subscribe<Restart>((_) =>
            {
                deadPanel.Hide();
                Cursor.lockState = CursorLockMode.Locked;
            });
            EventBus.Instance.Subscribe<AmmoStatus>((_) =>
            {
                if (_.IsHaveAmmo)
                {
                    noAmmo.Hide();
                }
                else
                {
                    noAmmo.Show();
                }
            });
        }
        private void ShowDeadPanel(DeadEvent<Unit.Player.Controller> deadEvent)
        {
            deadPanel.Show();
            Cursor.lockState = CursorLockMode.None;
            EventBus.Instance.Invoke(new Stop());
        }
    }
}

