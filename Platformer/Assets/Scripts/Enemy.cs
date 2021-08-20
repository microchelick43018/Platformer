using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Sides _startTurnDirection;

    private Transform _patrolPoint1;
    private Transform _patrolPoint2;
    private bool _isMovingToPoint1;

    private enum Sides
    {
        left = 0,
        right = 1
    }

    public void Init(Transform patrolPoint1, Transform patrolPoint2)
    {
        _patrolPoint1 = patrolPoint1;
        _patrolPoint2 = patrolPoint2;
        _isMovingToPoint1 = true;
        TurnToCorrectSide(patrolPoint1.position - transform.position);
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_isMovingToPoint1)
        {
            MoveToPoint(_patrolPoint1.position);
            if (transform.position == _patrolPoint1.position)
            {
                _isMovingToPoint1 = false;
                TurnToCorrectSide(_patrolPoint2.position - transform.position);
            }
        }
        else
        {
            MoveToPoint(_patrolPoint2.position);
            if (transform.position == _patrolPoint2.position)
            {
                _isMovingToPoint1 = true;
                TurnToCorrectSide(_patrolPoint1.position - transform.position);
            }
        }
    }

    private void MoveToPoint(Vector3 point)
    {
        transform.position = Vector3.MoveTowards(transform.position, point, _speed * Time.deltaTime);
    }
    
    private void TurnToCorrectSide(Vector2 movingVector)
    {
        if (movingVector.x >= 0)
        {
            transform.rotation = new Quaternion(0, (int)_startTurnDirection * 180, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, (int)(_startTurnDirection - 1) * 180, 0, 0);
        }
    }
}
