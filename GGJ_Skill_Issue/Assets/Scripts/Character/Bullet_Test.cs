using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Test : Projectile
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + "Target hit");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + "Target hit");
    }
}
