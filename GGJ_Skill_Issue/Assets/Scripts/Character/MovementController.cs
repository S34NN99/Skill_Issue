using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject parent;
    public float speed = 8.0f;
    public float timeScaleMultiplier = 1.0f;
    public bool isPossessed = false;

    private bool isJumpNow;

    void MoveToVector(Vector2 normalisedDirection, float scale = 1.0f)
    {
        Vector3 newPos = scale * speed * Time.deltaTime * timeScaleMultiplier * new Vector3(normalisedDirection.x , normalisedDirection.y, 0);
        parent.transform.position += newPos;
    }

    void MoveLeft(float scale = 1)
    {
        MoveToVector(Vector2.left, scale);
    }
    void MoveRight(float scale = 1)
    {
        MoveToVector(Vector2.right, scale);
    }

    void JumpUp(float scale = 1)
    {
        parent.GetComponent<Character>().rigidbody2D.AddForce(scale * speed * Time.deltaTime * timeScaleMultiplier * Vector2.up, ForceMode2D.Impulse);
    } 

    bool IsTouchingBottomGround()
    {
        ContactFilter2D hhhh = new ContactFilter2D();
        List<Collider2D> tuna = new List<Collider2D>();

        if (parent.GetComponent<Character>().collider2D.OverlapCollider(hhhh, tuna) > 0)
        {
            return true;
        }

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPossessed)
        {
            if (Input.GetKey(KeyCode.A))
            {
                MoveLeft();
            }
            if (Input.GetKey(KeyCode.D))
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.Space) && IsTouchingBottomGround())
            {
                isJumpNow = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isJumpNow)
        {
            JumpUp(50);
            isJumpNow = false;
        }
        
    }
}
