using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
                        Rigidbody2D _rb2d;

                        //Movimentação
    [SerializeField]    short _speed;
    [SerializeField]    short _speedLimit;
                        Vector2 _horizontal;
                        sbyte _isRotated = 1;

                        //Pulo
    [SerializeField]    float _jump;
                        bool _grounded;
                        bool _LWall;
                        bool _RWall;

                        //Dash
    [SerializeField]    float _dash;
    [SerializeField]    float _dashJump;
    [SerializeField]    float _dashCooldown;
                        float _dashLast;

    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _dashLast = Time.time;
    }

    void FixedUpdate()
    {
        //Movimentação
        _rb2d.AddForce(new Vector2(_horizontal.x * _speed * 10, 0), ForceMode2D.Force);
        _rb2d.velocity = Vector2.ClampMagnitude(_rb2d.velocity, _speedLimit);

        if (_rb2d.velocity.x < 0)//Virar para esquerda
        {
            _rb2d.transform.rotation = Quaternion.Euler(0, 180, 0);
            _isRotated = -1;
        }
        else if (_rb2d.velocity.x > 1)//Virar para direita
        {
            _rb2d.transform.rotation = Quaternion.identity;
            _isRotated = 1;
        }
    }

    public void OnMove(InputAction.CallbackContext context)//Movimentação
    {
        _horizontal = context.ReadValue<Vector2>();

        if (_horizontal.x < 0)
        {
            _horizontal.x = -1;
        } else if (_horizontal.x > 0)
        {
            _horizontal.x = 1;
        }
    }

    public void OnJump(InputAction.CallbackContext context)//Pulo
    {
        _grounded = Physics2D.BoxCast(_rb2d.transform.position, Vector2.one, 0, Vector2.down, 0.1f);
        _LWall = Physics2D.BoxCast(_rb2d.transform.position, Vector2.one, 0, Vector2.left, 0.01f);
        _RWall = Physics2D.BoxCast(_rb2d.transform.position, Vector2.one, 0, Vector2.right, 0.01f);

        if (_grounded)//Checar chão
        {
            _rb2d.AddForce(Vector2.up * _jump * 100, ForceMode2D.Force);
        }

        if (_LWall)//Checar parede esquerda
        {
            _rb2d.AddForce(new Vector2(_jump, _jump * 3), ForceMode2D.Impulse);
        }
        else if (_RWall)//Checar parede direita
        {
            _rb2d.AddForce(new Vector2(_jump * -1, _jump * 3), ForceMode2D.Impulse);
        }
    }

    public void OnDash(InputAction.CallbackContext context)//Dash
    {
        if (Time.time - _dashLast < _dashCooldown) return;//Cooldown

        if (_horizontal.x == 0)//Dash parado
        {
            _rb2d.AddForce(new Vector2(_dash * _isRotated * 100, _dashJump * 100), ForceMode2D.Force);
        }
        else//Dash andando
        {
            _rb2d.AddForce(new Vector2(_dash * _horizontal.x * 100, _dashJump * 100), ForceMode2D.Force);
        }

        _dashLast = Time.time;
    }
}
