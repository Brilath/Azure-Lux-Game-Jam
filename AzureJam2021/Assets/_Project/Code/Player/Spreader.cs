using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Motor))]
public class Spreader : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _onFire;
    private Sheep _currentSheep;
    private Hazard _currentHazard;
    private Motor _motor;

    private Coroutine _spreaderCoroutine;

    public static Action<Sheep, Hazard> OnSpreadHazard = delegate { };

    private void Awake()
    {
        HazardObject.OnSpreadHazard += HandleSpreadHazard;
        Spreader.OnSpreadHazard += HandleSpreadHazard;
        _currentSheep = GetComponent<Sheep>();
        _motor = GetComponent<Motor>();
    }
    private void OnDestroy()
    {
        HazardObject.OnSpreadHazard -= HandleSpreadHazard;
        Spreader.OnSpreadHazard -= HandleSpreadHazard;
    }

    private void HandleSpreadHazard(Sheep sheep, Hazard hazard)
    {
        if (_currentSheep != sheep) return;
        if (_spreaderCoroutine != null) return;        

        Debug.Log($"{gameObject.name} is spreading {hazard.Name} now");
        StartCoroutine(StartSpreading(hazard));
    }

    private IEnumerator StartSpreading(Hazard hazard)
    {
        _currentHazard = hazard;
        _motor.CanBeControlled = false;
        EnableParticles(hazard);
        float time = 0;
        Sheep targetSheep = GetNearestTarget();

        if(targetSheep != null)
            _motor.HeadTowards(targetSheep);

        while (time < hazard.Duration)
        {
            yield return new WaitForSeconds(1);
            time++;
        }

        DisableParticles(hazard);
        _motor.HeadForward();
        _motor.CanBeControlled = true;
        _currentHazard = null;
        Debug.Log($"{gameObject.name} has stopped spreading {hazard.Name}");
    }

    private Sheep GetNearestTarget()
    {
        Sheep nearestSheep = null;
        float distance = Mathf.Infinity;
        
        Sheep[] foundSheeps = FindObjectsOfType<Sheep>();
        List<Sheep> flock = foundSheeps.ToList();
        
        if (flock.Contains(_currentSheep))
            flock.Remove(_currentSheep);

        if (flock.Count <= 0) return nearestSheep;
        
        foreach (Sheep sheep in flock)
        {
            float tempDistance = Vector2.Distance(transform.position, sheep.transform.position);
            if (distance > tempDistance)
            {
                nearestSheep = sheep;
                distance = tempDistance;
            }
        }

        return nearestSheep;
    }

    private void EnableParticles(Hazard hazard)
    {
        var main = _particleSystem.main;
        main.startColor = hazard.ParticleColor;
        _particleSystem.gameObject.SetActive(true);

        if (hazard.Type == HazardType.Fire)
        {
            _onFire.SetActive(true);
        }
    }

    private void DisableParticles(Hazard hazard)
    {
        _particleSystem.gameObject.SetActive(false);
        if(hazard.Type == HazardType.Fire)
        {
            _onFire.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_currentHazard == null) return;

        Debug.Log($"You hit something {collision.gameObject.name}!");
        Sheep sheep = collision.gameObject.GetComponent<Sheep>();
        if (sheep == null) return;

        ItemHolder itemHolder = sheep.GetComponent<ItemHolder>();
        Health health = sheep.GetComponent<Health>();
        if (itemHolder.CurrentItem == null ||
            itemHolder.CurrentItem.ItemType != _currentHazard.ItemCounter)
        {
            Debug.Log($"Applying {_currentHazard.Damage} damage to {sheep.name}");
            if (_currentHazard.IsDamageOverTime)
            {
                health.ModifyHealthOverTime(_currentHazard.Damage, _currentHazard.Duration);
                OnSpreadHazard?.Invoke(sheep, _currentHazard);
            }
            else
            {
                health.ModifyHealth(_currentHazard.Damage);
            }
        }
    }
}