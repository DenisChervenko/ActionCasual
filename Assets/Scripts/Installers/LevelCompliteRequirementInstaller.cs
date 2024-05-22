using UnityEngine;
using Zenject;

public class LevelCompliteRequirementInstaller : MonoInstaller
{
    [SerializeField] private LevelCompleteRequirement _levelCompleteRequirement;
    public override void InstallBindings()
    {
        Container.Bind<LevelCompleteRequirement>().FromInstance(_levelCompleteRequirement).AsSingle().NonLazy();
    }
}