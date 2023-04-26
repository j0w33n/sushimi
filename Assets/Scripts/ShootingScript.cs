using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Transform firept;
    Player player;
    public GameObject instprefab;
    private float nextinsttime;
    public float instrate;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Generate();
            Destroy(instprefab, 3);
        }
        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalkRight")) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        } 
        else if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalkLeft")) {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } 
        else if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalkUp")) {
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        } 
        else if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalkDown")) {
            transform.localRotation = Quaternion.Euler(0, 0, -90);
        } 
        else if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalkTopRight")) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        } 
        else if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalkTopLeft")) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        } 
        else if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalkBottomRight")) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        } 
        else if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalkBottomLeft")) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        } 
        else {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void Generate() {
        if (Time.time < nextinsttime) return;
        Instantiate(instprefab, transform.position, transform.rotation);
        nextinsttime = Time.time + instrate;
    }
}
