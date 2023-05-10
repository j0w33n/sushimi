using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public List<GameObject> upgradepool;
    public GameObject upgrade1,upgrade2,upgrade3;
    // Start is called before the first frame update
    void Start()
    {
        upgrade1 = upgradepool[Random.Range(0, upgradepool.Count)];
        upgrade2 = upgradepool[Random.Range(0, upgradepool.Count)];
        upgrade3 = upgradepool[Random.Range(0, upgradepool.Count)];
    }
    public void ClosePanel() {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
