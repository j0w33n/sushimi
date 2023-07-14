using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Terresquall;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public GameObject ammo1;
    public GameObject ammo2;
    public GameObject ammo3;
    public GameObject ammo4;
    public GameObject ammo5;
    public GameObject ammo6;
    public GameObject ammo7;
    public GameObject ammo8;
    public GameObject ammo9;
    public GameObject ammo10;
    public GameObject ammo11;
    public GameObject ammo12;
    public GameObject ammo13;
    public GameObject ammo14;
    public GameObject ammo15;
    public GameObject ammo16;

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
    //public Slider ammobar;
    public Slider enemykillcount;
    public GameObject panel, currentroom;
    public AudioClip levelmusic;
    public string leveltoload;
    [SerializeField] VirtualJoystick[] joysticks;
    public List<GameObject> rooms = new List<GameObject>();
    Weapon[] weapons;
    [SerializeField] Image line;
    [SerializeField] int nlines;
    public int ammobar;



    //[SerializeField] float ammobarwidth;
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
        //ammobarwidth = ammobar.GetComponent<RectTransform>().sizeDelta.x;
        //nlines = (int)ammobar.maxValue / 2;
        //AmmoSegments();
    }

    // Update is called once per frame
    void Update() {

        if (ammobar == 0) {
            ammo1.SetActive(false);
            ammo2.SetActive(false);
            ammo3.SetActive(false);
            ammo4.SetActive(false);
            ammo5.SetActive(false);
            ammo6.SetActive(false);
            ammo7.SetActive(false);
            ammo8.SetActive(false);
            ammo9.SetActive(false);
            ammo10.SetActive(false);
            ammo11.SetActive(false);
            ammo12.SetActive(false);
            ammo13.SetActive(false);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 1) {
            ammo1.SetActive(true);
            ammo2.SetActive(false);
            ammo3.SetActive(false);
            ammo4.SetActive(false);
            ammo5.SetActive(false);
            ammo6.SetActive(false);
            ammo7.SetActive(false);
            ammo8.SetActive(false);
            ammo9.SetActive(false);
            ammo10.SetActive(false);
            ammo11.SetActive(false);
            ammo12.SetActive(false);
            ammo13.SetActive(false);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 2) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(false);
            ammo4.SetActive(false);
            ammo5.SetActive(false);
            ammo6.SetActive(false);
            ammo7.SetActive(false);
            ammo8.SetActive(false);
            ammo9.SetActive(false);
            ammo10.SetActive(false);
            ammo11.SetActive(false);
            ammo12.SetActive(false);
            ammo13.SetActive(false);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 3) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(false);
            ammo5.SetActive(false);
            ammo6.SetActive(false);
            ammo7.SetActive(false);
            ammo8.SetActive(false);
            ammo9.SetActive(false);
            ammo10.SetActive(false);
            ammo11.SetActive(false);
            ammo12.SetActive(false);
            ammo13.SetActive(false);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }


        if (ammobar == 4) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(true);
            ammo5.SetActive(false);
            ammo6.SetActive(false);
            ammo7.SetActive(false);
            ammo8.SetActive(false);
            ammo9.SetActive(false);
            ammo10.SetActive(false);
            ammo11.SetActive(false);
            ammo12.SetActive(false);
            ammo13.SetActive(false);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 5) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(true);
            ammo5.SetActive(true);
            ammo6.SetActive(false);
            ammo7.SetActive(false);
            ammo8.SetActive(false);
            ammo9.SetActive(false);
            ammo10.SetActive(false);
            ammo11.SetActive(false);
            ammo12.SetActive(false);
            ammo13.SetActive(false);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 6) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(true);
            ammo5.SetActive(true);
            ammo6.SetActive(true);
            ammo7.SetActive(false);
            ammo8.SetActive(false);
            ammo9.SetActive(false);
            ammo10.SetActive(false);
            ammo11.SetActive(false);
            ammo12.SetActive(false);
            ammo13.SetActive(false);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 7) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(true);
            ammo5.SetActive(true);
            ammo6.SetActive(true);
            ammo7.SetActive(true);
            ammo8.SetActive(false);
            ammo9.SetActive(false);
            ammo10.SetActive(false);
            ammo11.SetActive(false);
            ammo12.SetActive(false);
            ammo13.SetActive(false);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 8) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(true);
            ammo5.SetActive(true);
            ammo6.SetActive(true);
            ammo7.SetActive(true);
            ammo8.SetActive(true);
            ammo9.SetActive(false);
            ammo10.SetActive(false);
            ammo11.SetActive(false);
            ammo12.SetActive(false);
            ammo13.SetActive(false);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 9) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(true);
            ammo5.SetActive(true);
            ammo6.SetActive(true);
            ammo7.SetActive(true);
            ammo8.SetActive(true);
            ammo9.SetActive(true);
            ammo10.SetActive(false);
            ammo11.SetActive(false);
            ammo12.SetActive(false);
            ammo13.SetActive(false);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 10) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(true);
            ammo5.SetActive(true);
            ammo6.SetActive(true);
            ammo7.SetActive(true);
            ammo8.SetActive(true);
            ammo9.SetActive(true);
            ammo10.SetActive(true);
            ammo11.SetActive(false);
            ammo12.SetActive(false);
            ammo13.SetActive(false);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 11) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(true);
            ammo5.SetActive(true);
            ammo6.SetActive(true);
            ammo7.SetActive(true);
            ammo8.SetActive(true);
            ammo9.SetActive(true);
            ammo10.SetActive(true);
            ammo11.SetActive(true);
            ammo12.SetActive(false);
            ammo13.SetActive(false);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 12) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(true);
            ammo5.SetActive(true);
            ammo6.SetActive(true);
            ammo7.SetActive(true);
            ammo8.SetActive(true);
            ammo9.SetActive(true);
            ammo10.SetActive(true);
            ammo11.SetActive(true);
            ammo12.SetActive(true);
            ammo13.SetActive(false);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 13) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(true);
            ammo5.SetActive(true);
            ammo6.SetActive(true);
            ammo7.SetActive(true);
            ammo8.SetActive(true);
            ammo9.SetActive(true);
            ammo10.SetActive(true);
            ammo11.SetActive(true);
            ammo12.SetActive(true);
            ammo13.SetActive(true);
            ammo14.SetActive(false);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 14) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(true);
            ammo5.SetActive(true);
            ammo6.SetActive(true);
            ammo7.SetActive(true);
            ammo8.SetActive(true);
            ammo9.SetActive(true);
            ammo10.SetActive(true);
            ammo11.SetActive(true);
            ammo12.SetActive(true);
            ammo13.SetActive(true);
            ammo14.SetActive(true);
            ammo15.SetActive(false);
            ammo16.SetActive(false);

        }

        if (ammobar == 15) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(true);
            ammo5.SetActive(true);
            ammo6.SetActive(true);
            ammo7.SetActive(true);
            ammo8.SetActive(true);
            ammo9.SetActive(true);
            ammo10.SetActive(true);
            ammo11.SetActive(true);
            ammo12.SetActive(true);
            ammo13.SetActive(true);
            ammo14.SetActive(true);
            ammo15.SetActive(true);
            ammo16.SetActive(false);

        }

        if (ammobar == 16) {
            ammo1.SetActive(true);
            ammo2.SetActive(true);
            ammo3.SetActive(true);
            ammo4.SetActive(true);
            ammo5.SetActive(true);
            ammo6.SetActive(true);
            ammo7.SetActive(true);
            ammo8.SetActive(true);
            ammo9.SetActive(true);
            ammo10.SetActive(true);
            ammo11.SetActive(true);
            ammo12.SetActive(true);
            ammo13.SetActive(true);
            ammo14.SetActive(true);
            ammo15.SetActive(true);
            ammo16.SetActive(true);

        }


        //print(PlayerPrefs.GetString("Current Level"));
        //print(PlayerPrefs.GetInt("Current Room"));
        if (!FindObjectOfType<End>(true).endlevel) ShowTime();
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
                foreach (var i in currentroom.GetComponent<Room>().entrance) {
                    i.SetActive(false);
                }
                AudioManager.instance.PlaySFX(AudioManager.instance.entranceSound);
                player.GetComponentInChildren<Weapon>().ammo = player.GetComponentInChildren<Weapon>().maxammo;
                //player.arrow.gameObject.SetActive(true);
            }
        }
        if (totalenemieskilled >= 10 /*&& !currentroom.GetComponent<Room>().roomstart*/) {
            StartCoroutine(upgradepanel());
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

        timer.GetComponent<Text>().text = "Time taken: " + minutes + ":" + seconds;
    }
    /*void AmmoSegments() {
        float segmentlength = ammobarwidth / (nlines + 1);
        //float offset = segmentlength;
        //Image img = Instantiate(line);
        //img.transform.SetParent(ammobar.transform);
        //img.rectTransform.localScale = new Vector3(1, 1, 1);
        //img.rectTransform.localPosition += new Vector3(segmentlength + offset * nlines, 0, 0);
        List<Image> lines = new List<Image>();
        for (int i = 0; i < nlines; i++) {
            Image img = Instantiate(line, ammobar.transform.position - new Vector3(333.8f, 0, 0), Quaternion.identity, ammobar.transform);
            lines.Add(img);
        }
        for (int i = 0; i < lines.Count; i++) {
            if (i == 0) lines[i].transform.position += new Vector3(segmentlength * 2, 0, 0);
            else lines[i].transform.position = lines[i - 1].transform.position + new Vector3(segmentlength * 2, 0, 0);
        }
    }*/
}
