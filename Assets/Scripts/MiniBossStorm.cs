using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossStorm : MonoBehaviour
{
    Rigidbody2D rb;
    public float force;
    public float life;
    public float pullForce = 10f;  // The force with which the storm pulls objects towards it

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * force;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, life);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        // Check if the object has a Rigidbody2D component
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            if (other.GetComponent<Enemy>() || other.GetComponent<MiniBossStorm>()) return;
            // Calculate the direction from the object to the tornado
            Vector2 direction = transform.position - other.transform.position;
            // Normalize the direction vector
            direction.Normalize();
            // Apply a pulling force to the object
            rb.AddForce(direction * pullForce);
        }
    }
}
