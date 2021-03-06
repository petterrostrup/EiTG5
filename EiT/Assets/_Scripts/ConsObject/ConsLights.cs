﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsLights : ConsObj {
    // Setup relevant properties
    private enum Mode { On, Off }
    private string[] modeNames = { "På", "Av" };
    private enum Type { LED, Halogen, CFL, Incandescent}
    private string[] typeNames = { "LED", "Halogen", "Sparepære", "Glødepære" };
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
    int[] yearlyPowerCons = new int[] { 20, 134, 35, 175 };

    public bool IsOn()
    {
        return currentMode == Mode.On;
    }

    // Setup lights
    public Light[] attachedLights;
    private float[] lightIntensities;
    private int numLamps = 0;
    private int lightStep = 0;
    Color curAmbLight;
    int lightLevel;
    House house;

    // Lamp cover
    public LampCover lampCover;

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

    public override int GetMaxCons()
    {
        return MaxValue(powerConsArray);
    }

    public override void UpdateUICons()
    {
        base.UpdateUICons();
        Text text = consPanel.transform.Find("UICons").transform.Find("Text").GetComponent<Text>();
        text.text += "\n(Gj.snittlig årsforbruk: " + yearlyPowerCons[(int)currentType] + " kWh)";
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
            buttons[i].GetComponentInChildren<Text>().text = typeNames[(int)((Type)i)];
            if (i == (int)currentType)
            {
                buttons[i].SetSelected();
            }
            else
            {
                buttons[i].SetUnselected();
            }
        }

        GameObject UIButtonsTwo = consPanel.GetComponent<ConsPanel>().UIButtonsTwo;
        UIButtonsTwo.SetActive(true);
        buttons = UIButtonsTwo.GetComponentsInChildren<UIButton>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = modeNames[(int)((Mode)i)];
            if (i == (int)currentMode)
            {
                buttons[i].SetSelected();
            }
            else
            {
                buttons[i].SetUnselected();
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
        house.UpdateLight();
        lampCover.TurnOff();
    }

    private void TurnOn()
    {
        currentMode = Mode.On;
        for (int i = 0; i < attachedLights.Length; i++)
        {
            attachedLights[i].intensity = lightIntensities[i];
        }
        SetCurrentPowerCons(powerConsArray[(int)currentMode, (int)currentType]);
        house.UpdateLight();
        lampCover.TurnOn();
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
        house = GameObject.FindObjectOfType<House>();
    }

    // Used for initialization
    public void Start()
    {
        SetCurrentPowerCons(powerConsArray[(int)currentMode, (int)currentType]);
    }

    // Utility functions
    public override void SetModeOff()
    {
        TurnOff();
    }

    public override void SetModeOn()
    {
        TurnOn();
    }

    public override void SetRandomMode()
    {
        if (Random.value > 0.5f)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public override void SetRandomType()
    {
        SetType((int)Mathf.Floor(Random.value * 4));
    }
}
