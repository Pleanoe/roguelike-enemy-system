using System;
using System.Collections;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{
    public static event Action<Transform> OnStartShoot;

    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] Transform firePoint;
    [SerializeField] private AmmoTracker ammoTracker;
    
    private WaitForSeconds delay;
    private bool isCooldown = false;

    private void Awake()
    {
        delay = new WaitForSeconds(fireRate);
    }
    private void OnEnable() => PlayerRotater.OnReadyToShoot += TryShoot;
    private void OnDisable() => PlayerRotater.OnReadyToShoot -= TryShoot;
    public void TryShoot()
    {
        if (!isCooldown && ammoTracker.CurrentAmmo > 0)
        {
            StartCoroutine(ShootRoutine());
        }
    }
    public IEnumerator ShootRoutine()
    {
        isCooldown = true;
        OnStartShoot?.Invoke(firePoint);

        yield return delay;
        isCooldown = false;
    }
}

