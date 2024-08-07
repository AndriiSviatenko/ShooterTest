using Patterns.Factory.Enemy;
using Unit.Enemy;

namespace Patterns.Pool.Enemy
{
    public class EnemyPool : BasePool<Controller>
    {
        private EnemyFactory _factory;
        public void SetFactory(EnemyFactory factory)
        {
            _factory = factory;
        }
        public override Controller CreateInstance()
        {
            var instance = _factory.Spawn();
            instance.Stop();
            return instance;
        }
    }
}

