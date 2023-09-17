using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Firing : MonoBehaviour
{
    [SerializeField] GameObject disparo;
    [SerializeField] GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Instantiate(disparo, new Vector2(player.transform.position.x + 1, player.transform.position.y), Quaternion.identity);
    }
}
