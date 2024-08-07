using Patterns.Pool.Bullet;
using Systems;
using Unit.Enemy;
using UnityEngine;

namespace Patterns.Factory.Enemy
{
    public class EnemyFactory : MultyPlay<Controller>
    {
        [SerializeField] private Transform[] spawnPoints;
        private WayPointSystem _wayPointSystem;
        private BulletPool _bulletPool;
        public void SetPool(BulletPool pool)
        {
            _bulletPool = pool;
        }
        public void SetWayPointSystem(WayPointSystem wayPointSystem)
        {
            _wayPointSystem = wayPointSystem;
        }
        public override Controller Spawn()
        {
            var instance = base.Spawn();
            instance.SetBulletPool(_bulletPool);
            instance.SetWalkPointSystem(_wayPointSystem);
            instance.SetPosition(spawnPoints[Random.Range(0, spawnPoints.Length)].position);
            return instance;
        }
    }
}

