using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProjectile : ProjectileScript
{
    public float blastradius;
    public GameObject explosionvfx;
    protected override void Update() {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, blastradius);

        // Loops through the targets.
        foreach (Collider2D t in targets) {

            // Checks if the target we are checking is a valid enemy.
            Enemy e = t.GetComponent<Enemy>();
            DestructibleObject d = t.GetComponent<DestructibleObject>();
            if (e) {
                // Vary the damage based on distance.
                Vector2 diff = e.transform.position - transform.position;
                damage = 10 * (1 - diff.magnitude / blastradius);
            }
            else if (d) {
                Vector2 diff2 = d.transform.position - transform.position;
                damage = 10 * (1 - diff2.magnitude / blastradius);
            }
        }
        print(damage);
        base.Update();
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<Enemy>() || collision.GetComponent<DestructibleObject>()) {
            Destroy(Instantiate(explosionvfx, transform.position, transform.rotation), 1f);
        }
        base.OnTriggerEnter2D(collision);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, blastradius);
    }
}
