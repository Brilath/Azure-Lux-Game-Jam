using System;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _selectedCircle;
    [SerializeField] private Transform _followPoint;
    public Transform FollowPoint { get { return _followPoint; } private set { _followPoint = value; } }

    public static Action<Sheep> OnSheepSpawn = delegate { };
    public static Action<Sheep> OnSheepDead = delegate { };

    private void Start()
    {
        OnSheepSpawn?.Invoke(this);
    }
    private void OnDestroy()
    {
        OnSheepDead?.Invoke(this);
    }

    public void SelectSheep()
    {
        _selectedCircle.enabled = true;
    }
    public void DeselectSheep()
    {
        _selectedCircle.enabled = false;
    }
}
