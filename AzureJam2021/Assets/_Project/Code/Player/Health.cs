using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float _maxLife;
    [SerializeField] private float _currentLife;

    [Header("UI")]
    [SerializeField] private Image _healthBarImage;

    [Header("Death")]
    [SerializeField] private GameObject _deathPrefab;

    public static Action<GameObject> OnDeath = delegate { };
    public static Action<GameObject> OnDamageTaken = delegate { };

    private void Awake()
    {
        ApplyAmount(_maxLife);
    }

    public void ModifyHealth(float amount)
    {
        ApplyAmount(amount);
        if (amount < 0)
            OnDamageTaken?.Invoke(gameObject);
    }

    public void ModifyHealthOverTime(float amount, float duration)
    {
        StartCoroutine(ApplyAmountOverTime(amount, duration));
        if (amount < 0)
            OnDamageTaken?.Invoke(gameObject);
    }

    private void ApplyAmount(float amount)
    {
        _currentLife += amount;
        _currentLife = Mathf.Clamp(_currentLife, 0, _maxLife);

        _healthBarImage.fillAmount = _currentLife / _maxLife;

        if (_currentLife <= 0)
        {
            Debug.Log("Shit this sheep died");
            Instantiate(_deathPrefab, transform.position, Quaternion.identity);
            OnDeath?.Invoke(gameObject);
            Destroy(gameObject);
        }        
    }

    private IEnumerator ApplyAmountOverTime(float amount, float duration)
    {
        float time = 0;
        float modifyAmount = amount / duration;
        while (time < duration)
        {
            ApplyAmount(modifyAmount);
            time++;
            yield return new WaitForSeconds(1);            
        }
    }
}
