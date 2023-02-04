using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public MovementController movementController;
    public new Collider2D collider2D;
    public new Rigidbody2D rigidbody2D;

    private void Start()
    {
        movementController = GetComponentInChildren<MovementController>();

        if (movementController)
        {
            movementController.parent = gameObject;
        }

        collider2D = GetComponentInChildren<Collider2D>();
        rigidbody2D = GetComponentInChildren<Rigidbody2D>();
    }

}
