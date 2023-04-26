using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Chest : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown() {
        OpenPanel();
    }
    public void OpenPanel() {
        if (panel != null) {
            bool isactive = panel.activeSelf;
            panel.SetActive(!isactive);
        }
    }
}
