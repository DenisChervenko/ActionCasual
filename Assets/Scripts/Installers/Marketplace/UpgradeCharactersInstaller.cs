using UnityEngine;
using Zenject;

public class UpgradeCharactersInstaller : MonoInstaller
{
    [SerializeField] private UpgradeCharacter _upgradeCharacter;
    public override void InstallBindings()
    {
        Container.Bind<UpgradeCharacter>().FromInstance(_upgradeCharacter).AsSingle().NonLazy();
    }
}