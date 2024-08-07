using System;
using Systems;
using UnityEngine;

namespace Unit.Enemy
{
    public class AI : MonoBehaviour
    {
        public event Func<Vector3> GetPointEvent;
        public event Action<Vector3> MoveEvent;
        public event Action<Vector3> RotateEvent;
        public event Action AttackEvent;

        [SerializeField] private Detector detector;

        private Transform _targetMove;
        private Transform _detectorTarget;
        private Vector3? _newPos;
        private bool _isStop;
        private void Start()
        {
            _newPos = GetPointEvent?.Invoke();
            _targetMove = new GameObject("Target").transform;
        }
        public void EnableTargetMove()
        {
            _targetMove?.gameObject.SetActive(true);
        }
        public void DisableTargetMove()
        {
            _targetMove?.gameObject.SetActive(false);
        }
        public void UpdateTarget()
        {
            if (_isStop) return;
            _newPos = GetPointEvent?.Invoke();
            _targetMove.position = _newPos.Value;
        }
        public void UpdateStop(bool value)
        {
            _isStop = value;
        }
        private void Update()
        {
            if (_isStop) return;

            bool playerIsInSightRange = detector.IsTargetInSightRange();
            bool playerIsInAttackRange = detector.IsTargetInAttackRange();

            if (!playerIsInSightRange && !playerIsInAttackRange)
            {
                MoveEvent?.Invoke(_targetMove.position);
                RotateEvent?.Invoke(_targetMove.position);

            }
            else if (playerIsInSightRange && !playerIsInAttackRange)
            {
                _detectorTarget = detector.FindTarget();
                if (_targetMove != null)
                {
                    MoveEvent?.Invoke(_detectorTarget.position);
                }
            }
            else if (playerIsInSightRange && playerIsInAttackRange)
            {
                if (_detectorTarget != null)
                {
                    RotateEvent?.Invoke(_detectorTarget.position);
                    AttackEvent?.Invoke();
                }
            }
        }
        public void Stop()
        {
            MoveEvent?.Invoke(transform.position);
        }
    }
}