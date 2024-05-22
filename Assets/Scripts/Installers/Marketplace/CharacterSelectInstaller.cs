using UnityEngine;
using Zenject;

public class CharacterSelectInstaller : MonoInstaller
{
    [SerializeField] private CharacterSelect _characterSelect;
    public override void InstallBindings()
    {
        Container.Bind<CharacterSelect>().FromInstance(_characterSelect).AsSingle().NonLazy();
    }
}