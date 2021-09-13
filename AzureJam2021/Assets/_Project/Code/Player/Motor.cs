using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    [SerializeField] private float _baseMoveSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private bool _canBeControlled;
    public bool CanBeControlled { get { return _canBeControlled; } set { _canBeControlled = value; } }
    
    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        GameController.OnGameStart += HandleGameStart;
        GameController.OnGameOver += HandleGameOver;
    }

    private void OnDestroy()
    {
        GameController.OnGameStart -= HandleGameStart;
        GameController.OnGameOver -= HandleGameOver;
    }

    private void Start()
    {
        CanBeControlled = true;
        _direction.y = 1;
    }

    private void FixedUpdate()
    {
        _body.velocity = _direction * _moveSpeed * Time.deltaTime;
    }

    private void HandleGameStart()
    {
        _moveSpeed = _baseMoveSpeed;
    }

    private void HandleGameOver()
    {
        _moveSpeed = 0;
    }

    public void MoveTowards(float direction)
    {
        if (!CanBeControlled) return;

        float moveDirection = 1;
        if (transform.position.x > direction)
            moveDirection = -1;
        _direction.x = moveDirection;
    }

    public void HeadTowards(Sheep sheep)
    {
        float moveDirection = 1;
        if (transform.position.x > sheep.transform.position.x)
            moveDirection = -1;
        _direction.x = moveDirection;
    }

    public void HeadForward()
    {
        _direction.x = 0;
    }
}
