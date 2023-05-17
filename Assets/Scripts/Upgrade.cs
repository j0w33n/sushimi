using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    Player player;
    Weapon[] shooting;
    LevelManager levelManager;
    UpgradePanel upgradePanel;
    public void IncreaseFireRate(float value) {
        foreach(var i in shooting) {
            i.firerate += value;
            i.reloadspeed += value;
            upgradePanel.active = false;
        }
    }
    public void IncreaseHealth(float value) {
        player.maxhitpoints += value;
        player.hitpoints = player.maxhitpoints;
        RectTransform rect = player.healthbar.fillRect.GetComponent<RectTransform>();
        RectTransform fillarea = player.healthbar.fillRect.GetComponentInParent<RectTransform>();
        RectTransform bg = player.healthbar.GetComponentInChildren<Image>().GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x + value * 2, rect.sizeDelta.y);
        fillarea.right = new Vector3(fillarea.right.x + value, fillarea.right.y, fillarea.right.z);
        bg.sizeDelta = new Vector2(bg.sizeDelta.x + value, bg.sizeDelta.y);
        upgradePanel.active = false;
    }
    public void IncreaseMaxAmmo(int value) {
        foreach(var i in shooting) {
            i.maxammo += value;
            i.ammo = i.maxammo;
        }
        RectTransform rect = levelManager.ammobar.fillRect.GetComponent<RectTransform>();
        RectTransform fillarea = levelManager.ammobar.fillRect.GetComponentInParent<RectTransform>();
        RectTransform bg = levelManager.ammobar.fillRect.GetComponentInChildren<Image>().GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x + value * 2, rect.sizeDelta.y);
        fillarea.right = new Vector3(fillarea.right.x + value, fillarea.right.y, fillarea.right.z);
        bg.sizeDelta = new Vector2(bg.sizeDelta.x + value, bg.sizeDelta.y);
        upgradePanel.active = false;
    }
    public void IncreaseDamage(int value) {
        foreach(var i in shooting) {
            i.projectileprefab.GetComponent<ProjectileScript>().damage += value;
        }
        upgradePanel.active = false;
    }
    public void DoubleBarrelGun() {
        player.SwitchWeapon(1);
        upgradePanel.active = false;
    }
    public void SlowingBullets() {
        foreach(var i in shooting) {
            i.slow = true;
        }
        upgradePanel.active = false;
    }
    public void ExplodingBullets() {
        foreach(var i in shooting) {
            i.explode = true;
        }
        upgradePanel.active = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        shooting = player.GetComponentsInChildren<Weapon>();
        levelManager = FindObjectOfType<LevelManager>();
        upgradePanel = FindObjectOfType<UpgradePanel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!upgradePanel.active) {
            GetComponent<Button>().interactable = false;
            GetComponent<Button>().GetComponent<Image>().color = new Color(255, 255, 255, 0);
            GetComponent<Button>().GetComponentInChildren<Text>().color = new Color(50, 50, 50, 0);

        } else {
            GetComponent<Button>().interactable = true;
            GetComponent<Button>().GetComponent<Image>().color = new Color(255, 255, 255, 255);
            GetComponent<Button>().GetComponentInChildren<Text>().color = Color.black;
        }
    }
}
