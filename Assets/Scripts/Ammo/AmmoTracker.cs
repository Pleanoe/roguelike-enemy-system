using System;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoTracker : MonoBehaviour
{
    public static event Action<int> OnAmmoChange;
    public int CurrentAmmo {get; private set;}

    private void OnEnable() => BulletsManager.OnStartShoot += RemoveAmmo;
    private void OnDisable() => BulletsManager.OnStartShoot -= RemoveAmmo;
       
    private void Start()
    { 
        CurrentAmmo = 30;
        OnAmmoChange?.Invoke(CurrentAmmo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddAmmo(5);
            OnAmmoChange?.Invoke(CurrentAmmo);
        }
    }
    public void AddAmmo(int amount) //Добавить патроны
    {
       CurrentAmmo += amount;
    }
    public void RemoveAmmo(Transform firePoint) //Выстрелить или убрать патроны
    {
        if (CurrentAmmo > 0)
        {
            CurrentAmmo--;
            OnAmmoChange?.Invoke(CurrentAmmo);
        }
    }
}
