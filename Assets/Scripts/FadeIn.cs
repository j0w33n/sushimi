using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {
    public float fadetime;
    [SerializeField]private Image blackscreen;
    // Start is called before the first frame update
    void Start() {
        gameObject.SetActive(true);
        //blackscreen = GetComponentInChildren<Image>();
        StartCoroutine(Disappear());
    }

    // Update is called once per frame
    void Update() {
        if (FindObjectOfType<End>() != null) gameObject.SetActive(false);
    }
    public IEnumerator Appear(bool white = false) {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        if (white) blackscreen.color = Color.white;
        else blackscreen.color = Color.black;
        blackscreen.CrossFadeAlpha(1f, 3, false);
    }
    public IEnumerator Disappear() {
        gameObject.SetActive(true);
        if (blackscreen != null) { blackscreen.CrossFadeAlpha(0f, fadetime, false); }
        yield return new WaitForSeconds(fadetime);
        gameObject.SetActive(false);
    }
}
