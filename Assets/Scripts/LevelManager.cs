using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Terresquall;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    Player player;
    public List<EnemySpawner> enemyspawns;
    public float waitToRespawn;
    public bool wavecomplete;
    public int waves;
    public int enemieskilled;
    public int totalenemieskilled;
    public int totalenemies = 0;
    public GameObject partcount, timer;
    public int parts;
    public Slider ammobar;
    public Slider enemykillcount;
    public GameObject panel, currentroom;
    public AudioClip levelmusic;
    public string leveltoload;
    [SerializeField] VirtualJoystick[] joysticks;
    public List<GameObject> rooms = new List<GameObject>();
    Weapon[] weapons;
    public Sprite[] ammosprites,healthsprites;
    public Image ammoimg,healthimg;
    public int continues,ammoiter,healthiter;
    // Start is called before the first frame update
    void Start() {
        parts = PlayerPrefs.GetInt("Parts", 0);
        totalenemieskilled = PlayerPrefs.GetInt("Total Enemies Killed", 0);
        StartCoroutine(SwitchMusic(levelmusic));
        player = FindObjectOfType<Player>();
        VirtualJoystick.instances.Insert(0, joysticks[0]);
        VirtualJoystick.instances.Insert(1, joysticks[1]);
        PlayerPrefs.SetString("Current Level", SceneManager.GetActiveScene().name);
        player.transform.position = rooms[PlayerPrefs.GetInt("Current Room")].transform.position;
        weapons = FindObjectsOfType<Weapon>();
        foreach (Weapon w in weapons) {
            w.projectileprefab.GetComponent<ProjectileScript>().upgraded = false;
            w.explodingprojectile.GetComponent<ProjectileScript>().upgraded = false;
            w.slowingprojectile.GetComponent<ProjectileScript>().upgraded = false;
            w.projectileprefab.GetComponent<ProjectileScript>().damage = 1;
            w.explodingprojectile.GetComponent<ProjectileScript>().damage = 1;
            w.slowingprojectile.GetComponent<ProjectileScript>().damage = 1;
        }
        continues = 10;
        if (player.GetComponentInChildren<DoubleBarrelGun>() != null) ammoiter = 3;
        else ammoiter = 0;
        healthiter = 0;
    }
    // Update is called once per frame
    void Update() {
        //print(PlayerPrefs.GetString("Current Level"));
        //print(PlayerPrefs.GetInt("Current Room"));
        healthimg.sprite = healthsprites[healthiter];
        ammoimg.sprite = ammosprites[ammoiter];
        if (!FindObjectOfType<End>(true).endlevel) ShowTime();
        partcount.GetComponent<Text>().text = "x " + parts.ToString();
        enemykillcount.value = totalenemieskilled;
        if (currentroom.GetComponent<Room>().roomstart) {
            PlayerPrefs.SetInt("Current Room", rooms.IndexOf(currentroom));
            if (enemieskilled >= totalenemies) {
                wavecomplete = true;
                StartCoroutine(EndOfWave());
            }
            if (waves == 0 && currentroom.GetComponent<Room>().roomstart) {
                currentroom.GetComponent<Room>().roomstart = false;
                foreach (var i in currentroom.GetComponent<Room>().entrance) {
                    i.SetActive(false);
                    //Destroy(Instantiate(currentroom.GetComponent<Room>().particle,i.transform.position,Quaternion.identity),1);
                }
                AudioManager.instance.PlaySFX(AudioManager.instance.entranceSound);
                player.GetComponentInChildren<Weapon>().ammo = player.GetComponentInChildren<Weapon>().maxammo;
            }
        }
        if (totalenemieskilled >= 10 && !currentroom.GetComponent<Room>().roomstart) {
            StartCoroutine(upgradepanel());
        }
        if (continues == 0) {
            StartCoroutine(SpecialRespawn());
        }

        if (FindObjectOfType<End>(true).endlevel) {
            string scenename;

            scenename = SceneManager.GetActiveScene().name;
            //Total
            PlayerPrefs.SetFloat("TotalParts", PlayerPrefs.GetFloat("TotalParts") + parts);
            PlayerPrefs.SetFloat("TotalTime", PlayerPrefs.GetFloat("TotalTime") + Time.timeSinceLevelLoad);


            //Per Level
            if (scenename == "Tutorial") {
                PlayerPrefs.SetFloat("Level0Parts", parts);
                PlayerPrefs.SetFloat("Level0Time", Time.timeSinceLevelLoad);
                PlayerPrefs.SetInt("Level0Completed", 1);
            } else if (scenename == "Level 1") {
                PlayerPrefs.SetFloat("Level1Parts", parts);
                PlayerPrefs.SetFloat("Level1Time", Time.timeSinceLevelLoad);
                PlayerPrefs.SetInt("Level1Completed", 1);
            } else if (scenename == "Level 2") {
                PlayerPrefs.SetFloat("Level2Parts", parts);
                PlayerPrefs.SetFloat("Level2Time", Time.timeSinceLevelLoad);
                PlayerPrefs.SetInt("Level2Completed", 1);
            }
        }
    }
    public void Respawn() {
        StartCoroutine(RespawnCo());
    }
    IEnumerator upgradepanel() {
        panel.GetComponent<UpgradePanel>().SetUpgrades();
        panel.SetActive(true);
        panel.GetComponent<UpgradePanel>().isactive = true;
        totalenemieskilled = 0;
        yield return new WaitForSeconds(panel.GetComponent<UpgradePanel>().anim.GetCurrentAnimatorStateInfo(0).length + panel.GetComponent<UpgradePanel>().anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //Time.timeScale = 0;
    }
    IEnumerator RespawnCo() {
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        yield return new WaitForSeconds(0.25f);
        player.gameObject.SetActive(false); // deactivates player
        yield return new WaitForSeconds(waitToRespawn); // how long to wait before respawning player
        player.gameObject.SetActive(true); // reactivates player
        FadeIn screen = FindObjectOfType<FadeIn>(true);
        if(screen != null) {
            //screen.gameObject.SetActive(true);
            StartCoroutine(screen.Appear());
            StartCoroutine(screen.Disappear());
        }
        player.transform.position = player.respawnpoint; // moves player to respawn point
        player.dead = false;
    }
   public IEnumerator SpecialRespawn() {
        PlayerPrefs.SetInt("Current Room", 0);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        yield return new WaitForSeconds(0.25f);
        player.gameObject.SetActive(false); // deactivates player
        yield return new WaitForSeconds(waitToRespawn); // how long to wait before respawning player
        player.gameObject.SetActive(true); // reactivates player
        //StartCoroutine(FindObjectOfType<FadeIn>(true).Appear());
        //StartCoroutine(FindObjectOfType<FadeIn>(true).Disappear());
        player.transform.position = rooms[0].transform.position; // moves player to respawn point
        player.dead = false;
        continues = 10;
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
            foreach (var i in enemyspawns) {
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
    void ShowTime() {
        int seconds, minutes;
        minutes = (int)Time.timeSinceLevelLoad / 60;
        seconds = (int)Time.timeSinceLevelLoad % 60;
        if(seconds < 10)timer.GetComponent<Text>().text = "Time taken: " + minutes + ":" + "0" + seconds;
        else timer.GetComponent<Text>().text = "Time taken: " + minutes + ":" + seconds;
        
    }

    public void GameWin() {
        string scenename;

        scenename = SceneManager.GetActiveScene().name;
        //Total
        PlayerPrefs.SetFloat("TotalParts", PlayerPrefs.GetFloat("TotalParts") + parts);
        PlayerPrefs.SetFloat("TotalTime", PlayerPrefs.GetFloat("TotalTime") + Time.timeSinceLevelLoad);


        //Per Level
        if (scenename == "Tutorial") {
            PlayerPrefs.SetFloat("Level0Parts", parts);
            PlayerPrefs.SetFloat("Level0Time", Time.timeSinceLevelLoad);
            PlayerPrefs.SetInt("Level0Completed", 1);
        } else if (scenename == "Level 1") {
            PlayerPrefs.SetFloat("Level1Parts", parts);
            PlayerPrefs.SetFloat("Level1Time", Time.timeSinceLevelLoad);
            PlayerPrefs.SetInt("Level1Completed", 1);
        } else if (scenename == "Level 2") {
            PlayerPrefs.SetFloat("Level2Parts", parts);
            PlayerPrefs.SetFloat("Level2Time", Time.timeSinceLevelLoad);
            PlayerPrefs.SetInt("Level2Completed", 1);
        }


    }
}
