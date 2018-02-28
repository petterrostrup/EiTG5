using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class VRObject : MonoBehaviour {

    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    [SerializeField] private GameObject usagePanel;
    Vector3 usagePanelScale;

    [SerializeField] private bool usesPower;
    [SerializeField] private bool usesWater;
    [SerializeField] private int[] powerConsumptions;
    [SerializeField] private int[] waterConsumptions;
    [SerializeField] Vector3 usagePanelPosition;

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
        if (usesPower || usesWater)
        {
            usagePanel.SetActive(true);

            Vector3 objectPos = m_InteractiveItem.transform.position;
            Quaternion objectRot = m_InteractiveItem.transform.rotation;

            objectPos.y += 1.3f + m_InteractiveItem.transform.localScale.y;

            usagePanel.transform.position = objectPos;
            usagePanel.transform.rotation = objectRot;

            Transform child = usagePanel.transform.Find("UsageText");
            Text t = child.GetComponent<Text>();
            t.text = "";

            if (usesPower)
            {
                t.text = t.text + "Power consumption:  " + powerConsumptions[0].ToString() + "W";
            }
            if (usesWater)
            {
                t.text = t.text + "\nWater consumption: " + waterConsumptions[0].ToString() + "L/h";
            }
        }
    }

    //Handle the Out event
    private void HandleOut()
    {
        usagePanel.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        usagePanel.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
