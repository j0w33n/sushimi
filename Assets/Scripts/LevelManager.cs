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
    public GameObject partcount,timer;
    public int parts;
    public Slider ammobar, enemykillcount;
    public GameObject panel, currentroom;
    public AudioClip levelmusic;
    public string leveltoload;
    [SerializeField] VirtualJoystick[] joysticks;
    public List<GameObject> rooms = new List<GameObject>();
    Weapon[] weapons;
    [SerializeField]Image line;
    [SerializeField]int nlines;
    [SerializeField]float ammobarwidth;
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
        weapons = FindObjectsOfType<Weapon>();
        foreach(Weapon w in weapons) {
            w.projectileprefab.GetComponent<ProjectileScript>().upgraded = false;
            w.explodingprojectile.GetComponent<ProjectileScript>().upgraded = false;
            w.slowingprojectile.GetComponent<ProjectileScript>().upgraded = false;
            w.projectileprefab.GetComponent<ProjectileScript>().damage = 1;
            w.explodingprojectile.GetComponent<ProjectileScript>().damage = 1;
            w.slowingprojectile.GetComponent<ProjectileScript>().damage = 1;
        }
        ammobarwidth = ammobar.GetComponent<RectTransform>().sizeDelta.x;
        nlines = (int)ammobar.maxValue / 2;
        AmmoSegments();
    }

    // Update is called once per frame
    void Update()
    {
        //print(PlayerPrefs.GetString("Current Level"));
        //print(PlayerPrefs.GetInt("Current Room"));
        if (!FindObjectOfType<End>().endlevel) ShowTime();
        partcount.GetComponent<Text>().text = "x " + parts.ToString();
        enemykillcount.value = totalenemieskilled;
        if (currentroom.GetComponent<Room>().roomstart) {
            PlayerPrefs.SetInt("Current Room", rooms.IndexOf(currentroom));
            //player.arrow.gameObject.SetActive(false);
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
                player.GetComponentInChildren<Weapon>().ammo = player.GetComponentInChildren<Weapon>().maxammo;
                //player.arrow.gameObject.SetActive(true);
            }
        }
        if(totalenemieskilled >= 10 && !currentroom.GetComponent<Room>().roomstart) {
            panel.GetComponent<UpgradePanel>().SetUpgrades();
            StartCoroutine(upgradepanel());
        }
    }
    public void Respawn() {
        StartCoroutine(RespawnCo());
    }
    IEnumerator upgradepanel() {
        panel.SetActive(true);
        panel.GetComponent<UpgradePanel>().isactive = true;
        totalenemieskilled = 0;
        yield return new WaitForSeconds(panel.GetComponent<UpgradePanel>().anim.GetCurrentAnimatorStateInfo(0).length + panel.GetComponent<UpgradePanel>().anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Time.timeScale = 0;
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
    void ShowTime() {
        int seconds,minutes;
        minutes = (int)Time.timeSinceLevelLoad / 60;
        seconds = (int)Time.timeSinceLevelLoad % 60;

        timer.GetComponent<Text>().text = "Time taken: " + minutes + ":" + seconds;
    }
    void AmmoSegments() {
        float segmentlength = ammobarwidth / (nlines + 1);
        //float offset = segmentlength;
        //Image img = Instantiate(line);
        //img.transform.SetParent(ammobar.transform);
        //img.rectTransform.localScale = new Vector3(1, 1, 1);
        //img.rectTransform.localPosition += new Vector3(segmentlength + offset * nlines, 0, 0);
        List<Image> lines = new List<Image>();
        for (int i = 0; i < nlines; i++) {
            Image img = Instantiate(line,ammobar.transform.position - new Vector3(333.8f,0,0), Quaternion.identity, ammobar.transform);
            lines.Add(img);
        }
        for(int i=0;i< lines.Count;i++) {
            if (i == 0) lines[i].transform.position += new Vector3(segmentlength*2, 0, 0);
            else lines[i].transform.position = lines[i - 1].transform.position + new Vector3(segmentlength*2, 0, 0);
        }
    }
}
