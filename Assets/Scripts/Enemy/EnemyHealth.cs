using UnityEngine;
using System;
public class EnemyHealth : MonoBehaviour,IDamagable
{
    public event Action OnDied;
    
    private int _health;
    private bool _isDead;
    public int CurrentHealth => _health;

    public void TakeDamage(int damage)
    {
        if (_isDead) return;
        
        _health -= damage;
        Debug.Log("Враг получил урон, текущее хп: " + _health);
        if (_health <= 0)
        {
            Die();
        }
    }
    public void Init(int health)
    {
        _health = health;
        _isDead = false;
        Debug.Log("Скрипт здороавья проинициализирован!");
    }

    private void Die()
    {
        _isDead = true;
        OnDied?.Invoke();
    }
}
