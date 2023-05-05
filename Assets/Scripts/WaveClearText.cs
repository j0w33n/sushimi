using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveClearText : MonoBehaviour
{
    Animator anim;
    LevelManager levelManager;
    [SerializeField] float activetime;
    public float ogactivetime;
    public bool wave;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        anim = GetComponent<Animator>();
        activetime = ogactivetime;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Wave", wave);
        anim.SetFloat("Active", activetime);
        if (levelManager.wavecomplete) {
            wave = true;
            activetime -= Time.deltaTime;
            if (activetime <= 0) {
                StartCoroutine(WaitForAnim());
            }
        }
        else if(levelManager.waves == 0) {
            gameObject.GetComponent<Text>().text = "ROOM CLEAR";
            wave = true;
            activetime -= Time.deltaTime;
            if (activetime <= 0) {
                StartCoroutine(RoomClear());
            }
        }
    }
    IEnumerator WaitForAnim() {
        wave = false;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        levelManager.wavecomplete = false;
        activetime = ogactivetime;
    }
    IEnumerator RoomClear() {
        wave = false;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        gameObject.SetActive(false);
    }
}
