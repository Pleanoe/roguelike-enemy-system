using UnityEngine;
using TMPro;
public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    private void OnEnable() =>  AmmoTracker.OnAmmoChange += UpdateAmmoUI;
    private void OnDisable() => AmmoTracker.OnAmmoChange -= UpdateAmmoUI;
    private void UpdateAmmoUI(int ammo)
    {
        ammoText.SetText("Ammo: {0}",ammo);
    }
}
