using Animation;
using Interfaces;
using Patterns.EventBus;
using Patterns.EventBus.Events;
using Patterns.Pool.Bullet;
using System;
using Systems;
using UnityEngine;

namespace Unit.Enemy
{
    public class Controller : MonoBehaviour, IPoolable<Controller>
    {
        public event Action<Controller> ReturnInPoolEvent;
        [SerializeField] private AudioClip shoot;
        [SerializeField] private Audio.Controller audioController;
        [SerializeField] private AI ai;
        [SerializeField] private AnimationController animationController;
        [SerializeField] private AnimationCallBack animationCallBack;
        [SerializeField] private Movement movement;
        [SerializeField] private RotateToTarget rotate;
        [SerializeField] private Attack attack;
        [SerializeField] private Health health;
        public Movement Movement => movement;
        public Health Health => health;
        public Attack Attack => attack;
        private bool _isAlive;
        private WayPointSystem _wayPointSystem;
        private BulletPool _bulletPool;

        public void SetWalkPointSystem(WayPointSystem wayPointSystem)
        {
            _wayPointSystem = wayPointSystem;
        }
        public void SetBulletPool(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }
        private void Start()
        {
            EventBus.Instance.Subscribe<Stop>((_) => ai.UpdateStop(true));
            EventBus.Instance.Subscribe<Restart>((_) => ai.UpdateStop(false));
        }

        private void Dead()
        {
            animationCallBack.DeadEvent += ReturnInPool;
        }

        public void Play()
        {
            ai.EnableTargetMove();
            attack.SetPool(_bulletPool);
            ai.AttackEvent += () =>
            {

                animationController.SetTrigger("Attack");
                attack.AttackPlayer();
                attack.AttackEvent += () => audioController.Play(shoot);

            };

            movement.MoveEndEvent += ai.UpdateTarget;
            ai.GetPointEvent += _wayPointSystem.GetPoint;
            ai.RotateEvent += rotate.Rotate;
            ai.MoveEvent += (_) =>
            {
                animationController.SetTrigger("Walk");
                movement.Move(_);
            };

            health.DeadEvent += () =>
            {
                ai.UpdateStop(true);
                ai.Stop();
                animationController.SetTrigger("Dead");
                Dead();

            };
            gameObject.SetActive(true);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void ReturnInPool()
        {
            ReturnInPoolEvent?.Invoke(this);
            EventBus.Instance.Invoke(new DeadEvent<Controller>(this));
        }

        public void Stop()
        {
            attack.SetPool(null);
            movement.MoveEndEvent -= ai.UpdateTarget;
            ai.GetPointEvent -= _wayPointSystem.GetPoint;
            ai.RotateEvent -= rotate.Rotate;
            gameObject.SetActive(false);
            ai.DisableTargetMove();
        }
        private void OnDestroy()
        {
            EventBus.Instance.Unsubscribe<Stop>((_) => ai.UpdateStop(true));
            EventBus.Instance.Unsubscribe<Restart>((_) => ai.UpdateStop(false));
        }
    }
}
