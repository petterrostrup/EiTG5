using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsPanel : MonoBehaviour {

    // Initialization of subelements
    public GameObject UICons;
    public GameObject UISlider;
    public GameObject UIButtonsTwo;
    public GameObject UIButtonsThree;
    public GameObject UIButtonsFour;

    // Utility functions
    public void ResetAll ()
    {
        UIButton[] children = gameObject.GetComponentsInChildren<UIButton>();
        if (children != null)
        {
            foreach (UIButton button in children)
            {
                button.SetUnselected();
            }
        }
        UISlider.SetActive(false);
        UIButtonsTwo.SetActive(false);
        UIButtonsThree.SetActive(false);
        UIButtonsFour.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
