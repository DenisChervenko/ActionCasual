using UnityEngine;
using Zenject;

public class EnemyDetectedInstaller : MonoInstaller
{
    [SerializeField] private EnemyDetected _enemyDetected;

    public override void InstallBindings()
    {
        Container.Bind<EnemyDetected>().FromInstance(_enemyDetected).AsSingle().NonLazy();
    }
}