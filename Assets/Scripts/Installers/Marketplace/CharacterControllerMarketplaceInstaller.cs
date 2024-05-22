using UnityEngine;
using Zenject;

public class CharacterControllerMarketplaceInstaller : MonoInstaller
{
    [SerializeField] private CharacterControllerMarket _characterControllerMarket;
    public override void InstallBindings()
    {
        Container.Bind<CharacterControllerMarket>().FromInstance(_characterControllerMarket).AsSingle().NonLazy();
    }
}