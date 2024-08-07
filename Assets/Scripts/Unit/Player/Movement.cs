using UnityEngine;

namespace Unit.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float gravity = Physics.gravity.y;
        [SerializeField] private float jumpHeight = 1.5f;
        [SerializeField] private float _groundDistance = 0.4f;
        private Vector3 _velocity;
        private bool _isGrounded;

        private void Update()
        {
            ApplyGravity();
        }

        public void ApplyGravity()
        {
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -1f;
            }
            _velocity.y += gravity * Time.deltaTime;
            controller.Move(_velocity * Time.deltaTime);
        }


        public void MovePlayer(Vector3 direction)
        {
            var newDirection = transform.right * direction.x + transform.forward * direction.z;
            controller.Move(newDirection * speed * Time.deltaTime);
        }

        public void TryJump()
        {
            if (controller.isGrounded)
            {
                Jump();
            }
        }
        private void Jump()
        {
            _velocity.y = jumpHeight;
        }
    }
}

