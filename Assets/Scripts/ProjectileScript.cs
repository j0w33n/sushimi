using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float damage = 1;
    public float projectilelife;
    private Weapon shootingScript;
    //public float splashRange = 1; //we gonna add aoe as buff after clearing some stages

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootingScript = FindObjectOfType<Weapon>();
        damage = 1;
        Move();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Enemy>()) {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        Destroy(gameObject, projectilelife);
        Vector2 lookdir = shootingScript.joystickposition - rb.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
   public virtual void Move() {
        rb.velocity = shootingScript.joystickposition.normalized * speed;
    }
}
