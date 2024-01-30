using ProjectDawn.Navigation.Hybrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EntityRegistrator : MonoBehaviour
{
    private AgentAuthoring _agent;
    [Inject] private AllEntitiesDataController _dataController;
    [SerializeField] private bool _isFriend; // for simplicity
    private void Awake()
    {
        _agent = GetComponent<AgentAuthoring>();
    }
    private void OnEnable()
    {
        if (_isFriend) _dataController.RegisterFriend(_agent);
        else _dataController.RegisterEnemy(_agent);
    }
    private void OnDisable()
    {
        if (_isFriend) _dataController.RemoveFriend(_agent);
        else _dataController.RemoveEnemy(_agent);
    }
}
