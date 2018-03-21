using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour {

    private enum Choise
    {
        mode, type
    }

    [SerializeField] private Choise choise = Choise.mode;

    public string GetChoise()
    {
        return choise.ToString();
    }

    public void UpdateTypeButton(int type)
    {

    }

    public void UpdateModeButton(int mode)
    {
        UIButton[] buttons = gameObject.GetComponentsInChildren<UIButton>();
        foreach (UIButton button in buttons)
        {
            if (button.transform.GetSiblingIndex() == mode)
            {
                button.SetSelected();
            }
            else
            {
                button.SetUnselected();
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
