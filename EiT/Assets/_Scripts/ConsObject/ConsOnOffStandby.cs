using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsOnOffStandby : ConsObj {

    public enum Mode { On, Standby, Off }
    private string[] modeNames = { "På", "Standby", "Av" };
    public Mode currentMode = Mode.On;
    public int[] powerConsArray = new int[3] { 7, 2, 0 };
    public int[] waterConsArray = new int[3] { 0, 0, 0 };

    public override void SetMode(int modeIndex)
    {
        base.SetType(modeIndex);
        currentMode = (Mode)modeIndex;
        SetCurrentPowerCons(powerConsArray[(int)currentMode]);
    }

    public Mode GetMode()
    {
        return currentMode;
    }

    // Setting up ConsPanel
    public override void MoveConsPanel()
    {
        base.MoveConsPanel();

        GameObject UIButtonsThree = consPanel.GetComponent<ConsPanel>().UIButtonsThree;
        UIButtonsThree.SetActive(true);
        UIButton[] buttons = UIButtonsThree.GetComponentsInChildren<UIButton>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = modeNames[(int)((Mode)i)];
            if (i == (int)currentMode)
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

    // Use this for initialization
    void Start()
    {
        SetCurrentPowerCons(powerConsArray[(int)currentMode]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Utility functions
    public override void SetModeOff()
    {
        SetMode((int)Mode.Off);
    }

    public override void SetModeOn()
    {
        SetMode((int)Mode.On);
    }

    public override void SetRandomMode()
    {
        SetMode((int)Mathf.Floor(Random.value * 3));
    }

    public override int GetMaxCons()
    {
        return MaxValue(powerConsArray);
    }
}
