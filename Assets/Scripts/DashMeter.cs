using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashMeter : MonoBehaviour
{
    public Image dashBar;

    Player player;

    /*void Start() {
        player = FindObjectOfType<Player>();
        dashBar = GetComponent<Image>();
    }*/

    /*void Update() {
        float fillamt = 1 - (player.nextdashtime - Time.time) / player._dashingTime;
        dashBar.fillAmount = Mathf.Clamp01(fillamt);
        print(dashBar.fillAmount);
       // dashBar.fillAmount = 1 - player.GetCurrentDashingCooldown() / player._dashingTime;

        //DashMeterFiller(fillAmount);
        ColorChanger();
    }*/
    /*void DashMeterFiller(float fillAmount) {

        dashBar.fillAmount = fillAmount;

    }*/

    /*void ColorChanger() {

        Color dashColor = new Color(0, 1, 1, 1);
        dashBar.color = dashColor;
    }*/

}
