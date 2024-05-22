using UnityEngine;
using Zenject;

public class ButtonStateInstaller : MonoInstaller
{
    [SerializeField] private ButtonStateController _buttonStateController;
    public override void InstallBindings()
    {
        Container.Bind<ButtonStateController>().FromInstance(_buttonStateController).AsSingle().NonLazy();
    }
}