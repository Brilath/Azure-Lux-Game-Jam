using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    [SerializeField] private Sheep _currentSheep;

    private Camera _gameCamera;

    private void Awake()
    {
        _gameCamera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (_currentSheep != null)
            {
                _currentSheep.DeselectSheep();
                _currentSheep = null;
            }

            Vector3 worldPosition = _gameCamera.ScreenToWorldPoint(Input.mousePosition);
            var collider = Physics2D.OverlapPoint(worldPosition);
            if (collider != null)
            {
                Sheep sheep = collider.GetComponent<Sheep>();
                if (sheep == null) return;
                _currentSheep = sheep;
                _currentSheep.SelectSheep();
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
            if (_currentSheep == null) return;

            Vector2 worldPosition = _gameCamera.ScreenToWorldPoint(Input.mousePosition);
            
            Motor sheepMotor = _currentSheep.GetComponent<Motor>();
            if (sheepMotor == null) return;
            sheepMotor.MoveTowards(worldPosition.x);
        }
    }
}
