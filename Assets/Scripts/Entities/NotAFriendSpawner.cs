using ProjectDawn.Navigation.Hybrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NotAFriendSpawner : MonoBehaviour
{
    [SerializeField] private Squad _squadPrefab;
    [SerializeField] private AgentAuthoring _enemyPrefab;

    [Inject] private DiContainer _diCont;
    [Inject] private AllEntitiesDataController _dataController;
    public void SpawnEntity(Vector3 position, int count)
    {
        if (_dataController.isEnemiesOverMaxCount) return;
        var squad = _diCont.InstantiatePrefabForComponent<Squad>(_squadPrefab);
        squad.transform.position = position;
        for (int i = 0; i < count; i++)
        {

            var agent = _diCont.InstantiatePrefabForComponent<AgentAuthoring>(_enemyPrefab);
            agent.transform.position = position;
            squad.AddAgent(agent);

        }

    }

}
