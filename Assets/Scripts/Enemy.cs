using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GeneralFunctions
{
    public GameObject floatingTextPrefab;
    public float movespeed;
    private Player player;
    private Rigidbody2D rb;
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        hitpoints = maxhitpoints;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
       // SetHealth(hitpoints, maxhitpoints);
        if (hitpoints <= 0) {
            StartCoroutine(Death());
        }
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }
    private void FixedUpdate() {
        Move(movement);
    }
    private void Move(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * movespeed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<ProjectileScript>() ) {
            //Destroy(collision.gameObject);
            TakeHit(collision.GetComponent<ProjectileScript>().damage);
            StartCoroutine(DamageFeedback());
            if (floatingTextPrefab && hitpoints > 0)
            {
                ShowFloatingText();
            }
        }
    }
   void ShowFloatingText()
    {
        var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = hitpoints.ToString();
    }
    public IEnumerator Death()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.25f);
       gameObject.SetActive(false);
    }
}
