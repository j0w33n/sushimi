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
        target = FindObjectOfType<Player>(true).transform;
        Move();
    }

    // Update is called once per frame
    protected override void Update()
    {
        Destroy(gameObject, projectilelife);
    }
    public override void Move() {
        Vector3 dir = target.position - transform.position;
        rb.velocity = dir.normalized * speed;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.GetComponent<Enemy>()) {
            Destroy(gameObject);
        }
        if(collision.GetComponent<Player>() && !collision.GetComponent<Player>().dead) {
            Destroy(gameObject);
        }
    }
}
