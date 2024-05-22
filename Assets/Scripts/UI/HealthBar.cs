using UnityEngine.UI;
using UnityEngine;
using Zenject;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    private float _health;
    private float _maxHealth;

    [Inject] PlayerStats playerStats;

    private void Start()
    {
        _health = playerStats.Health;
        _maxHealth = _health;
        _healthBar.fillAmount = _health;
    }

    private void OnGetDamage(float damage)
    {
        _health -= damage;
        _healthBar.fillAmount = _health / _maxHealth;
    }

    private void OnEnable() => playerStats.OnGetDamageCallback += OnGetDamage;
    private void OnDisable() => playerStats.OnGetDamageCallback -= OnGetDamage;
}
