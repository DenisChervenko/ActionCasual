using UnityEngine;
using Zenject;

public class CounterUpdateInstaller : MonoInstaller
{
    [SerializeField] private CounterUpdate _counterUpdate;
    public override void InstallBindings()
    {
        Container.Bind<CounterUpdate>().FromInstance(_counterUpdate).AsSingle().NonLazy();
    }
}