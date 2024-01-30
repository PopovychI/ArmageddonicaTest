using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutualDestruction : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    private Animator _animator;
    private const string _die = "Die";

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    private async UniTask Destruct()
    {
        await UniTask.Delay(500);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_targetLayer & (1 << other.gameObject.layer)) != 0)
        {
            _animator.SetTrigger(_die);
            _ = Destruct();
            other.enabled = false;
        }
    }
}
