using Patterns.EventBus;
using Patterns.EventBus.Events;
using System;
using UnityEngine;

namespace Inputs
{
    public class InputProvider : MonoBehaviour
    {
        public const string NAME_AXIS_HORIZONTAL = "Horizontal";
        public const string NAME_AXIS_VERTICAL = "Vertical";
        public const string NAME_AXIS_MOUSE_X = "Mouse X";
        public const string NAME_AXIS_MOUSE_Y = "Mouse Y";

        public event Action<Vector3> MovementEvent;
        public event Action<Vector3> MouseEvent;
        public event Action JumpClickEvent;
        public event Action ShootClickEvent;
        public event Action ReloadClickEvent;

        private Vector3 _direction;
        private bool _isStop;

        private void UpdateStop(bool value)
        {
            _isStop = value;
        }
        private void Start()
        {
            _direction = Vector3.zero;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            EventBus.Instance.Subscribe<Stop>((_) => UpdateStop(true));
            EventBus.Instance.Subscribe<Restart>((_) => UpdateStop(false));
        }

        private void Update()
        {
            if (_isStop) return;
            _direction = new Vector3(Input.GetAxis(NAME_AXIS_HORIZONTAL),
                0, Input.GetAxis(NAME_AXIS_VERTICAL));

            MouseEvent?.Invoke(new Vector3(Input.GetAxisRaw(NAME_AXIS_MOUSE_X), Input.GetAxisRaw(NAME_AXIS_MOUSE_Y)));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpClickEvent?.Invoke();
            }

            if (Input.GetMouseButtonDown(0))
            {
                ShootClickEvent?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadClickEvent?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            if (_isStop) return;
            MovementEvent?.Invoke(_direction);
        }
    }
}