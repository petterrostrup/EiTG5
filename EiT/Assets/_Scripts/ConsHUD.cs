using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsHUD : MonoBehaviour {

    ConsObj[] consObjects;
    int totalPowerCons = 0;
    Text text;
    float currentYRotation;

	// Use this for initialization
	void Start () {
        consObjects = GameObject.FindObjectsOfType<ConsObj>();
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
        text.text = "Power consumption: " + totalPowerCons + "W";
    }
	
	// Update is called once per frame
	void Update () {
	}
}
