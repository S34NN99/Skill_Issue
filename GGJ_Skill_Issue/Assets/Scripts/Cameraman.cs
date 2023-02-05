using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraman : MonoBehaviour
{
    public bool FollowPlayer;
    public GameObject player;
    public Vector3 offset;


    private void Update()
    {
        if (FollowPlayer && player != null)
        {
            Camera.main.transform.position = player.transform.position + offset;
        }
    }
}
