using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {
    public float fadetime;
    private Image blackscreen;
    // Start is called before the first frame update
    void Start() {
        blackscreen = GetComponent<Image>();
        StartCoroutine(Disappear());
    }

    // Update is called once per frame
    void Update() {
    }
    public IEnumerator Disappear() {
        blackscreen.CrossFadeAlpha(0f, fadetime, false);
        yield return new WaitForSeconds(fadetime);
        gameObject.SetActive(false);
    }
    public IEnumerator Appear() {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        blackscreen.CrossFadeAlpha(1f, 3, false);
    }
}
