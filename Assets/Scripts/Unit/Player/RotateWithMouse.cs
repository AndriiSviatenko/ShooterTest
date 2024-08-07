using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Unit.Player
{
    public class RotateWithMouse : MonoBehaviour, IRotateable
    {
        [SerializeField] private float mouseSensitivity = 100f;
        [SerializeField] private Transform camera;
        private float xRotation = 0f;

        public void Rotate(Vector3 target)
        {
            var newInput = target * mouseSensitivity * Time.deltaTime;
            xRotation -= newInput.y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * newInput.x);
        }
    }
}

