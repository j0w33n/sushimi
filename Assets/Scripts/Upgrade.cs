using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Upgrade : MonoBehaviour
{
    Player player;
    Weapon[] shooting;
    LevelManager levelManager;
    UpgradePanel upgradePanel;
    bool switchupgrade;
    public Image activebuff;
    public Sprite buffimage;
    public void IncreaseFireRate(float value) {
        SwitchUpgrade();
        if (!switchupgrade) return;
        foreach(var i in shooting) {
            i.firerate -= value;
            i.reloadspeed += value;
            ClosePanel();
        }
    }
    public void IncreaseHealth(float value) {
        SwitchUpgrade();
        if (!switchupgrade) return;
        player.maxhitpoints += value;
        if (player.maxhitpoints > player.absmaxhealth) player.maxhitpoints = player.absmaxhealth;
        player.hitpoints = player.maxhitpoints;
        if (player.maxhitpoints <= player.absmaxhealth && levelManager.healthiter != levelManager.healthsprites.Length) levelManager.healthiter += 1;
        ClosePanel();
    }
    public void IncreaseMaxAmmo(int value) {
        SwitchUpgrade();
        if (!switchupgrade) return;
        bool caniterate = false;
        foreach (var i in shooting) {
            i.maxammo += value;
            if (i.maxammo > i.absmaxammo) i.maxammo = i.absmaxammo;
            if (i.maxammo <= i.absmaxammo && levelManager.ammoiter != levelManager.ammosprites.Length) caniterate = true;
            i.ammo = i.maxammo;
        }
        if(caniterate) levelManager.ammoiter += 1;
        ClosePanel();
    }
    public void IncreaseDamage(int value) {
        SwitchUpgrade();
        if (!switchupgrade) return;
        foreach (var i in shooting) {
            i.projectileprefab.GetComponent<ProjectileScript>().damage += value;
            i.explodingprojectile.GetComponent<ProjectileScript>().damage += value;
            i.slowingprojectile.GetComponent<ProjectileScript>().damage += value;
            i.projectileprefab.GetComponent<ProjectileScript>().upgraded = true; ;
            i.explodingprojectile.GetComponent<ProjectileScript>().upgraded = true;
            i.slowingprojectile.GetComponent<ProjectileScript>().upgraded = true;
        }
        ClosePanel();
    }
    public void SlowingBullets() {
        SwitchUpgrade();
        if (!switchupgrade) return;
        foreach (var i in shooting) {
            i.slow = true;
            i.explode = false;
        }
        ClosePanel();
    }
    public void ExplodingBullets() {
        SwitchUpgrade();
        if (!switchupgrade) return;
        foreach (var i in shooting) {
            i.explode = true;
            i.slow = false;
        }
        ClosePanel();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        shooting = player.GetComponentsInChildren<Weapon>(true);
        levelManager = FindObjectOfType<LevelManager>();
        upgradePanel = FindObjectOfType<UpgradePanel>();
        activebuff = GameObject.FindGameObjectWithTag("Buff").GetComponent<Image>();
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 0);
    }
    public void SwitchUpgrade() { switchupgrade = !switchupgrade; }
    void ClosePanel() {
        upgradePanel.isactive = false;
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
        activebuff.sprite = buffimage;
        //Time.timeScale = 1;
        upgradePanel.gameObject.SetActive(false);
        foreach (Transform i in upgradePanel.GetComponentsInChildren<Transform>()) {
            if(i != upgradePanel.transform)Destroy(i.gameObject);
        }
        upgradePanel.upgrades.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        if (!upgradePanel.isactive) {
            GetComponent<Button>().interactable = false;
            GetComponent<Button>().GetComponent<Image>().color = new Color(255, 255, 255, 0);
        } 
        else {
            GetComponent<Button>().interactable = true;
            GetComponent<Button>().GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }
}
