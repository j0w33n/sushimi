using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    Player player;
    ShootingScript shooting;
    LevelManager levelManager;
    public bool clicked;
    public void IncreaseFireRate(float value) {
        clicked = true;
        shooting.firerate += value;
    }
    public void IncreaseHealth(float value) {
        clicked = true;
        player.maxhitpoints += value;
        player.hitpoints = player.maxhitpoints;
        RectTransform rect = player.healthbar.fillRect.GetComponent<RectTransform>();
        RectTransform fillarea = player.healthbar.fillRect.GetComponentInParent<RectTransform>();
        RectTransform bg = player.healthbar.GetComponentInChildren<Image>().GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x + value * 2, rect.sizeDelta.y);
        fillarea.right = new Vector3(fillarea.right.x + value, fillarea.right.y, fillarea.right.z);
        bg.sizeDelta = new Vector2(bg.sizeDelta.x + value, bg.sizeDelta.y);
    }
    public void IncreaseMaxAmmo(int value) {
        clicked = true;
        shooting.maxammo += value;
        shooting.ammo = shooting.maxammo;
        RectTransform rect = levelManager.ammobar.fillRect.GetComponent<RectTransform>();
        RectTransform fillarea = levelManager.ammobar.fillRect.GetComponentInParent<RectTransform>();
        RectTransform bg = levelManager.ammobar.fillRect.GetComponentInChildren<Image>().GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x + value * 2, rect.sizeDelta.y);
        fillarea.right = new Vector3(fillarea.right.x + value, fillarea.right.y, fillarea.right.z);
        bg.sizeDelta = new Vector2(bg.sizeDelta.x + value, bg.sizeDelta.y);
    }
    public void NotClicked() {
        clicked = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        shooting = player.GetComponentInChildren<ShootingScript>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
