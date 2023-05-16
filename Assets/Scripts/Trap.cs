using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player p = other.GetComponent<Player>();
        if (!p) return;

        if(other is BoxCollider2D)
        {
            p.TakeHit(damage);
        }
        else if(other is CircleCollider2D)
        {

        }
    }
}
