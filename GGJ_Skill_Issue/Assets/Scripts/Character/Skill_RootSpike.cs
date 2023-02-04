using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RootSpike : Skill
{
    public GameObject spikePrefab_head, spikePrefab_body;
    public float bodySpawnDistance;
    public GameObject instanced_SpikeHead;
    public GameObject instanced_SpikeBodyCurr;
    public List<GameObject> instanced_SpikeBody = new List<GameObject>();
    public Damageable hostile;
    public Transform attackPoint;
    public float returnAnimTime;
    Vector3 spawnMarker;
    List<Collider2D> collided = new List<Collider2D>();

    void Update()
    {
        UpdateFunc();
        if (durationCache > 0)
        {
            durationCache -= Time.deltaTime;
            if (durationCache < returnAnimTime && instanced_SpikeHead)
            {
                spawnMarker += new Vector3((player.transform.position - attackPoint.position).normalized.x, 0, 0) * attackRange * returnAnimTime;

                instanced_SpikeHead.transform.position = spawnMarker;
                GameObject objA = null, objB = null;

                if (instanced_SpikeBody.Count > 1)
                {
                    objB = instanced_SpikeBodyCurr;
                    objA = instanced_SpikeBody[instanced_SpikeBody.Count - 2];
                }
                else
                {
                    objB = instanced_SpikeHead;
                    objA = instanced_SpikeBodyCurr;
                }
                if (objA && objB)
                {
                    if (Vector3.Distance(objA.transform.position, objB.transform.position) < bodySpawnDistance)
                    {
                        instanced_SpikeBody.Remove(objA);
                        Destroy(objA);
                        if (instanced_SpikeBody.Count >0)
                        {
                            instanced_SpikeBodyCurr = instanced_SpikeBody[instanced_SpikeBody.Count - 1];

                        }
                    }
                    else
                    {
                        objA.transform.position = instanced_SpikeHead.transform.position - (attackPoint.position - player.transform.position).normalized * bodySpawnDistance;
                    }
                }
                else 
                {
                    Destroy(objA);
                    Destroy(objB);
                }
                
            }
            else if (instanced_SpikeHead)
            {
                spawnMarker += new Vector3((attackPoint.position - player.transform.position).normalized.x, 0, 0) * attackRange * (attackDuration - returnAnimTime);

                instanced_SpikeHead.transform.position = spawnMarker;
                GameObject objA = null, objB = null;

                if (instanced_SpikeBody.Count > 1)
                {
                    objB = instanced_SpikeBodyCurr;
                    objA = instanced_SpikeBody[instanced_SpikeBody.Count - 2];
                }
                else
                {
                    objB = instanced_SpikeHead;
                    objA = instanced_SpikeBodyCurr;
                }
                if (Vector3.Distance(objA.transform.position, objB.transform.position) > bodySpawnDistance)
                {
                    instanced_SpikeBody.Add(instanced_SpikeBodyCurr = Instantiate(spikePrefab_body, attackPoint.transform));
                }
                else
                {
                    instanced_SpikeBodyCurr.transform.position = instanced_SpikeHead.transform.position - (attackPoint.position - player.transform.position).normalized * bodySpawnDistance;
                }
            }

            foreach (var currTag in GetComponentInParent<Character>().hostileTags)
            {
                CheckTagsAndDamage(instanced_SpikeHead, currTag);
                foreach (var bodyPart in instanced_SpikeBody)
                {
                    CheckTagsAndDamage(bodyPart, currTag);
                }
            }
        }
        else //Skill ends
        {
            Destroy(instanced_SpikeHead);
            instanced_SpikeBody.ForEach(Destroy);
            instanced_SpikeBody.Clear();
        }
    }

    public override void OnSkillCast()
    {
        if (player.SpendMP(mpCost))
        {
            base.OnSkillCast();

            spawnMarker = attackPoint.position;
            durationCache = attackDuration;

            instanced_SpikeHead = Instantiate(spikePrefab_head, attackPoint.transform);
            instanced_SpikeBody.Add(instanced_SpikeBodyCurr = Instantiate(spikePrefab_body, attackPoint.transform));
           

            foreach (var currTag in GetComponentInParent<Character>().hostileTags)
            {
                CheckTagsAndDamage(instanced_SpikeHead, currTag);
                foreach (var bodyPart in instanced_SpikeBody)
                {
                    CheckTagsAndDamage(bodyPart, currTag);
                }
            }
        }
    }

    List<Collider2D> CheckCollided(GameObject inBody)
    {
        if (inBody.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), collided) > 0)
        {
            return collided;
        }
        return null;
    }

    public void CheckTagsAndDamage(GameObject inBody, string input)
    {
        hostile = CheckCollided(inBody)?.Find(x => x.CompareTag(input))?.gameObject.GetComponent<Damageable>();
        if (hostile == null)
        {
            return;
        }
        hostile.Damage(attackDamage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(spawnMarker, 1);
    }
}
