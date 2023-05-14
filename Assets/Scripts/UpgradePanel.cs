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
        SetUpgrades();
    }
    public void ClosePanel() {
        gameObject.SetActive(false);
        foreach(Button i in upgrades) {
            i.interactable = true;
        }
    }
    void SetUpgrades() {
        Button upgrade1 = Instantiate(upgrades[0]);
        upgrade1.transform.SetParent(transform);
        upgrade1.GetComponent<RectTransform>().anchorMin = new Vector2(0f,0.5f);
        upgrade1.GetComponent<RectTransform>().anchorMax = new Vector2(0f,0.5f);
        upgrade1.GetComponent<RectTransform>().localPosition = new Vector3(150, 0, 0);
        upgrade1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        upgrade1.interactable = true;

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
        if (upgrades[0].GetComponent<Upgrade>().clicked) {
            upgrades[1].interactable = false;
            upgrades[2].interactable = false;
        }
        else if (upgrades[1].GetComponent<Upgrade>().clicked) {
            upgrades[0].interactable = false;
            upgrades[2].interactable = false;
        }
        else if (upgrades[2].GetComponent<Upgrade>().clicked) {
            upgrades[1].interactable = false;
            upgrades[0].interactable = false;
        }
    }
}
