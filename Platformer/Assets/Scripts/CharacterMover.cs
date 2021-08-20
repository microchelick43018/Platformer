using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerAnimController))]

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Sides _startTurnDirection;
    [SerializeField] private float _jumpForce;

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private enum Sides
    {
        left = 0,
        right = 1
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        
        if (Input.GetKeyDown(KeyCode.Space) && _rigidbody.velocity.y == 0)
        {
            Jump();
        }
    }

    private void Move()
    {
        float axisValue = Input.GetAxis("Horizontal");
        TurnToCorrectSide(axisValue);
        transform.Translate(transform.right * axisValue * Time.deltaTime * _speed);
        _animator.SetFloat(PlayerAnimController.Params.Speed, _speed * Mathf.Abs(axisValue));
    }

    private void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void TurnToCorrectSide(float axisValue)
    {
        if (axisValue > 0)
        {
            transform.localRotation = new Quaternion(0, (int)_startTurnDirection * 180, 0, 0);
        }
        else if (axisValue < 0)
        {
            transform.localRotation = new Quaternion(0, (int)(_startTurnDirection - 1) * 180, 0, 0);
        }
    }
}