using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashMeter : MonoBehaviour
{
    public Image dashBar;

    Player player;

    void Reset() {
        player = FindObjectOfType<Player>();
        dashBar = GetComponent<Image>();
    }

    void Update() {

        /*float fillAmount = 1 - player.GetCurrentDashingCooldown() / player._dashingTime;

        DashMeterFiller(fillAmount);
        ColorChanger(fillAmount);*/
    }
    void DashMeterFiller(float fillAmount) {

        dashBar.fillAmount = fillAmount;

    }

    void ColorChanger(float fillAmount) {

        Color dashColor = new Color(0, 1, 1, 1);
        dashBar.color = dashColor;
    }

}
