using System;

namespace Interfaces
{
    public interface IDamagable
    {
        void TakeDamage(float damage);
        bool IsEnemy { get; }
    }
}
