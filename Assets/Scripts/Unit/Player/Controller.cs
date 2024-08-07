using Inputs;
using Patterns.EventBus;
using Patterns.EventBus.Events;
using Systems;
using UnityEngine;
using Weapon;

namespace Unit.Player
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private AudioClip shoot;
        [SerializeField] private AudioClip reload;
        [SerializeField] private Audio.Controller audioController;
        [SerializeField] private InputProvider inputProvider;
        [SerializeField] private Movement movement;
        [SerializeField] private RotateWithMouse rotate;
        [SerializeField] private Health health;
        [SerializeField] private Range weapon;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            inputProvider.MovementEvent += movement.MovePlayer;
            inputProvider.MouseEvent += rotate.Rotate;
            inputProvider.JumpClickEvent += movement.TryJump;

            weapon.ShootEvent += () =>
            {
                audioController.Play(shoot);
            };
            weapon.StartReloadEvent += () =>
            {
                audioController.Play(reload);
            };

            health.DeadEvent += () => EventBus.Instance?.Invoke
            (new DeadEvent<Controller>(this));
        }
        private void OnDestroy()
        {
            inputProvider.MovementEvent -= movement.MovePlayer;
            inputProvider.MouseEvent -= rotate.Rotate;
            inputProvider.JumpClickEvent -= movement.TryJump;
        }
    }
}