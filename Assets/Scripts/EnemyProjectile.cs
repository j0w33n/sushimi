using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : ProjectileScript
{
    public Transform target;
    public float turnSpeed = 40f;
    // Start is called before the first frame update
    protected override void Start() {
        rb = GetComponent<Rigidbody2D>();
        Move();
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Destroy(gameObject, projectilelife);
    }
    public override void Move() {
        if (target) {
            Vector3 desiredFacing = target.position - transform.position;
            Quaternion desiredRotation = Quaternion.LookRotation(desiredFacing);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, turnSpeed * Time.deltaTime);
        }
        rb.velocity = transform.right * speed;
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<Player>() && !collision.GetComponent<Player>().dead) {
            Destroy(gameObject);
        }
    }
}
