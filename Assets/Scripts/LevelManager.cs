using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    Player player;
    [SerializeField]List<EnemySpawner> enemyspawns;
    public float waitToRespawn;
    public bool wavecomplete;
    public int waves;
    public int enemieskilled;
    public int totalenemieskilled;
    [SerializeField]int totalenemies = 0;
    public GameObject partcount;
    public int parts;
    public Slider ammobar, enemykillcount;
    public GameObject panel, currentroom;
    public AudioClip partsound, healthsound;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        foreach(GameObject i in currentroom.GetComponent<Room>().myenemyspawns) {
            enemyspawns.Add(i.GetComponent<EnemySpawner>());
        }
        foreach (var i in enemyspawns) {
            totalenemies += i.enemiestospawn[i.currentwave];
        }
        FindObjectOfType<AudioManager>()?.StopMusic();
    }

    // Update is called once per frame
    void Update()
    {
        partcount.GetComponent<Text>().text = "x " + parts.ToString();
        enemykillcount.value = totalenemieskilled;
        if (currentroom.GetComponent<Room>().roomstart) {
            if (enemieskilled == totalenemies) {
                wavecomplete = true;
                if(waves > 0) {
                    waves -= 1;
                    enemieskilled = 0;
                    totalenemies = 0;
                    foreach (var i in enemyspawns) {
                        i.currentwave += 1;
                        totalenemies += i.enemiestospawn[i.currentwave];
                        i.enemiesspawned = 0;
                    }
                }
            }
            if (waves == 0 && currentroom.GetComponent<Room>().roomstart) {
                currentroom.GetComponent<Room>().roomstart = false;
            }
        }
        if(totalenemieskilled == 25) {
            panel.GetComponent<UpgradePanel>().active= true;
            totalenemieskilled = 0;
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
