using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Terresquall;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Player player;
    public List<EnemySpawner> enemyspawns;
    public float waitToRespawn;
    public bool wavecomplete;
    public int waves;
    public int enemieskilled;
    public int totalenemieskilled;
    public int totalenemies = 0;
    public GameObject partcount;
    public int parts;
    public Slider ammobar, enemykillcount;
    public GameObject panel, currentroom;
    public AudioClip levelmusic;
    public string leveltoload;
    [SerializeField] VirtualJoystick[] joysticks;
    public List<GameObject> rooms = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        parts = PlayerPrefs.GetInt("Parts", 0);
        totalenemieskilled = PlayerPrefs.GetInt("Total Enemies Killed", 0);
        StartCoroutine(SwitchMusic(levelmusic));
        player = FindObjectOfType<Player>();
        VirtualJoystick.instances.Insert(0,joysticks[0]);
        VirtualJoystick.instances.Insert(1,joysticks[1]);
        PlayerPrefs.SetString("Current Level", SceneManager.GetActiveScene().name);
        player.transform.position = rooms[PlayerPrefs.GetInt("Current Room")].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //print(PlayerPrefs.GetString("Current Level"));
        //print(PlayerPrefs.GetInt("Current Room"));
        partcount.GetComponent<Text>().text = "x " + parts.ToString();
        enemykillcount.value = totalenemieskilled;
        if (currentroom.GetComponent<Room>().roomstart) {
            PlayerPrefs.SetInt("Current Room", rooms.IndexOf(currentroom));
            if (enemieskilled == totalenemies) {
                wavecomplete = true;
                StartCoroutine(EndOfWave());
            }
            if (waves == 0 && currentroom.GetComponent<Room>().roomstart) {
                currentroom.GetComponent<Room>().roomstart = false;
                foreach(var i in currentroom.GetComponent<Room>().entrance) {
                    i.SetActive(false);
                }
                AudioManager.instance.PlaySFX(AudioManager.instance.entranceSound);
                //StopCoroutine(player.GetComponentInChildren<Weapon>().Reload());
                player.GetComponentInChildren<Weapon>().ammo = player.GetComponentInChildren<Weapon>().maxammo;
            }
        }
        if(totalenemieskilled >= 25 && !currentroom.GetComponent<Room>().roomstart) {
            panel.GetComponent<UpgradePanel>().active= true;
            totalenemieskilled = 0;
        }
    }
    public void Respawn() {
        StartCoroutine(RespawnCo());
    }
    IEnumerator RespawnCo() {
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        yield return new WaitForSeconds(0.25f);
        player.gameObject.SetActive(false); // deactivates player
        yield return new WaitForSeconds(waitToRespawn); // how long to wait before respawning player
        player.gameObject.SetActive(true); // reactivates player
        player.transform.position = player.respawnpoint; // moves player to respawn point
        player.dead = false;
    }
    IEnumerator EndOfWave() {
        if (waves > 0) {
            waves -= 1;
            enemieskilled = 0;
            totalenemies = 0;
            foreach (var i in enemyspawns) {
                i.canSpawn = false;
                i.currentwave += 1;
                totalenemies += i.enemiestospawn[i.currentwave];
                i.enemiesspawned = 0;
            }
            yield return new WaitForSeconds(4f);
            foreach(var i in enemyspawns) {
                i.canSpawn = true;
            }
        }
    }
   public static IEnumerator SwitchMusic(AudioClip music) {
        AudioManager.instance.StopMusic();
        yield return new WaitForEndOfFrame();
        AudioManager.instance.BGMusic = music;
        AudioManager.instance.PlayMusic(AudioManager.instance.BGMusic);
    }
}
