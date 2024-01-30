using ProjectDawn.Navigation;
using ProjectDawn.Navigation.Hybrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;
using Unity.Entities.UniversalDelegates;
using Cysharp.Threading.Tasks;
using System.Linq;

public class Squad : MonoBehaviour
{
    public System.Action<Vector3> OnDestinationChanged;
    [SerializeField] private float _destinationNextRefreshTime = 0.5f;
    [SerializeField] private Vector3 _destination;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private LayerMask _friendlySquadLayer;
    [Space(20)]
    [SerializeField] private List<AgentAuthoring> _agents = new();
    [Space(20)]
    [SerializeField] private List<Transform> _targets = new();
    private Transform _currentTarget;
    private float _currentClosestDistance;

    public List<AgentAuthoring> Agents => _agents;
    private void Awake()
    {
        _destination = transform.position;
        _currentTarget = transform;
    }
    private void Start()
    {
        InvokeRepeating(nameof(UpdateDestination), 0, _destinationNextRefreshTime);
    }
    public void AddAgents(AgentAuthoring[] agents)
    {
        _agents.AddRange(agents);
    }
    public void AddAgent(AgentAuthoring agent)
    {
        _agents.Add(agent);
        agent.SetDestinationDeferred(_destination);
        transform.position = _agents[0].transform.position;
    }
    private void ChangeSquadDestination(Transform newTarget)
    {
        if (!newTarget.parent.gameObject.activeSelf) return;
        _destination = newTarget.position;
        _currentTarget = newTarget;
        for (int i = 0; i < _agents.Count; i++)
        {
            _agents[i].SetDestinationDeferred(_destination);
        }
    }
    private void  UpdateDestination()
    {
        _targets.RemoveAll((x => !x.parent.gameObject.activeSelf));
        if (_targets.Count == 0) _currentTarget = transform;
        else if (!_currentTarget.parent.gameObject.activeSelf) _currentTarget = _targets[0];
        for (int i = 0; i < _agents.Count; i++)
        {
            transform.position = _agents[0].transform.position;
            _agents[i].SetDestinationDeferred(_currentTarget.position);
        }
    }
    private void TryMergeSquads(Squad squad)
    {
        if (squad.Agents[0].DefaultBody.IsStopped)
        {
            for (int i = 0; i < squad.Agents.Count; i++)
            {
                squad.Agents[i].transform.parent = transform;
                _agents.AddRange(squad.Agents);
            }
        }
        squad.gameObject.SetActive(false);
    }
    private void AddToTargetList(Transform targetTransform)
    {
        _targets.RemoveAll((x => !x.parent.gameObject.activeSelf));
        CheckDistanceFromSquad(targetTransform);
        _targets.Add(targetTransform);
    }
    private void CheckDistanceFromSquad(Transform targetTransform)
    {
        var distance = Vector3.Distance(targetTransform.position, transform.position);
        if (!_currentTarget) _currentTarget = transform;
        if (_currentTarget == transform) _currentClosestDistance = 100000;
        if (distance < _currentClosestDistance)
        {
            _currentClosestDistance = distance;
            ChangeSquadDestination(targetTransform);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if ((_targetLayer & (1 << other.gameObject.layer)) != 0)
        {
            AddToTargetList(other.transform);
        }
        if ((_friendlySquadLayer & (1 << other.gameObject.layer)) != 0)
        {
            //  TryMergeSquads(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other) { _targets.Remove(_targets.Find(x => !x.transform.parent.gameObject.activeSelf)); }
        if ((_targetLayer & (1 << other.gameObject.layer)) != 0)
        {
            if (other.transform == _currentTarget)
            {
                _targets.Remove(_currentTarget);
                
            }
        }
    }

}

