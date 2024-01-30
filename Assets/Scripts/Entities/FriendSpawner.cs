using ProjectDawn.Navigation.Hybrid;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class FriendSpawner : MonoBehaviour
{
    [SerializeField] private List<AgentAuthoring> _friendPrefabs;
    [SerializeField] private Squad _squadPrefab;
    [SerializeField] private int _maxUnitsInSquad = 10;
    [SerializeField] private float _minDistanceForSquad = 8f;

    private int _unitsInSquadCount = 0;
    private int _currentFriendType = 0;
    private Squad _currentSquad;

    private Vector3 _lastSpawnPosition;

    [Inject] private MouseClickPositionHandler _posHandler;
    [Inject] private DiContainer _diCont;

    private void Awake()
    {
        _posHandler.OnGroundClick += SpawnEntity;
        _posHandler.OnGroundPointerUp += (() => _unitsInSquadCount = 0);
    }

    // I decided to copy what they are doing in an example, but i'd rather do some sort of factory
    public void SpawnEntity(Vector3 position)
    {
        if (_unitsInSquadCount == 0)
        {
            _currentFriendType = Random.Range(0, _friendPrefabs.Count);
            _currentSquad = Instantiate(_squadPrefab, position, Quaternion.identity);
            _lastSpawnPosition = position;
        }
        var friend = _diCont.InstantiatePrefabForComponent<AgentAuthoring>(_friendPrefabs[_currentFriendType]);
        friend.transform.position = position;
        _currentSquad.AddAgent(friend);
        _unitsInSquadCount++;

        if (_unitsInSquadCount == _maxUnitsInSquad || Vector3.Distance(_lastSpawnPosition, position) > _minDistanceForSquad) _unitsInSquadCount = 0;
    }
    

    

    private void OnDestroy()
    {
        _posHandler.OnGroundClick -= SpawnEntity;
    }
}
