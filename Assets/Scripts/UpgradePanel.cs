using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public List<GameObject> upgradepool;
    public Button[] upgrades;
    Animator anim;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++) {
            upgrades[i] = upgradepool[Random.Range(0, upgradepool.Count)].GetComponent<Button>();
        }
        anim = GetComponent<Animator>();
        SetUpgrades();
    }
    void SetUpgrades() {
        Button upgrade1 = Instantiate(upgrades[0]);
        upgrade1.transform.SetParent(transform);
        upgrade1.GetComponent<RectTransform>().anchorMin = new Vector2(0f,0.5f);
        upgrade1.GetComponent<RectTransform>().anchorMax = new Vector2(0f,0.5f);
        upgrade1.GetComponent<RectTransform>().localPosition = new Vector3(150, 0, 0);
        upgrade1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        Button upgrade2 = Instantiate(upgrades[1]);
        upgrade2.transform.SetParent(transform);
        upgrade2.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        upgrade2.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        upgrade2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        upgrade2.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        upgrade2.interactable = true;

        Button upgrade3 = Instantiate(upgrades[2]);
        upgrade3.transform.SetParent(transform);
        upgrade3.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 0.5f);
        upgrade3.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 0.5f);
        upgrade3.GetComponent<RectTransform>().localPosition = new Vector3(-150, 0, 0);
        upgrade3.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        upgrade3.interactable = true;
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Active", active);
    }
}
