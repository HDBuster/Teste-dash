using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dashing : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] float dashSpeed;

    Vector2 rotation;
    Mover direction;

    //int isRotated;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        rb2d.AddForce(new Vector2(dashSpeed * rotation.x, 2), ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        rotation = GetComponent<Mover>().press;
    }
}
