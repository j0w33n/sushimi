using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int partvalue,healthvalue;
    Rigidbody2D rb;

    bool hasTarget;
    Vector3 targetPosition;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        partvalue = Random.Range(1, 4);
    }
    private void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, 2.5f, LayerMask.GetMask("Player"));
        if (player != null) {
            hasTarget = true; 
            targetPosition = player.transform.position;
        } 
        else {
            hasTarget = false;
        }
        if (hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector2(targetDirection.x,targetDirection.y) * moveSpeed;
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 2.5f);
    }
}
