using UnityEngine;
using Zenject;

public class CombatBehaviourInstaller : MonoInstaller
{
    [SerializeField] private CombatBehaviour _combatBehaviour;
    public override void InstallBindings()
    {
        Container.Bind<CombatBehaviour>().FromInstance(_combatBehaviour).AsSingle().NonLazy();
    }
}