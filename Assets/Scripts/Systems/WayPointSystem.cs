using UnityEngine;

namespace Systems
{
    public class WayPointSystem : MonoBehaviour
    {
        [SerializeField] private float walkPointRange;
        private Vector3 _walkPoint;

        public Vector3 GetPoint()
        {
            SearchWalkPoint();
            return _walkPoint;
        }
        private void SearchWalkPoint()
        {
            var randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
            var randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

            Vector3 potentialWalkPoint = new Vector3(transform.position.x + randomX,
                transform.position.y, transform.position.z + randomZ);
            _walkPoint = potentialWalkPoint;

        }
    }
}