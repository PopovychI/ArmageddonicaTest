using ProjectDawn.Navigation.Hybrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEntitiesDataController : MonoBehaviour
{
    [SerializeField] private int _maxEnemiesCount = 300;
    [SerializeField] private List<AgentAuthoring> _allEnemies;
    [SerializeField] private List<AgentAuthoring> _allFriends;

    public bool isEnemiesOverMaxCount => _allEnemies.Count >= _maxEnemiesCount;
    public int CurrentEnemiesCount => _allEnemies.Count;
    public int CurrentFriendsCount => _allFriends.Count;
    public int CurrentFriendsDead { get; set; }
    public int CurrentEnemiesDead { get; set; }

    public void RegisterEnemy(AgentAuthoring enemy)
    {
        _allEnemies.Add(enemy);
    }
    public void RegisterFriend(AgentAuthoring friend)
    {
        _allFriends.Add(friend);
    }
    public void RemoveEnemy(AgentAuthoring enemy)
    {
        _allEnemies.Remove(enemy);
        CurrentEnemiesDead++;
    }
    public void RemoveFriend(AgentAuthoring friend)
    {
        _allFriends.Remove(friend);
        CurrentFriendsDead++;
    }
}
