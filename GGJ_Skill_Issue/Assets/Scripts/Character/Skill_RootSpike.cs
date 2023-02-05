using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RootSpike : Skill
{
    public GameObject spikePrefab_head, spikePrefab_body;
    public float acceleration;
    public GameObject instanced_SpikeHead;
    public GameObject instanced_SpikeBody;
    public Damageable hostile;
    public Transform attackPoint;
    public float returnAnimTime;
    List<Collider2D> collided = new List<Collider2D>();

    void Update()
    {
        UpdateFunc();

        if (durationCache > 0)
        {
            durationCache -= Time.deltaTime;

            if (durationCache > returnAnimTime)
            {
                instanced_SpikeHead.transform.position += (attackPoint.position - player.transform.position).normalized * acceleration * Time.deltaTime;
            }
            else
            {
                instanced_SpikeHead.transform.position -= (attackPoint.position - player.transform.position).normalized * acceleration * Time.deltaTime;
            }

            Vector3 startPosition = player.transform.position;
            Vector3 endPosition = instanced_SpikeHead.transform.position - (attackPoint.position - player.transform.position).normalized/3;
            bool mirrorZ = true;


            Strech(instanced_SpikeBody, startPosition, endPosition, mirrorZ);

            foreach (var currTag in GetComponentInParent<Character>().hostileTags)
            {
                CheckTagsAndDamage(instanced_SpikeHead, currTag);
                CheckTagsAndDamage(instanced_SpikeBody, currTag);
            }
        }
        else //Skill ends
        {
            Destroy(instanced_SpikeHead);
            Destroy(instanced_SpikeBody);
        }

        //if (durationCache > 0)
        //{
        //    durationCache -= Time.deltaTime;

        //    GameObject objA, objB;

        //    objA = instanced_SpikeHead;
        //    objB = instanced_SpikeBodyCurr;

        //    if (durationCache > returnAnimTime && instanced_SpikeHead)
        //    {
        //        spawnMarker.transform.position += (attackPoint.position - player.transform.position).normalized * attackRange * Time.deltaTime;

        //        if (Mathf.Abs(objB.transform.position.x - attackPoint.transform.position.x) > objA.transform.lossyScale.x * bodySpawnDistance)
        //        {
        //            instanced_SpikeBodyCurr = Instantiate(spikePrefab_body, attackPoint.transform);
        //            instanced_SpikeBody.Add(instanced_SpikeBodyCurr);
        //        }

        //        objB = instanced_SpikeBodyCurr;
        //        objB.transform.position = spawnMarker.transform.position - (attackPoint.position - player.transform.position).normalized * objA.transform.lossyScale.x * Time.deltaTime;
        //    }
        //    else if (instanced_SpikeHead)
        //    {
        //        spawnMarker.transform.position -= (attackPoint.position - player.transform.position).normalized * attackRange * Time.deltaTime;

        //        if (instanced_SpikeBody.Count > 0)
        //        {
        //            int victim = instanced_SpikeBody.FindIndex(x => Mathf.Abs(objB.transform.position.x - x.transform.position.x) < 0.1);
        //            GameObject temp = instanced_SpikeBody[victim];
        //            instanced_SpikeBody.RemoveAt(victim);
        //            Destroy(temp);
        //        }
        //        else
        //        {
        //            instanced_SpikeBody.Clear();
        //        }


        //        objB = instanced_SpikeBody[0];

        //        objB.transform.position = spawnMarker.transform.position + (attackPoint.position - player.transform.position).normalized * objA.transform.lossyScale.x * Time.deltaTime;
        //    }

        //    objA.transform.position = spawnMarker.transform.position;


        //    foreach (var currTag in GetComponentInParent<Character>().hostileTags)
        //    {
        //        CheckTagsAndDamage(instanced_SpikeHead, currTag);
        //        foreach (var bodyPart in instanced_SpikeBody)
        //        {
        //            CheckTagsAndDamage(bodyPart, currTag);
        //        }
        //    }
        //}
        //else //Skill ends
        //{
        //    Destroy(instanced_SpikeHead);
        //    instanced_SpikeBody.ForEach(Destroy);
        //    instanced_SpikeBody.Clear();
        //}
    }
    public void Strech(GameObject _sprite, Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ)
    {
        Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
        _sprite.transform.position = centerPos;
        Vector3 direction = _finalPosition - _initialPosition;
        direction = Vector3.Normalize(direction);
        _sprite.transform.right = direction;
        if (_mirrorZ) _sprite.transform.right *= -1f;
        Vector3 scale = Vector3.one;
        scale.x = Vector3.Distance(_initialPosition, _finalPosition);
        scale.y /= 3;
        _sprite.transform.localScale = scale;
    }

    public override void OnSkillCast()
    {
        if (player.SpendMP(mpCost))
        {
            base.OnSkillCast();
            if (!player.unlockedSkills.Contains(type))
            {
                return;
            }
            durationCache = attackDuration;

            instanced_SpikeHead = Instantiate(spikePrefab_head, attackPoint);
            instanced_SpikeBody = Instantiate(spikePrefab_body, attackPoint);
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

}
