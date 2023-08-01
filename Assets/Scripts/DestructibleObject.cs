using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : Unit
{
    public GameObject[] itemdrops;
    public int dropamt;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        hitpoints = maxhitpoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitpoints <= 0) {
            Destroy(gameObject);
            for (int i = 0; i < Random.Range(1, dropamt + 1); i++) {
                if (itemdrops[Random.Range(0, itemdrops.Length)] == null) return;
                Instantiate(itemdrops[Random.Range(0,itemdrops.Length)], transform.position, transform.rotation);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<ExplodingProjectile>()) {
            TakeHit(Mathf.Abs(collision.GetComponent<ExplodingProjectile>().damage));
            AudioManager.instance.PlaySFX(AudioManager.instance.impactSound);
            StartCoroutine(DamageFeedback());
        }
        else if (collision.GetComponent<ProjectileScript>() && !collision.CompareTag("Enemy")) {
            TakeHit(collision.GetComponent<ProjectileScript>().damage);
            AudioManager.instance.PlaySFX(AudioManager.instance.impactSound);
            StartCoroutine(DamageFeedback());
        }
    }
}
