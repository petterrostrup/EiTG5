using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsHUD : MonoBehaviour {

    ConsObj[] consObjects;
    int totalPowerCons = 0;
    Text text;
    float currentYRotation;
    ConsBar consBar;

    // Use this for initialization
    void Start () {
        consObjects = GameObject.FindObjectsOfType<ConsObj>();
	}

    public int GetTotalConsumption()
    {
        return totalPowerCons;
    }

    void Awake ()
    {
        consBar = GameObject.Find("ConsBar").GetComponent<ConsBar>();
    }

    public void UpdateHUDCons()
    {
        totalPowerCons = 0;

        consObjects = GameObject.FindObjectsOfType<ConsObj>();
        foreach (ConsObj obj in consObjects)
        {
            totalPowerCons += obj.GetCurrentPowerCons();
        }

        text = gameObject.GetComponentInChildren<Text>();
        text.text = "Strømforbruk: " + totalPowerCons + "W";
        consBar.SetCurrentCons(totalPowerCons);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
