using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupper : MonoBehaviour
{
    public Collider2D selfCollider;
    public Player player;
    List<Collider2D> collided = new List<Collider2D>();

    private void Update()
    {
        if (selfCollider.OverlapCollider(new ContactFilter2D(), collided) > 0)
        {
            foreach (var item in collided)
            {
                
                Pickups checkValidItem = item.gameObject.GetComponent<Pickups>();
                if (checkValidItem)
                {
                    Debug.Log(checkValidItem.gameObject.name);
                    switch (checkValidItem.type)
                    {
                        case PickupType.SkillPickup:
                            player.sp += checkValidItem.count;
                            break;
                        default:
                            break;
                    }

                    checkValidItem.EndSelf();
                }
            }
        }
    }

}
