using Interfaces;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Unit.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour, IMovable
    {
        public event Action MoveEndEvent;

        [SerializeField] private NavMeshAgent agent;

        private Vector3 _target;
        private bool _isMoving;
        private void Update()
        {
            if (_isMoving && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                _isMoving = false;
                MoveEndEvent?.Invoke();
                Debug.Log("End");
            }
        }
        public void Move(Vector3 target)
        {
            _target = target;
            _isMoving = true;
            agent.SetDestination(_target);
        }
    }
}
