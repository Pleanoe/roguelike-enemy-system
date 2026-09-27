using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVision : MonoBehaviour
{
    public event Action<PlayerHealth> OnPlayerNearby;
    
    private float _timer;
    private float _updateInterval = 0.5f;
    private float _maxDistance = 1f;

    private Transform _target;
    private PlayerHealth _targetHealth;
    
    public bool isPlayerNearby { get; private set; }

    public void Init(float attackDistance,Transform target)
    {
        _maxDistance = attackDistance;
        _target = target;
        target.TryGetComponent(out _targetHealth);
    }
    public void CheckPlayer()
    {
        _timer += Time.deltaTime;
        if (_timer < _updateInterval) return;
        _timer = 0;
        float distance = Vector3.Distance(transform.position, _target.position);
        isPlayerNearby = distance <= _maxDistance;
        if (isPlayerNearby) OnPlayerNearby?.Invoke(_targetHealth);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isPlayerNearby ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _maxDistance);
    }
}

