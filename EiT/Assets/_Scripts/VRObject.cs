using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class VRObject : MonoBehaviour {

    [SerializeField] private VRInteractiveItem m_InteractiveItem;

    [SerializeField] private bool usesPower;
    [SerializeField] private bool usesWater;
    [SerializeField] private int[] powerConsumptions;
    [SerializeField] private int[] waterConsumptions;

    private void OnEnable()
    {
        m_InteractiveItem.OnClick += HandleClick;
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
    }

    private void OnDisable()
    {
        m_InteractiveItem.OnClick -= HandleClick;
        m_InteractiveItem.OnOver -= HandleOver;
        m_InteractiveItem.OnOut -= HandleOut;
    }

    //Handle the Click event
    private void HandleClick()
    {
        
    }

    //Handle the Over event
    private void HandleOver()
    {
        
    }

    //Handle the Out event
    private void HandleOut()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
