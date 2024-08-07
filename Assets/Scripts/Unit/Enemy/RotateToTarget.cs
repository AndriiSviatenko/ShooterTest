using Interfaces;
using System.Collections;
using UnityEngine;

namespace Unit.Enemy
{
    public class RotateToTarget : MonoBehaviour, IRotateable
    {
        [SerializeField] private float speed;

        public void Rotate(Vector3 target)
        {
            Vector3 directionToTarget = target - transform.position;
            directionToTarget.y = 0;

            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = 
                    Quaternion.LookRotation(directionToTarget);
                transform.rotation = 
                    Quaternion.Slerp(transform.rotation, targetRotation, 
                    Time.deltaTime * speed);
            }
        }
    }
}