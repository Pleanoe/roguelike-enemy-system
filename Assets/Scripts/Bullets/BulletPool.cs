using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int poolSize = 20;
    [SerializeField] private GameObject bulletPrefab;

    private Queue<GameObject> bullets = new Queue<GameObject>();
    
    private void Awake()
    {
        InitializePool();
    }
    private void OnEnable() => BulletsManager.OnStartShoot += SetBullet;
    private void OnDisable() => BulletsManager.OnStartShoot -= SetBullet;
        
    private void InitializePool() //Инициализация пула
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewBulletInPool();
        }
    }

    private GameObject CreateNewBulletInPool()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation, transform);
        if (bullet.TryGetComponent<Bullet>(out var bulletScript))
        {
            bulletScript.SetPool(this);
        }

        bullet.SetActive(false);
        bullets.Enqueue(bullet);
        return bullet;
    }

    public void SetBullet(Transform spawnPosition) //Выстрел
    {
        if (bullets.Count == 0) //Если пул закончился, расширение его
        {
            CreateNewBulletInPool();
        }
        GameObject bullet = bullets.Dequeue(); //Достать пулю
        
        bullet.transform.position = spawnPosition.position;
        bullet.transform.rotation = spawnPosition.rotation;
        bullet.SetActive(true); //Вкючение пули
    }

    public void ReturnToPool(GameObject bullet) //Вернуть пулю в пул
    {
        bullet.SetActive(false);
        bullets.Enqueue(bullet);
    }
}