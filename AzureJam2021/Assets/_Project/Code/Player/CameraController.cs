using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private Sheep _currentSheepTarget;
    [SerializeField] private List<Sheep> _sheepTargets;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        _sheepTargets = new List<Sheep>();
        Sheep.OnSheepSpawn += HandleSheepSpawn;
        Sheep.OnSheepDead += HandleSheepDead;
    }
    private void OnDestroy()
    {
        Sheep.OnSheepSpawn -= HandleSheepSpawn;
        Sheep.OnSheepDead -= HandleSheepDead;
    }

    private void HandleSheepSpawn(Sheep sheep)
    {
        if (vcam.Follow == null)
        {
            SetTarget(sheep);
        }
        else
        {
            if (!_sheepTargets.Contains(sheep))
                _sheepTargets.Add(sheep);
        }
    }
    private void HandleSheepDead(Sheep sheep)
    {
        if (_currentSheepTarget == sheep)
        {
            if (_sheepTargets.Count > 0)
            {
                SetTarget(_sheepTargets[0]);
            }
        }
        else
        {
            if (_sheepTargets.Contains(sheep))
                _sheepTargets.Remove(sheep);
        }
    }
    private void SetTarget(Sheep sheep)
    {
        vcam.Follow = sheep.transform;
        _currentSheepTarget = sheep;
        if (_sheepTargets.Contains(sheep))
            _sheepTargets.Remove(sheep);
    }
}
