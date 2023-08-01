using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float speed;
    public float damage = 1;
    public float projectilelife;
    private Weapon shootingScript;
    public bool upgraded;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootingScript = FindObjectOfType<Weapon>();
        Move();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Enemy>() && !collision.GetComponent<Enemy>().dead && !collision.GetComponent<Enemy>().anim.GetCurrentAnimatorStateInfo(0).IsTag("Spawn")) {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (!upgraded && !GetComponent<ExplodingProjectile>()) damage = 1;
        Destroy(gameObject, projectilelife);
        Vector2 joystickPosition = shootingScript.joystickposition.normalized;
        if (joystickPosition != Vector2.zero) {
            float angle = Mathf.Atan2(joystickPosition.y, joystickPosition.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle + 90;
        }
    }
   public virtual void Move() {
        rb.velocity = shootingScript.joystickposition.normalized * speed;
    }
}
