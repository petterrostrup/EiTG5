using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsOnOffStandby : ConsObj {

    private enum Mode { On, Standby, Off }
    private Mode currentMode = Mode.On;
    public int[] powerConsArray = new int[3] { 7, 2, 0 };
    public int[] waterConsArray = new int[3] { 0, 0, 0 };

    public override void SetMode(int modeIndex)
    {
        base.SetType(modeIndex);
        currentMode = (Mode)modeIndex;
        SetCurrentPowerCons(powerConsArray[(int)currentMode]);
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
            buttons[i].GetComponentInChildren<Text>().text = ((Mode)i).ToString();
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
}
