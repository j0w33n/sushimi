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
    public Image activebuff;
   /* [System.Serializable]
    public class UpgradeData {
        public UpgradeType upgradeType;
        public float floatValue;
        public int intValue;
        public bool boolValue;
    }

    public enum UpgradeType {
        IncreaseFireRate,
        IncreaseHealth,
        IncreaseMaxAmmo,
        IncreaseDamage,
        DoubleBarrelGun,
        SlowingBullets,
        ExplodingBullets
    }

    public List<UpgradeData> upgrades = new List<UpgradeData>();
    private int currentUpgradeIndex = 0;
    // Switch to the next upgrade
    public void SwitchUpgrade() {
        currentUpgradeIndex++;
        if (currentUpgradeIndex >= upgrades.Count)
            currentUpgradeIndex = 0;

        ApplyUpgrade(upgrades[currentUpgradeIndex]);
    }
    private void ApplyUpgrade(UpgradeData upgrade) {
        switch (upgrade.upgradeType) {
            case UpgradeType.IncreaseFireRate:
                IncreaseFireRate(upgrade.floatValue);
                break;
            case UpgradeType.IncreaseHealth:
                IncreaseHealth(upgrade.floatValue);
                break;
            case UpgradeType.IncreaseMaxAmmo:
                IncreaseMaxAmmo(upgrade.intValue);
                break;
            case UpgradeType.IncreaseDamage:
                IncreaseDamage(upgrade.intValue);
                break;
            case UpgradeType.DoubleBarrelGun:
                DoubleBarrelGun();
                break;
            case UpgradeType.SlowingBullets:
                SlowingBullets();
                break;
            case UpgradeType.ExplodingBullets:
                ExplodingBullets();
                break;
        }
        upgradePanel.active = false;
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
        activebuff.sprite = GetComponent<Image>().sprite;
    }*/
    public void IncreaseFireRate(float value) {
        foreach(var i in shooting) {
            i.firerate -= value;
            i.reloadspeed += value;
            upgradePanel.active = false;
            activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
            activebuff.sprite = GetComponent<Image>().sprite;
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
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
        activebuff.sprite = GetComponent<Image>().sprite;
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
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
        activebuff.sprite = GetComponent<Image>().sprite;
    }
    public void IncreaseDamage(int value) {
        foreach(var i in shooting) {
            i.projectileprefab.GetComponent<ProjectileScript>().damage += value;
            i.explodingprojectile.GetComponent<ProjectileScript>().damage += value;
            i.slowingprojectile.GetComponent<ProjectileScript>().damage += value;
            i.projectileprefab.GetComponent<ProjectileScript>().upgraded = true; ;
            i.explodingprojectile.GetComponent<ProjectileScript>().upgraded = true;
            i.slowingprojectile.GetComponent<ProjectileScript>().upgraded = true;
        }
        upgradePanel.active = false;
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
        activebuff.sprite = GetComponent<Image>().sprite;
    }
    public void DoubleBarrelGun() {
        player.SwitchWeapon(1);
        upgradePanel.active = false;
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
        activebuff.sprite = GetComponent<Image>().sprite;
    }
    public void SlowingBullets() {
        foreach(var i in shooting) {
            i.slow = true;
        }
        upgradePanel.active = false;
        activebuff.color = new Color(activebuff.color.r, activebuff.color.g, activebuff.color.b, 1);
        activebuff.sprite = GetComponent<Image>().sprite;
    }
    public void ExplodingBullets() {
        foreach(var i in shooting) {
            i.explode = true;
        }
        upgradePanel.active = false;
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

    // Update is called once per frame
    void Update()
    {
        if (!upgradePanel.active) {
            GetComponent<Button>().interactable = false;
            GetComponent<Button>().GetComponent<Image>().color = new Color(255, 255, 255, 0);

        } else {
            GetComponent<Button>().interactable = true;
            GetComponent<Button>().GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }
}
