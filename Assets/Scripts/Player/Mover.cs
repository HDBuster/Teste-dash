using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] int speed;
    [SerializeField] float speedLimit;

    public Vector2 press;
    public sbyte isRotated;

    void Awake()
    {
       rb2d = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        press = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb2d.AddForce(new Vector2(press.x * speed * 10, 0), ForceMode2D.Force);
        rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, speedLimit);

        if (rb2d.velocity.x < 0)
        {
            rb2d.transform.rotation = Quaternion.Euler(0, 180, 0);
            isRotated = 1;
        } else if (rb2d.velocity.x > 1)
        {
            rb2d.transform.rotation = Quaternion.identity;
            isRotated = -1;
        }
    }
}
