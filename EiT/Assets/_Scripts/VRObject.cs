using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class VRObject : MonoBehaviour {

    private VRInteractiveItem m_InteractiveItem;
    Renderer rend;
    [SerializeField] private GameObject usagePanel;
    Vector3 usagePanelScale;

    public bool usesPower;
    public bool usesWater;
    public int[] powerConsumptions;
    public int[] waterConsumptions;
    public Vector3 usagePanelPosition;

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
            //Display the information panel for the object
            usagePanel.SetActive(true);

            Vector3 objectPos = rend.bounds.center;
            Quaternion objectRot = m_InteractiveItem.transform.rotation;

            objectPos.y += rend.bounds.extents.y + 0.3f;

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
        rend = m_InteractiveItem.GetComponent<Renderer>();
    }
    void Awake ()
    {
        m_InteractiveItem = gameObject.GetComponent<VRInteractiveItem>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}