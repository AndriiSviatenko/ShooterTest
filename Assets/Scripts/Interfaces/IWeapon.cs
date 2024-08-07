using System;
using System.Collections;

namespace Interfaces
{
    public interface IWeapon
    {
        event Action ShootEvent;
        void Shoot();
        void Reload();
    }
}

