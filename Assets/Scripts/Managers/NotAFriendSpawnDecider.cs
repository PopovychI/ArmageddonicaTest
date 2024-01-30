using Cysharp.Threading.Tasks;
using ProjectDawn.Navigation.Sample.Mass;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class NotAFriendSpawnDecider : MonoBehaviour
{
    [SerializeField] private int _spawnDelaySeconds = 10;
    [SerializeField] private int _spawnCount = 10;
    [SerializeField] private int _initalSpawnCount = 300;
    [SerializeField] private List<Transform> _spawnPoints;   
    private int _spawnDelay;
    
    [Inject] private NotAFriendSpawner _spawner;

    private void Start()
    {
        _spawnDelay = _spawnDelaySeconds * 1000;
        _ = BeginLoop();
        for (int i = 0; i < _initalSpawnCount/_spawnCount; i++)
        {
            var rnd = Random.Range(0, _spawnPoints.Count);
         _spawner.SpawnEntity(_spawnPoints[rnd].position, _spawnCount);
        }
    }
    private async UniTask BeginLoop()
    {
        while (true)
        {
            var rnd = Random.Range(0, _spawnPoints.Count);
            _spawner.SpawnEntity(_spawnPoints[rnd].position, _spawnCount);
            await UniTask.Delay(_spawnDelay);
        }
    }
}
