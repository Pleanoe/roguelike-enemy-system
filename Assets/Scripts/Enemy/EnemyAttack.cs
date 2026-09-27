using System;
using UnityEngine;
public class EnemyAttack : MonoBehaviour
{
    private float _timer = 0f;
    private float _attackRate;
    private int _damage;
    public void Init(float attackRate, int damage)
    {
        _attackRate = attackRate;
        _damage = damage;
        _timer = 0;
    }

    public void TryAttackPlayer(PlayerHealth playerHealth)
    {
        _timer += Time.deltaTime;
        Debug.Log($"Таймер скрипта атаки: " + _timer);
        if (_timer >= _attackRate)
        {
            Debug.Log($"Attack: таймер {_timer}/{_attackRate}");
            playerHealth.TakeDamage(_damage);
            _timer = 0;
        }
    }
}
