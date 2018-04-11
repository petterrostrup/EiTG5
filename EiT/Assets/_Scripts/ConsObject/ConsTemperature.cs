using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsTemperature : ConsObj
{
    // Setup relevant properties
    private enum Mode { On, Off }
    private string[] modeNames = { "På", "Av" };
    private enum Type { TwentyFour, TwentyTwo, Twenty, Eighteen }
    private string[] typeNames = { "24°", "22°", "20°", "18°" };
    private Mode currentMode = Mode.On;
    private Type currentType = Type.TwentyFour;
    int[,] powerConsArray = new int[2, 4] {
        { 2500, 2225, 1950, 1675 },  //On
        { 0, 0, 0, 0 }     //Off
        };
    int[,] waterConsArray = new int[2, 4] {
        { 0, 0, 0, 0 },     //On
        { 0, 0, 0, 0 }      //Off
        };
    int[] yearlyPowerCons = new int[] { 2500*52*84, 2225 * 52 * 84, 1950 * 52 * 84, 1675 * 52 * 84 };

    public bool IsOn()
    {
        return currentMode == Mode.On;
    }

    House house;

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

    private void TurnOff()
    {
        currentMode = Mode.Off;
        SetCurrentPowerCons(powerConsArray[(int)currentMode, (int)currentType]);
    }

    private void TurnOn()
    {
        currentMode = Mode.On;
        SetCurrentPowerCons(powerConsArray[(int)currentMode, (int)currentType]);
    }

    // Used for initialization
    public override void Awake()
    {
        base.Awake();
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

    public override void UpdateUICons()
    {
        base.UpdateUICons();
        Text text = consPanel.transform.Find("UICons").transform.Find("Text").GetComponent<Text>();
        if (yearlyPowerCons[(int)currentType] > 1000)
        {
            text.text += "\n(Gj.snittlig årsforbruk: " + yearlyPowerCons[(int)currentType]/1000 + " MWh)";
        }
        else
        {
            text.text += "\n(Gj.snittlig årsforbruk: " + yearlyPowerCons[(int)currentType] + " kWh)";
        }
    }
}
