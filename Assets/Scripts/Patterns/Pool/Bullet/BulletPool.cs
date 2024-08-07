using Patterns.Factory.Bullet;

namespace Patterns.Pool.Bullet
{
    public class BulletPool : BasePool<Bullets.Bullet>
    {
        private BulletFactory _factory;
        public void SetFactory(BulletFactory factory)
        {
            _factory = factory;
        }
        public override Bullets.Bullet CreateInstance()
        {
            var instance = _factory.Spawn();
            instance.Stop();
            return instance;
        }
    }
}
