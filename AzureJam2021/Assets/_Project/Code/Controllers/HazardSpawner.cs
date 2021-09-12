using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    [SerializeField] private HazardObject _hazardPrefab;
    [SerializeField] private List<Hazard> _availableHazards;
    
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int rand = Random.Range(0, _availableHazards.Count);
            HazardObject hazard = Instantiate(_hazardPrefab, transform.GetChild(i).position, Quaternion.identity);
            hazard.Initialize(_availableHazards[rand]);
        }
    }
}
