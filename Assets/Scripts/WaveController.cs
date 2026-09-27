using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private List<WaveData> _waves;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private float _delayBetweenWaves = 3f;

    public event Action<int> OnWaveStarted;
    public event Action OnAllWavesCompleted;

    private int _currentWaveIndex = -1;
    private int _enemiesAliveInWave;

    private float _timer;
    private bool _waitingForNextWave;

    private void OnEnable()
    {
        // Переиспользуем событие, которое у тебя уже есть для пула - не нужно ничего дублировать
        EnemyBrain.OnBrainDied += HandleEnemyDied;
    }

    private void OnDisable()
    {
        EnemyBrain.OnBrainDied -= HandleEnemyDied;
    }

    private void Start()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (!_waitingForNextWave) return;

        _timer += Time.deltaTime;
        if (_timer >= _delayBetweenWaves)
        {
            _waitingForNextWave = false;
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        _currentWaveIndex++;
        if (_currentWaveIndex >= _waves.Count)
        {
            OnAllWavesCompleted?.Invoke();
            return;
        }

        WaveData wave = _waves[_currentWaveIndex];
        _enemiesAliveInWave = 0;

        foreach (EnemyGroup group in wave.enemies)
        {
            for (int i = 0; i < group.countEnemy; i++)
            {
                _spawner.SpawnEnemy(group.enemyPrefab);
                _enemiesAliveInWave++;
            }
        }

        OnWaveStarted?.Invoke(_currentWaveIndex);
    }

    private void HandleEnemyDied(EnemyBrain enemy)
    {
        // Обрати внимание: это событие статическое (мы обсуждали это в EnemyBrain) -
        // тут это НЕ баг, а ровно то поведение, что нужно: одна подписка на весь контроллер,
        // а не на каждого врага отдельно.
        _enemiesAliveInWave--;
        if (_enemiesAliveInWave <= 0)
        {
            _timer = 0f;
            _waitingForNextWave = true;
        }
    }
}