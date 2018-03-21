using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

    int numLamps, lightStep;
    ConsLights[] lamps;
    int lightsOn = 0;

    public void UpdateLight()
    {
        lightsOn = 0;
        foreach (ConsLights lamp in lamps)
        {
            if (lamp.IsOn()) { lightsOn += 1; }
        }
        byte lightLevel = (byte)Mathf.Floor(26 + 57 * lightsOn);
        RenderSettings.ambientLight = new Color32(lightLevel, lightLevel, lightLevel, 255);
    }

    // Use this for initialization
    void Awake () {
        lamps = GameObject.FindObjectsOfType<ConsLights>();
        numLamps = lamps.Length;
        Debug.Log(numLamps);
        lightStep = (int)Mathf.Floor((255 - 26) / 4);

        UpdateLight();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
