using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : Unit
{
    public GameObject[] itemdrops;
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
            Instantiate(itemdrops[Random.Range(0, itemdrops.Length)], transform.position, transform.rotation);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<ProjectileScript>()) {
            TakeHit(collision.GetComponent<ProjectileScript>().damage);
            StartCoroutine(DamageFeedback());
        }
    }
}
