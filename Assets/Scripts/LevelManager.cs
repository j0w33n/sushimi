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
    [SerializeField]int totalenemies = 0;
    public GameObject chest;
    public int parts;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        enemyspawns = FindObjectsOfType<EnemySpawner>();
        foreach (var i in enemyspawns) {
            totalenemies += i.enemiestospawn[i.currentwave];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (enemieskilled == totalenemies && waves > 0) {
            wavecomplete = true;
            waves -= 1;
            enemieskilled = 0;
            totalenemies = 0;
            foreach (var i in enemyspawns) {
                i.currentwave += 1;
                totalenemies += i.enemiestospawn[i.currentwave];
                i.enemiesspawned = 0;
            }
        }
        if(waves == 0) {
            chest.SetActive(true);
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
