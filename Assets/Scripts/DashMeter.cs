using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashMeter : MonoBehaviour
{
    public Image dashBar;

    Player player;

    void Start() {
        player = FindObjectOfType<Player>();
        dashBar = GetComponent<Image>();
    }

    void Update() {
        if (player._isDashing) {
            dashBar.CrossFadeAlpha(0.15f, player.immunityDuration,false);
        } 
        else {
            dashBar.CrossFadeAlpha(1f, player._dashingTime, false);
        }
    }
}
