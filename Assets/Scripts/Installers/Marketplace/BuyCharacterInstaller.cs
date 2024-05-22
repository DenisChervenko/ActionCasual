using UnityEngine;
using Zenject;

public class BuyCharacterInstaller : MonoInstaller
{
    [SerializeField] private BuyCharacter _buyCharacter;
    public override void InstallBindings()
    {
        Container.Bind<BuyCharacter>().FromInstance(_buyCharacter).AsSingle().NonLazy();
    }
}