using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _selectedCircle;

    public void SelectSheep()
    {
        _selectedCircle.enabled = true;
    }
    public void DeselectSheep()
    {
        _selectedCircle.enabled = false;
    }
}
