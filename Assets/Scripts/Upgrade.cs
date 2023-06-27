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
    public void IncreaseFireRate(float value) {
        SwitchUpgrade();
        if (!switchupgrade) return;
        foreach(var i in shooting) {
            i.firerate -= value;
            i.reloadspeed += value;
            upgradePanel.isactive = false;
            activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
            activebuff.sprite = GetComponent<Image>().sprite;
        }
    }
    public void IncreaseHealth(float value) {
        SwitchUpgrade();
        if (!switchupgrade) return;
        player.maxhitpoints += value;
        player.hitpoints = player.maxhitpoints;
        RectTransform rect = player.healthbar.fillRect.GetComponent<RectTransform>();
        RectTransform fillarea = player.healthbar.fillRect.GetComponentInParent<RectTransform>();
        RectTransform bg = player.healthbar.GetComponentInChildren<Image>().GetComponent<RectTransform>();
        rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, value);
        fillarea.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, value);
        bg.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, value);
        //rect.sizeDelta = new Vector2(rect.sizeDelta.x + value * 2, rect.sizeDelta.y);
        //fillarea.right = new Vector3(fillarea.right.x + value * 2, fillarea.right.y, fillarea.right.z);
        //bg.sizeDelta = new Vector2(bg.sizeDelta.x + value * 2, bg.sizeDelta.y);
        upgradePanel.isactive = false;
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
        activebuff.sprite = GetComponent<Image>().sprite;
    }
    public void IncreaseMaxAmmo(int value) {
        SwitchUpgrade();
        if (!switchupgrade) return;
        foreach (var i in shooting) {
            i.maxammo += value;
            i.ammo = i.maxammo;
        }
        RectTransform rect = levelManager.ammobar.fillRect.GetComponent<RectTransform>();
        RectTransform fillarea = levelManager.ammobar.fillRect.GetComponentInParent<RectTransform>();
        RectTransform bg = levelManager.ammobar.fillRect.GetComponentInChildren<Image>().GetComponent<RectTransform>();
        rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, value);
        fillarea.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, value);
        bg.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, value);
        /*rect.sizeDelta = new Vector2(rect.sizeDelta.x + value * 2, rect.sizeDelta.y);
        fillarea.right = new Vector3(fillarea.right.x + value * 2, fillarea.right.y, fillarea.right.z);
        bg.sizeDelta = new Vector2(bg.sizeDelta.x + value * 2, bg.sizeDelta.y);*/
        upgradePanel.isactive = false;
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
        activebuff.sprite = GetComponent<Image>().sprite;
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
        upgradePanel.isactive = false;
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
        activebuff.sprite = GetComponent<Image>().sprite;
    }
    /*public void DoubleBarrelGun() {
        SwitchUpgrade();
        if (!switchupgrade) return;
        player.SwitchWeapon(1);
        upgradePanel.isactive = false;
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
        activebuff.sprite = GetComponent<Image>().sprite;
    }*/
    public void SlowingBullets() {
        SwitchUpgrade();
        if (!switchupgrade) return;
        foreach (var i in shooting) {
            i.slow = true;
            i.explode = false;
        }
        upgradePanel.isactive = false;
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
        activebuff.sprite = GetComponent<Image>().sprite;
    }
    public void ExplodingBullets() {
        SwitchUpgrade();
        if (!switchupgrade) return;
        foreach (var i in shooting) {
            i.explode = true;
            i.slow = false;
        }
        upgradePanel.isactive = false;
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
        activebuff.sprite = GetComponent<Image>().sprite;
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
