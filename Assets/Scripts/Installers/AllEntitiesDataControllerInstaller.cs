using UnityEngine;
using Zenject;

public class AllEntitiesDataControllerInstaller : MonoInstaller
{
    [SerializeField] private AllEntitiesDataController _dataController;
    public override void InstallBindings()
    {
        Container.Bind<AllEntitiesDataController>().FromInstance(_dataController).AsSingle().NonLazy();
    }

}