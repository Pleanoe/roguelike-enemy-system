using System;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public static event Action<EnemyBrain> OnBrainDied;
    
    [Header("Config")] 
    private PlayerHealth _cachedPlayerHealth;
    [SerializeField] private EnemyData _data; //Данные, конфиг врага 

    [Header("Components")] [SerializeField]
    private EnemyMoving _enemyMoving;
    private EnemyHealth _enemyHealth;
    private EnemyVision _enemyVision;
    private EnemyAttack _enemyAttack;
    
    public EnemyBrain SourcePrefab { get; private set; }
    
    [SerializeField] Transform _targetToMove; //Игрок

    private void Awake()
    {
        _enemyMoving = GetComponent<EnemyMoving>(); //Получение компонентов врага
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyVision = GetComponent<EnemyVision>();
        _enemyAttack = GetComponent<EnemyAttack>();
        Debug.Log($"Attack component: {_enemyAttack}");
    }
    private void OnEnable()
    {
        _enemyHealth.OnDied += HandleDeath;
        _enemyVision.OnPlayerNearby += HandlePlayerNearby;
    }

    private void OnDisable()
    {
        _enemyHealth.OnDied -= HandleDeath;
        _enemyVision.OnPlayerNearby -= HandlePlayerNearby;
    }

    public void Init(Transform target, EnemyData data) //Инициализация мозга врага
    {
        _data = data;
        _targetToMove = target;
        
        _enemyMoving.Init(_targetToMove,_data.moveSpeed, _data.attackDistance);
        _enemyHealth.Init(_data.maxHealth);
        _enemyVision.Init(_data.attackDistance,_targetToMove);
        _enemyAttack.Init(_data.fireRate, _data.damage);
    }
    private void Update()
    {
        _enemyVision.CheckPlayer();
        if (_enemyVision.isPlayerNearby)
        {
            _enemyMoving.StopMoving();
            if (_cachedPlayerHealth != null)
            {
                _enemyAttack.TryAttackPlayer(_cachedPlayerHealth);
            }
        }
        else
        {
            _enemyMoving.MoveToTarget();
        }
    }
    public void SetSourcePrefab(EnemyBrain prefab)
    {
        SourcePrefab = prefab;
    }
    public void HandleDeath()
    {
        OnBrainDied?.Invoke(this);
    }
    private void HandlePlayerNearby(PlayerHealth playerHealth)
    {
        _cachedPlayerHealth = playerHealth; // просто запоминаем ссылку, атаку сюда больше не суём
    }
}
