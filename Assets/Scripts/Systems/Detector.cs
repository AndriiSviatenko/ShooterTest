using UnityEngine;

namespace Systems
{
    public class Detector : MonoBehaviour
    {
        [SerializeField] private LayerMask whatIsTarget;
        [SerializeField] private float sightRange;
        [SerializeField] private float attackRange;

        private bool _isSight;
        private bool _isAttack;
        private Transform _target;
        public bool IsTargetInSightRange()
        {
            _isSight = CheckForEnemiesOrTarget(sightRange);
            return CheckForEnemiesOrTarget(sightRange);
        }

        public bool IsTargetInAttackRange()
        {
            _isAttack = CheckForEnemiesOrTarget(attackRange);
            return CheckForEnemiesOrTarget(attackRange);
        }

        public Transform FindTarget()
        {
            Collider[] hits = 
                Physics.OverlapSphere(transform.position, sightRange, whatIsTarget);

            foreach (Collider hit in hits)
            {
                if (hit.transform != transform)
                {
                    _target = hit.transform;
                    return _target;
                }
            }

            return null;
        }

        private bool CheckForEnemiesOrTarget(float range)
        {
            Collider[] colliders = 
                Physics.OverlapSphere(transform.position, range, whatIsTarget);
            foreach (var collider in colliders)
            {
                if (collider.transform != transform)
                {
                    return true;
                }
            }
            return false;
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, sightRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
