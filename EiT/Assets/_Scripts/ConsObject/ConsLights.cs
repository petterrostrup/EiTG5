using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsLights : ConsObj {
    // Setup relevant properties
    private enum Mode { On, Off }
    private enum Type { LED, Halogen, CFL, Incandescent}
    private Mode currentMode = Mode.On;
    private Type currentType = Type.Incandescent;
    int[,] powerConsArray = new int[2, 4] { 
        { 7, 46, 12, 60 },  //On
        { 0, 0, 0, 0 }     //Off
        };
    int[,] waterConsArray = new int[2, 4] { 
        { 0, 0, 0, 0 },     //On
        { 0, 0, 0, 0 }      //Off
        };

    // Setup lights
    public Light[] attachedLights;
    private float[] lightIntensities;

    // Change of state 
    public override void SetType(int typeIndex)
    {
        base.SetType(typeIndex);
        currentType = (Type)typeIndex;
        SetCurrentPowerCons(powerConsArray[(int)currentMode, (int)currentType]);
    }

    public override void SetMode(int modeIndex)
    {
        base.SetType(modeIndex);
        currentMode = (Mode)modeIndex;
        SetCurrentPowerCons(powerConsArray[(int)currentMode, (int)currentType]);
        if ((Mode)modeIndex == Mode.On) { TurnOn(); }
        else if ((Mode)modeIndex == Mode.Off) { TurnOff(); }
    }

    // Setting up ConsPanel
    public override void MoveConsPanel()
    {
        base.MoveConsPanel();

        GameObject UIButtonsFour = consPanel.GetComponent<ConsPanel>().UIButtonsFour;
        UIButtonsFour.SetActive(true);
        UIButton[] buttons = UIButtonsFour.GetComponentsInChildren<UIButton>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = ((Type)i).ToString();
            if (i == (int)currentType)
            {
                buttons[i].SetSelected();
            }
        }

        GameObject UIButtonsTwo = consPanel.GetComponent<ConsPanel>().UIButtonsTwo;
        UIButtonsTwo.SetActive(true);
        buttons = UIButtonsTwo.GetComponentsInChildren<UIButton>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = ((Mode)i).ToString();
            if (i == (int)currentType)
            {
                buttons[i].SetSelected();
            }
        }

        SetupConsPanelCollider();
    }

    // Event handlers
    public override void HandleOver()
    {
        base.HandleOver();
    }

    public override void HandleOut()
    {
        base.HandleOut();
    }

    public override void HandleClick()
    {
        base.HandleClick();
    }

    // Switching on and off lights
    private void TurnOff()
    {
        currentMode= Mode.Off;
        for (int i = 0; i < attachedLights.Length; i++)
        {
            attachedLights[i].intensity = 0;
        }
        SetCurrentPowerCons(powerConsArray[(int)currentMode, (int)currentType]);
    }

    private void TurnOn()
    {
        currentMode = Mode.On;
        for (int i = 0; i < attachedLights.Length; i++)
        {
            attachedLights[i].intensity = lightIntensities[i];
        }
        SetCurrentPowerCons(powerConsArray[(int)currentMode, (int)currentType]);
    }

    // Used for initialization
    public override void Awake()
    {
        base.Awake();

        // Storing initial light intensities
        lightIntensities = new float[2];
        for (int i = 0; i < attachedLights.Length; i++)
        {
            lightIntensities[i] = attachedLights[i].intensity;
        }
    }
}
