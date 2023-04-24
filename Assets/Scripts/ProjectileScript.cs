using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float damage = 1;
    public float splashRange = 1; //we gonna add aoe as buff after clearing some stages

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(splashRange > 0)  //use gizmos?
        {
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<Enemy>();
                if (enemy)
                {
                    var closestPoint = hitCollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closestPoint, transform.position);

                    var damagePercent = Mathf.InverseLerp(splashRange, 0, distance);
                    enemy.TakeHit(damagePercent * damage);
                }
            }
        }
        else
        {
            if (collision.GetComponent<Enemy>())
            {
                Destroy(gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
