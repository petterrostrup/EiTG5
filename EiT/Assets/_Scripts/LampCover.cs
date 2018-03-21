using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class LampCover : MonoBehaviour {

    ConsLights parent;
    public Material LampOn;
    public Material LampOff;

    public void TurnOn()
    {
        gameObject.GetComponent<Renderer>().material = LampOn;
    }

    public void TurnOff()
    {
        gameObject.GetComponent<Renderer>().material = LampOff;
    }

    // Use this for initialization
    void Awake () {
        parent = gameObject.transform.parent.GetComponent<ConsLights>();
        Debug.Log(parent.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Event handlers (can be overridden by the derived classes)
    private void OnEnable()
    {
        gameObject.GetComponent<VRInteractiveItem>().OnClick += HandleClick;
        gameObject.GetComponent<VRInteractiveItem>().OnOver += HandleOver;
        gameObject.GetComponent<VRInteractiveItem>().OnOut += HandleOut;
    }

    private void OnDisable()
    {
        gameObject.GetComponent<VRInteractiveItem>().OnClick -= HandleClick;
        gameObject.GetComponent<VRInteractiveItem>().OnOver -= HandleOver;
        gameObject.GetComponent<VRInteractiveItem>().OnOut -= HandleOut;
    }

    // Event handlers
    public void HandleOver()
    {
        parent.HandleOver();   
    }

    public void HandleOut()
    {
        parent.HandleOut();
    }

    public void HandleClick()
    {
        parent.HandleClick();
    }
}
