using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;
public class Chest : MonoBehaviour
{
    public GameObject panel;
    //private TouchManager touchManager;
    // Start is called before the first frame update
    void Start()
    {
        //touchManager = FindObjectOfType<TouchManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTouchTap(Touch t) {
        if(t.position == (Vector2)transform.position) {
            OpenPanel();
        }
    }
    /*private void OnMouseDown() {
        OpenPanel();
    }*/
    public void OpenPanel() {
        if (panel != null) {
            bool isactive = panel.activeSelf;
            panel.SetActive(!isactive);
        }
    }
}
