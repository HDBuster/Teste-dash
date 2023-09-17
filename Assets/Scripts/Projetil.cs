using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] float shootSpeed;

    Mover press;
    Vector2 direcao;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        direcao = new Vector2(GetComponent<Mover>().isRotated * shootSpeed, rb2d.transform.position.y);
    }


    void FixedUpdate()
    {
        rb2d.AddForce(direcao, ForceMode2D.Impulse);
    }
}
