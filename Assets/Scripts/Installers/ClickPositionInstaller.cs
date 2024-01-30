using UnityEngine;
using Zenject;

public class ClickPositionInstaller : MonoInstaller
{
    [SerializeField] private MouseClickPositionHandler _clickHandler;
    public override void InstallBindings()
    {
        Container.Bind<MouseClickPositionHandler>().FromInstance(_clickHandler).AsSingle().NonLazy();
    }
}