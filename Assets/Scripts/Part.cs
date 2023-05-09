using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Part : MonoBehaviour , ICollectible
{
    public int partvalue;

    public static event Action OnPartCollected;
    Rigidbody2D rb;

    bool hasTarget;
    Vector3 targetPosition;
    float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Collect()
    {
        Debug.Log("Part Collected");
        Destroy(gameObject);
        OnPartCollected?.Invoke();
    }
    private void FixedUpdate()
    {
        if(hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
        }
    }
    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }

}
