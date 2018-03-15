using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class UIButton : MonoBehaviour {

    // Component initialized in Awake
    private VRInteractiveItem m_InterItem;

    private Transform textObject;
    private Text text;
    private Color32 unselectedColor = new Color32(255, 255, 255, 255);
    private Color32 selectedColor = new Color32(72, 197, 0, 255);
    private Color32 highlightedColor = new Color32(0, 255, 255, 255);
    public bool isSelected = false;

    public void SetUnselected() { gameObject.GetComponent<Image>().color = unselectedColor; }
    public void SetSelected() { gameObject.GetComponent<Image>().color = selectedColor; }
    public void SetHighLighted() { gameObject.GetComponent<Image>().color = highlightedColor; }

    public GameObject consPanel;

    public void SetButtontext(string buttonText){
        text.text = buttonText;
    }

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

    //Handle the Click event
    private void HandleClick() {
        isSelected = true;
        foreach (Transform child in gameObject.transform.parent.transform)
        {
            if (child != gameObject.transform)
            {
                child.GetComponent<Image>().color = unselectedColor;
                child.GetComponent<UIButton>().isSelected = false;
            }
        }
        gameObject.GetComponent<Image>().color = selectedColor;

        int buttonIndex = gameObject.transform.GetSiblingIndex();

        consPanel.transform.parent.GetComponent<ConsObj>().SetType(buttonIndex);
    }

    //Handle the Over event
    private void HandleOver() {
        gameObject.GetComponent<Image>().color = highlightedColor;
    }

    //Handle the Out event
    private void HandleOut()
    {
        if (isSelected)
        {
            gameObject.GetComponent<Image>().color = selectedColor;
        }
        else
        {
            gameObject.GetComponent<Image>().color = unselectedColor;
        }
    }

	// Use this for initialization
	void Awake () {
        m_InterItem = gameObject.GetComponent<VRInteractiveItem>();
        Transform textObject = gameObject.transform.Find("Text");
        Text text = textObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
