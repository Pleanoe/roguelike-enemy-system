using System;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMoving : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _target;

    private float _timer;
    private float updateInterval = 0.2f;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    public void Init(Transform target,float speed,float stoppingDistance)
    {
        _target = target;
        _agent.speed = speed;
       _agent.stoppingDistance = stoppingDistance;
       _timer = 0;
       if (_agent.hasPath) _agent.ResetPath();
        Debug.Log("Скрипт ходьбы врага проинициализирован");
    }
    public void MoveToTarget()
    {
        if (_target == null) return;
        
        _timer += Time.deltaTime;

        if (_timer >= updateInterval)
        {
            _timer = 0;
            _agent.SetDestination(_target.position);
            
        }
    }

    public void StopMoving()
    {
        if (_agent.hasPath) _agent.ResetPath();
    }
}
