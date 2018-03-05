using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class ConsObj : MonoBehaviour {
    // Component initialized in Awake
    private VRInteractiveItem m_InterItem;

    // Current consumption of object
    private int currentPowerCons = 0;
    private int currentWaterCons = 0;

    // Utility functions
    public int GetCurrentPowerCons() { return currentPowerCons; }
    public int GetCurrentWaterCons() { return currentWaterCons; }
    public void SetCurrentPowerCons(int powerCons) { currentPowerCons = powerCons; }
    public void SetCurrentWaterCons(int waterCons) { currentWaterCons = waterCons; }
    public void SetCurrentCons(int powerCons, int waterCons)
    {
        SetCurrentPowerCons(powerCons);
        SetCurrentWaterCons(waterCons);
    }

    // Event handlers (can be overridden by the derived classes)
    private void OnEnable()
    {
        m_InterItem.OnClick += HandleClick;
        m_InterItem.OnOver += HandleOver;
        m_InterItem.OnOut += HandleOut;
    }

    private void OnDisable()
    {
        m_InterItem.OnClick -= HandleClick;
        m_InterItem.OnOver -= HandleOver;
        m_InterItem.OnOut -= HandleOut;
    }

    public virtual void HandleOver(){ }
    public virtual void HandleOut() { }
    public virtual void HandleClick() { }

    // Use this for initialization
    public virtual void Awake()
    {
        m_InterItem = gameObject.GetComponent<VRInteractiveItem>();
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
