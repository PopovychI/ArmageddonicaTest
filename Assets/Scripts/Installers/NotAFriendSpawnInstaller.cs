using UnityEngine;
using Zenject;

public class NotAFriendSpawnInstaller : MonoInstaller
{
    [SerializeField] private NotAFriendSpawner _spawner;
    public override void InstallBindings()
    {
        Container.Bind<NotAFriendSpawner>().FromInstance(_spawner).AsSingle().NonLazy();
    }
}