using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector2 _direction;
    
    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _direction.y = 1;
    }

    private void Update()
    {
        _body.velocity = _direction * _moveSpeed * Time.deltaTime;
    }

    public void MoveTowards(float direction)
    {
        float moveDirection = 1;
        if (transform.position.x > direction)
            moveDirection = -1;
        _direction.x = moveDirection;
    }
}
