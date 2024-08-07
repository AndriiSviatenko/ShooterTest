using Patterns.EventBus;
using Patterns.EventBus.Events;
using Patterns.Pool.Enemy;
using System.Collections;
using System.Collections.Generic;
using Unit.Enemy;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private EnemyPool bulletPool;
    [SerializeField] private int maxEnemyCount;
    [SerializeField] private float delay;

    private List<Controller> _enemies = new List<Controller>();
    private Coroutine _coroutine;
    private int _currentEnemyCount;
    private bool _isStop;
    public void Init()
    {
        _coroutine = StartCoroutine(UpdateCoroutine());

        EventBus.Instance.Subscribe<DeadEvent<Controller>>((_) => _currentEnemyCount--);
        EventBus.Instance.Subscribe<Restart>(Restart);
        EventBus.Instance.Subscribe<Stop>(Stop);
    }
    public void Restart(Restart restart)
    {
        _enemies.RemoveAll(controller =>
        {
            controller.ReturnInPool();
            return true;
        });
        _isStop = false;
        _coroutine = StartCoroutine(UpdateCoroutine());
    }
    
    private void Stop(Stop stop)
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);
    }
    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            yield return null;
            if (_currentEnemyCount >= maxEnemyCount) continue;
            var enemy = bulletPool.Get();
            _currentEnemyCount++;
            enemy.Play();
            _enemies.Add(enemy);
            yield return new WaitForSeconds(delay);
        }
        
    }
}
