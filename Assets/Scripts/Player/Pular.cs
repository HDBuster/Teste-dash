using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pular : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] float jump;
    bool grounded;
    bool walledL;
    bool walledR;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        grounded = Physics2D.BoxCast(rb2d.transform.position, Vector2.one, 0, Vector2.down, 0.1f);

        walledL = Physics2D.BoxCast(rb2d.transform.position, Vector2.one, 0, Vector2.left, 0.1f);
        walledR = Physics2D.BoxCast(rb2d.transform.position, Vector2.one, 0, Vector2.right, 0.1f);

        if (grounded) 
        { 
        rb2d.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }

        if (walledL)
        {
            rb2d.AddForce(new Vector2(3, jump * 1.5f), ForceMode2D.Impulse);
        } else if (walledR)
        {
            rb2d.AddForce(new Vector2(-3, jump * 1.5f), ForceMode2D.Impulse);
        }
    }
}
