using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MouseClickPositionHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{

    public System.Action<Vector3> OnGroundClick;
    public System.Action OnGroundPointerUp;

    private NavMeshSurface _surface;

    private Vector3 _lastClickPosition;
    private PointerEventData _lastEventData;
    private int _groundLayer;
    private bool _drag;
    private bool _delayed;
    private const int _delay = 100;

    private void Awake()
    {
        _groundLayer = LayerMask.NameToLayer("Ground");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _drag = true;
        _lastEventData = eventData;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnGroundPointerUp?.Invoke();
        _drag = false;
    }
    private void GetPosition(PointerEventData eventData)
    {
        if (_delayed) return;
        NavMeshHit hit;
        _lastClickPosition = eventData.pointerCurrentRaycast.worldPosition;
        _delayed = true;
        NavMesh.SamplePosition(_lastClickPosition, out hit, 15f, _groundLayer);
        _ = DelayBetweenSpawn();
        if (!hit.hit) return;
        OnGroundClick?.Invoke(hit.position);
    }
    public void OnPointerMove(PointerEventData eventData)
    {
        if (_drag) GetPosition(_lastEventData);
    }
    public async UniTask DelayBetweenSpawn()
    {
        await UniTask.Delay(_delay);
        _delayed = false;
        await UniTask.CompletedTask;
    }
    private void OnDestroy()
    {
        OnGroundPointerUp = null;
        OnGroundClick = null;
    }


}
