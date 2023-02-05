using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Collider2D selfCollider;

    float age;
    public float Age => age;

    [SerializeField] float ageOfDeath;
    public float AgeOfDeath => ageOfDeath;

    [SerializeField] float speed = 1;
    public float Speed => speed;

    //[SerializeField] float gravityForce ;
    //public float GravityForce => gravityForce;

    [SerializeField] int damagePoints = 1;
    public int DamagePoints => damagePoints;

    List<Collider2D> collided = new List<Collider2D>();
    public List<Collider2D> Collided => collided;

    [SerializeField] Vector3 normalisedDirection;
    public Vector3 NormalisedDirection => normalisedDirection;

    [SerializeField] bool toDestroy;
    public bool ToDestroy => toDestroy;

    Damageable hostile;


    private void Update()
    {
        Aging();
    }

    void Aging()
    {
        age += Time.deltaTime;
        if (age > ageOfDeath)
        {
            toDestroy = true;
        }
    }


    public void SetDirection(Vector3 inNormalisedDirection)
    {
        normalisedDirection = inNormalisedDirection;
    }

    public void ProcessCourse()
    {
        transform.position += normalisedDirection * speed;
    }

    List<Collider2D> CheckCollided()
    {
        if (selfCollider.OverlapCollider(new ContactFilter2D(), collided) > 0)
        {
            return collided;
        }
        return null;
    }

    public void CheckTagsAndDamage(string input)
    {
        hostile = CheckCollided()?.Find(x => x.CompareTag(input))?.gameObject.GetComponent<Damageable>();

        Debug.Log("shjgsdjh");
        if (hostile == null)
        {
            return;
        }
        hostile.DamageEvents += ProcessToDestroy;
        hostile.Damage(damagePoints);
    }

    void ProcessToDestroy()
    {
        toDestroy = true;
    }

    public void ProcessUnaliveSelf()
    {
        if (toDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (hostile)
        {
            hostile.DamageEvents -= ProcessToDestroy;
        }
    }
}
