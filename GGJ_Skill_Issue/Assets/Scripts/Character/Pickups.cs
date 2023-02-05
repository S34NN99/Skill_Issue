using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    SkillPickup = 0
}
public class Pickups : MonoBehaviour
{
    public PickupType type;
    public int count;

    public void EndSelf()
    {
        Destroy(gameObject);
    }
}
