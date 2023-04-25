using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public float hitpoints;
    public float maxhitpoints;
    public Slider healthbar;
    public Color lowhealth;
    public Color highhealth;
    public Color damageFeedbackColor;
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

        healthbar.fillRect.GetComponent<Image>().color = Color.Lerp(lowhealth, highhealth, healthbar.normalizedValue);
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
