using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{

    [SerializeField] private Sheep _sheepPrefab;

    public static Action OnSheepSpawned = delegate { };

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Sheep sheep = Instantiate(_sheepPrefab, transform.GetChild(i).position, Quaternion.identity);
        }
        OnSheepSpawned?.Invoke();
    }
}
