using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject boss;
    public GameObject wall1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            boss.SetActive(true);
            wall1.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
