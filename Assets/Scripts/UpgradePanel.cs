using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public List<GameObject> upgradepool;
    public Button[] upgrades;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++) {
            upgrades[i] = upgradepool[Random.Range(0, upgradepool.Count)].GetComponent<Button>();
        }
    }
    public void ClosePanel() {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
