using System;
using TMPro;
using UnityEngine;

public class HealthTracker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _healthText;

    private void OnEnable()
    {
        PlayerHealth.OnHealthChange += ChangeHealthText;
    }

    private void OnDisable()
    {
        PlayerHealth.OnHealthChange -= ChangeHealthText;
    }
    private void ChangeHealthText(int _health)
    {
        _healthText.SetText($"Health:\n{_health}");
    }
}
