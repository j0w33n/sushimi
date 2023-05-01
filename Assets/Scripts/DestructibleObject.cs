using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        originalColor = sr.color;
        hitpoints = maxhitpoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitpoints <= 0) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<ProjectileScript>()) {
            TakeHit(collision.GetComponent<ProjectileScript>().damage);
            StartCoroutine(DamageFeedback());
        }
    }
}
