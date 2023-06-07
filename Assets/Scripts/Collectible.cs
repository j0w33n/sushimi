using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value;
    protected Rigidbody2D rb;

    public bool hasTarget;
    Vector3 targetPosition;
    public float moveSpeed;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, 2.5f, LayerMask.GetMask("Player"));
        if (player) {
            hasTarget = true; 
            targetPosition = player.transform.position;
        }
        if (hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector2(targetDirection.x,targetDirection.y) * moveSpeed;
        } 
        else {
            rb.velocity = Vector2.zero;
        }
    }
    protected void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 2.5f);
    }
}
