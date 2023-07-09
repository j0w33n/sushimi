using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : Unit
{
    public CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        hitpoints = maxhitpoints;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hitpoints <= 0) {
            circleCollider.enabled = false;
            sr.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<ExplodingProjectile>() && collision.tag != "Enemy") {
            TakeHit(Mathf.Abs(collision.GetComponent<ProjectileScript>().damage) * 2);
            AudioManager.instance.PlaySFX(hitsound);
            StartCoroutine(DamageFeedback());
        }
        else if(collision.GetComponent<ProjectileScript>() && collision.tag != "Enemy") {
            TakeHit(Mathf.Abs(collision.GetComponent<ProjectileScript>().damage));
            AudioManager.instance.PlaySFX(hitsound);
            StartCoroutine(DamageFeedback());
        }
    }
}
