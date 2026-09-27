using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour,IDamagable
{
    private void Start()
    {
        OnHealthChange?.Invoke(_health);
    }
    public static event Action<int> OnHealthChange;
    private int _health = 100;
    public int CurentHealth => _health;
    public void TakeDamage(int damage)
    {
        _health -= damage;
        Debug.Log($"Здоровье игрока: {_health}");
        OnHealthChange?.Invoke(_health);
    }
}
