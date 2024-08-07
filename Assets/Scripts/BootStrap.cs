using Patterns.Factory.Bullet;
using Patterns.Factory.Enemy;
using Patterns.Pool.Bullet;
using Patterns.Pool.Enemy;
using Systems;
using UI;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private BulletFactory bulletFactory;
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private Controller uiController;
    [SerializeField] private Game game;
    [SerializeField] private WayPointSystem wayPointSystem;
    private void Awake()
    {

        enemyFactory.SetPool(bulletPool);
        enemyFactory.SetWayPointSystem(wayPointSystem);

        enemyPool.SetFactory(enemyFactory);
        enemyPool.Init();


        bulletPool.SetFactory(bulletFactory);
        bulletPool.Init();
    }
    private void Start()
    {
        uiController.Init();
        game.Init();
    }
}
