using UnityEngine;
using Zenject;

public class CharacterListInstaller : MonoInstaller
{
    [SerializeField] private CharacterList _characterList;
    public override void InstallBindings()
    {
        Container.Bind<CharacterList>().FromInstance(_characterList).AsSingle().NonLazy();
    }
}