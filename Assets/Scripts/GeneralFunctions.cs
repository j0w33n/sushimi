using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralFunctions : MonoBehaviour
{
    public float hitpoints;
    public float maxhitpoints;
    public Slider healthbar;
    public Color Low;
    public Color High;
    public Color damageFeedbackColor = new Color(1, 0, 0);
    public Color originalColor;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    public void TakeHit(float damage) {
        hitpoints -= damage;
        //Healthbar.SetHealth(Hitpoints, MaxHitpoints);
    }
    public void SetHealth(float health, float maxHealth) {
        healthbar.value = health;
        healthbar.maxValue = maxHealth;

        healthbar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, healthbar.normalizedValue);
    }
    public IEnumerator DamageFeedback() {
        sr.color = damageFeedbackColor;
        yield return new WaitForSeconds(0.2f);
        sr.color = originalColor;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
