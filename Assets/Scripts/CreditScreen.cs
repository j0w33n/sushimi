using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClosePanel(GameObject panel) {
        if (panel != null) panel.SetActive(false);
    }
    public void OpenPanel(GameObject panel) {
        if (panel != null) panel.SetActive(true);
    }
}
