using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    Player player;
    EnemySpawner[] enemyspawns;
    public float waitToRespawn;
    public bool wavecomplete;
    public int waves;
    public int enemieskilled;
    int totalenemies = 0;
    public GameObject chest;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        enemyspawns = FindObjectsOfType<EnemySpawner>();
        foreach(var i in enemyspawns) {
            totalenemies += i.enemiestospawn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemieskilled == totalenemies) {
            wavecomplete = true;
            //waves -= 1;
            chest.SetActive(true);
            enemieskilled = 0;
        }
    }
    public void Respawn() {
        StartCoroutine(RespawnCo());
    }
    IEnumerator RespawnCo() {
        Physics2D.IgnoreLayerCollision(6, 7, false);
        yield return new WaitForSeconds(0.25f);
        player.gameObject.SetActive(false); // deactivates player
        yield return new WaitForSeconds(waitToRespawn); // how long to wait before respawning player
        player.gameObject.SetActive(true); // reactivates player
        player.transform.position = player.respawnpoint; // moves player to respawn point
        player.dead = false;
    }
}
