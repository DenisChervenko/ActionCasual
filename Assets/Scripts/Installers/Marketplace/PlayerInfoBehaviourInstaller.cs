using UnityEngine;
using Zenject;

public class PlayerInfoBehaviourInstaller : MonoInstaller
{
    [SerializeField] private PlayerInfoBehaviour _playerInfoBehaviour;
    public override void InstallBindings()
    {
        Container.Bind<PlayerInfoBehaviour>().FromInstance(_playerInfoBehaviour).AsSingle().NonLazy();
    }
}