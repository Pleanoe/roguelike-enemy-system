using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float speed = 10f;
    
    private Rigidbody rb;
    private BulletPool bulletPool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void SetPool(BulletPool bulletPool)
    {
        this.bulletPool = bulletPool;
    }

    private void OnEnable()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = transform.forward * speed;
        
        CancelInvoke(nameof(DespawnBullet));
        Invoke(nameof(DespawnBullet), lifeTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            if (other.TryGetComponent<IDamagable>(out IDamagable enemy))
            {
                enemy.TakeDamage(25);
            }
            DespawnBullet();
        }
    }
    private void DespawnBullet()
    {
        CancelInvoke(nameof(DespawnBullet));
        rb.linearVelocity = Vector3.zero;
        bulletPool.ReturnToPool(gameObject);
    }
}
