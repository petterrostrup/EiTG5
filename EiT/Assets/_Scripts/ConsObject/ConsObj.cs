using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;

public class ConsObj : MonoBehaviour {
    // Component initialized in Awake
    private VRInteractiveItem m_InterItem;
    [System.NonSerialized] public GameObject consPanel;
    Renderer objectRend;

    // Placement of ConsPanel relative to object
    private enum ConsPanelLoc { Top, Right, Left, UpperLeft, UpperRight, LowerRight, LowerLeft }
    [SerializeField] private ConsPanelLoc consPanelLoc = ConsPanelLoc.Top;
    public bool flipConsPanel = false;

    // Current consumption of object
    private int currentPowerCons = 0;
    private int currentWaterCons = 0;

    // Utility functions
    public int GetCurrentPowerCons() { return currentPowerCons; }
    public int GetCurrentWaterCons() { return currentWaterCons; }
    public void SetCurrentPowerCons(int powerCons) {
        currentPowerCons = powerCons;
        UpdateUICons();
    }
    public void SetCurrentWaterCons(int waterCons) {
        currentWaterCons = waterCons;
        UpdateUICons();
    }
    public void SetCurrentCons(int powerCons, int waterCons)
    {
        SetCurrentPowerCons(powerCons);
        SetCurrentWaterCons(waterCons);
        UpdateUICons();
    }

    private void UpdateUICons ()
    {
        Text text = consPanel.transform.Find("UICons").transform.Find("Text").GetComponent<Text>();
        text.text = "";
        if (currentPowerCons != 0)
        {
            text.text += "Strømforbruk: " + currentPowerCons.ToString() + " W";
        }
        if (currentWaterCons != 0)
        {
            text.text += "Vannforbruk: " + currentWaterCons.ToString() + " l/t";
        }
        GameObject.Find("ConsHUD").GetComponent<ConsHUD>().UpdateHUDCons();
    }

    public virtual void SetType(int typeIndex)
    {
        
    }

    public virtual int GetMaxCons()
    {
        return 0;
    }

    public virtual void SetMode(int modeIndex)
    {

    }

    // Utility functions

    public int MaxValue(int[] arr){
        var max = arr[0];
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] > max)
            {
                max = arr[i];
            }
        }
        return max;
    }

    public int MaxValue(int[,] arr)
    {
        var max = arr[0,0];
        int first = arr.GetLength(0);
        int second = arr.GetLength(1);
        for (int i = 0; i < first; i++)
        {
            for (int j = 0; j < second; j++)
            {
                if (arr[i,j] > max)
                {
                    max = arr[i,j];
                }
            }
        }
        return max;
    }

    public virtual void SetModeOff()
    {

    }

    public virtual void SetModeOn()
    {

    }

    public virtual void SetRandomMode()
    {

    }

    public virtual void SetRandomType()
    {

    }


    public virtual void MoveConsPanel()
    {
        Vector3 objectCenter = objectRend.bounds.center;
        Vector3 newConsPanelPos = objectCenter;
        Quaternion objectRot = gameObject.transform.rotation;
        Vector3 objectScale = gameObject.transform.lossyScale;
        float consPanelScale = 0.01f;
        float consPanelHeightPixels = consPanel.GetComponent<RectTransform>().rect.height;
        float consPanelWidthPixels = consPanel.GetComponent<RectTransform>().rect.width;
        float consPanelHeight = consPanelHeightPixels * consPanelScale;
        float consPanelWidth = consPanelWidthPixels * consPanelScale;
        float padding = 5 * consPanelScale;
        float projectionAlongRight = Vector3.Dot(consPanel.transform.right.normalized, objectRend.bounds.extents);
        float projectionRightSignum = Mathf.Sign(projectionAlongRight);
        float projectionAlongLeft = consPanel.transform.right.normalized.x * objectRend.bounds.extents.x + consPanel.transform.right.normalized.z * objectRend.bounds.extents.z; ;
        float projectionLeftSignum = Mathf.Sign(projectionAlongLeft);

        switch (consPanelLoc) //Place panel based on position selected in Unity UI
        {
            case ConsPanelLoc.Top:
                newConsPanelPos.y += objectRend.bounds.extents.y + consPanelHeight/2 + padding;
                break;
            case ConsPanelLoc.Right:
                newConsPanelPos -= consPanel.transform.right * (projectionRightSignum * (consPanelWidth / 2 + padding) + projectionAlongLeft);
                break;
            case ConsPanelLoc.Left:
                newConsPanelPos += consPanel.transform.right * (projectionLeftSignum * (consPanelWidth / 2 + padding) + projectionAlongLeft);
                break;
            case ConsPanelLoc.UpperLeft:
                newConsPanelPos += consPanel.transform.right * (projectionLeftSignum * (consPanelWidth / 2 + padding) + projectionAlongLeft);
                newConsPanelPos.y += objectRend.bounds.extents.y - consPanelHeight / 2;
                break;
            case ConsPanelLoc.UpperRight:
                newConsPanelPos -= consPanel.transform.right * (projectionRightSignum * (consPanelWidth / 2 + padding) + projectionAlongLeft);
                newConsPanelPos.y += objectRend.bounds.extents.y - consPanelHeight / 2;
                break;
            case ConsPanelLoc.LowerRight:
                newConsPanelPos -= consPanel.transform.right * (projectionRightSignum * (consPanelWidth / 2 + padding) + projectionAlongLeft);
                newConsPanelPos.y -= objectRend.bounds.extents.y - consPanelHeight / 2;
                break;
            case ConsPanelLoc.LowerLeft:
                newConsPanelPos += consPanel.transform.right * (projectionLeftSignum * (consPanelWidth / 2 + padding) + projectionAlongLeft);
                newConsPanelPos.y -= objectRend.bounds.extents.y - consPanelHeight / 2;
                break;
            default:
                break;
        }

        consPanel.transform.position = newConsPanelPos;
        consPanel.transform.rotation = objectRot;
        if (!flipConsPanel)
        {
            consPanel.transform.rotation *= Quaternion.Euler(0, 180, 0); //Rotating 180 degrees
        }
        consPanel.GetComponent<ConsPanel>().ResetAll();
        UpdateUICons();
    }

    public void SetupConsPanelCollider()
    {
        consPanel.GetComponent<BoxCollider>().size = new Vector3(consPanel.GetComponent<RectTransform>().rect.width, consPanel.GetComponent<RectTransform>().rect.height, 0.0001f);
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

    public virtual void HandleOver(){
        consPanel.transform.SetParent(gameObject.transform, true);
        consPanel.SetActive(true);
        MoveConsPanel();
    }
    public virtual void HandleOut() { }
    public virtual void HandleClick() { }

    // Use this for initialization
    public virtual void Awake()
    {
        m_InterItem = gameObject.GetComponent<VRInteractiveItem>();
        consPanel = GameObject.Find("ConsPanel");
        objectRend = gameObject.GetComponent<Renderer>();
    }

    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

	}
}
